using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallsController : MonoBehaviour
{
    public static BallsController instance;


    public Transform TargetPosition;
    public Ball Leader;
    public List<Ball> balls = new List<Ball>();

    public List<Material> materials = new List<Material>();
    public List<Material> numbers = new List<Material>();
    public GameObject VFX;
    public GameObject destroyVfx;
    public int maxNumber;


    private void Awake()
    {
        instance = this;
    }


    private void Start()
    {
        if (transform.childCount > 0)
        {
            Ball firstBall = transform.GetChild(0).GetComponent<Ball>();
            if (firstBall)
            {
                AddNewBall(firstBall);
            }
        }

        maxNumber = materials.Count;
    }

    public void StopOtherBalls()
    {

        for (int i = 0; i < balls.Count; i++)
        {
            if(balls[i]!=Leader)
            balls[i].DiseableCollider();

        }

    }

    public void FindLeader()
    {
        int maxNumber=0;
        int index = 0;

        for(int i = 0; i < balls.Count; i++)
        {
            if (balls[i].number > maxNumber)
            {
                maxNumber = balls[i].number;
                index = i;
            }
               
        }
        Leader = balls[index];

        if (Leader.isMerging)
        {
            StartCoroutine(ChangeLeaderCoroutine());
        }

        else
        {
            StopOtherBalls();

            Leader.FinishTheRace();
        }


    }

    IEnumerator ChangeLeaderCoroutine()
    {
        yield return null;
        FindLeader();
    }


    private void AddNewBall(Ball ball)
    {
        if (!balls.Contains(ball))
        {
            balls.Add(ball);
            ball.JoinTheFllow(TargetPosition);
        }
    }

    public void RemoveBall(Ball ball)
    {
        if (balls.Contains(ball))
        {
            balls.Remove(ball);
            ball.QuitTheFllow();
        }
        if (transform.childCount == 0 & !GameManager.instance.isStoped)
        {
            GameManager.instance.Lose();
        }
    }

    public void CheckNumbers(Ball ball1, Ball ball2)
    {
        AddNewBall(ball2);

        if (ball1.number == ball2.number & !ball1.isMerging & !ball2.isMerging)
            Merge(ball1,ball2);
            
    }


    private void Merge(Ball ball1,Ball ball2)
    {
        Vector3 position = ball1.transform.position;
        Quaternion rotation = ball1.transform.rotation;
        int number = ball1.number;

        if (number < maxNumber)
        {

            ball1.Remove();
            ball2.Merge();

        }
        
    }
}
