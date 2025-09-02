using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Ball : MonoBehaviour
{
    public int number;

    public bool followTarget;
    public Transform Target;

    public Transform myChild;
    public Renderer mesh;
    public bool removed;

    bool isFinished;
    float speed;


    Rigidbody rb;
    public bool isMerging;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        myChild = transform.GetChild(0);
        mesh = myChild.GetComponent<Renderer>();

    }

    public void JoinTheFllow(Transform Target)
    {
        if (!removed)
        {
            followTarget = true;
            this.Target = Target;
            transform.SetParent(BallsController.instance.transform);

            CorrectPosition();

            rb.constraints = RigidbodyConstraints.FreezePositionY;
            rb.freezeRotation = true;

            rb.drag = 20;
        }

    }

    public void QuitTheFllow()
    {
        followTarget = false;
        this.Target = null;
        transform.SetParent(null);
        rb.constraints = RigidbodyConstraints.None;
        rb.freezeRotation = true;
        rb.drag = 0;
        removed = true;
    }

    private void FixedUpdate()
    {

        if (followTarget )
        {
            if (GameManager.instance.isStoped)
                return;
         

            rb.MovePosition(Vector3.Lerp(transform.position, Target.position, 0.07f));

             myChild.Rotate((Target.right *10/transform.localScale.x),Space.World);
       


        }

        if (isFinished)
        {
            speed += Time.deltaTime*0.01f;
            rb.MovePosition(Vector3.Lerp(transform.position, GameManager.instance.Stairs.position, speed));
            transform.Rotate(Vector3.right*30/ transform.localScale.x, Space.World);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (followTarget & collision.collider.CompareTag("Ball"))
        {
            Ball ball = collision.collider.GetComponent<Ball>();
            BallsController.instance.CheckNumbers(this, ball);
        }

        if (collision.collider.CompareTag("Obstacle") & !isMerging )
        {
            BallsController.instance.RemoveBall(this);
            GameObject vfx = Instantiate(BallsController.instance.destroyVfx, transform.position,Quaternion.identity);
            vfx.GetComponent<VFXColor>().ChangeDestroyVfx(mesh.material, number);
            Destroy(this.gameObject);
        }



    }


    public void Remove()
    {
        isMerging = true;
        StartCoroutine(RemoveCoroutine());
    }

    IEnumerator RemoveCoroutine()
    {
        GetComponent<Collider>().isTrigger = true;
        yield return new WaitForSeconds(0.1f);
        BallsController.instance.RemoveBall(this);
        Destroy(this.gameObject);

    }

    public void Merge()
    {
        Material[] mat = { BallsController.instance.materials[number], BallsController.instance.numbers[number] };
        mesh.sharedMaterials = mat;
        ShowVfx();

        ChangeBall(1);

    }


    void ShowVfx()
    {

        GameObject vfx = Instantiate(BallsController.instance.VFX, transform);
        vfx.GetComponent<VFXColor>().ChangeSize(number);
        vfx.GetComponent<VFXColor>().ChangeColor(mesh.material.color);
        Destroy(vfx, 2f);
    }

    void ChangeBall(int num)
    {
        isMerging = true;

        Sequence Mysequence = DOTween.Sequence();
        Mysequence.Append(myChild.DOScale(myChild.localScale.x +(num * 0.6f), 0.1f));
        Mysequence.Append(transform.DOScale(transform.localScale.x + (transform.localScale.x *(num * 0.2f)), 0.05f));
        Mysequence.Append(myChild.DOScale(1, 0.1f));
        Mysequence.OnComplete(()=>MergeDone(num));

    }

    void MergeDone(int num)
    {
        CorrectPosition();
        rb.isKinematic = true;
        rb.isKinematic = false;
        number+=num;
        isMerging = false;
    }

    public void Multiply(int numb)
    {
        if (!isMerging)
        {
            int num = numb / 2;
            if (number + num > BallsController.instance.maxNumber)
                return;

            Material[] mat = { BallsController.instance.materials[number + num - 1], BallsController.instance.numbers[number + num - 1] };
            mesh.sharedMaterials = mat;

            ChangeBall(num);
        }



    }

    public void Division(int numb)
    {
        if (!isMerging)
        {
            int num = numb / 2;
            if (number - num <= 0)
                return;

            Material[] mat = { BallsController.instance.materials[number - num - 1], BallsController.instance.numbers[number - num - 1] };
            mesh.sharedMaterials = mat;

            ChangeBall(-num);
        }
       


    }


    void CorrectPosition()
    {
        float y = GetComponent<SphereCollider>().bounds.size.y / 2;
        transform.position = new Vector3(transform.position.x, Target.position.y + y, transform.position.z);
    }

    public void DiseableCollider()
    {
        if (rb)
        {
            rb.isKinematic = true;
            GetComponent<Collider>().enabled = false;
        }

    }

    public void FinishTheRace()
    {
        Vector3 pos = new Vector3(GameManager.instance.startStairs.position.x, transform.position.y, GameManager.instance.startStairs.position.z);
        rb.DOMove(pos, .5f).OnComplete(Jump);
        GameManager.instance.ChangeCameraTarget(transform);
        BallsController.instance.RemoveBall(this);

    }



    public void Jump()
    {
        isFinished = true;
        
        rb.constraints = RigidbodyConstraints.FreezePositionX;
    }

    public void Done()
    {
        GameManager.instance.Win();
        transform.rotation = Quaternion.Euler(0, 90, 40);
        transform.GetChild(0).localRotation = Quaternion.Euler(0, 0, 0);
        rb.freezeRotation = true;

    }

    public void Replaced(Transform tr)
    {
        if (isFinished)
        {
            isFinished = false;
            rb.DOJump(tr.position,2,1 ,.5f) ;
            Done();
        }

    }
}
