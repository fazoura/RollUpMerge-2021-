using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXColor : MonoBehaviour
{

    public void ChangeColor(Color color)
    {
        foreach(Transform child in transform)
        {
            ParticleSystem particleSystem = child.GetComponent<ParticleSystem>();
            if (particleSystem)
            {
                ParticleSystem.MainModule settings = particleSystem.main;
                settings.startColor = new ParticleSystem.MinMaxGradient(color);
            }
        }

        gameObject.SetActive(true);
    }

    public void ChangeSize(float Size)
    {
        foreach (Transform child in transform)
        {
            ParticleSystem particleSystem = child.GetComponent<ParticleSystem>();
            if (particleSystem)
            {
                child.localScale += (.2f * Size * child.localScale);

            }
        }
    }

    public void ChangeDestroyVfx(Material material, int Size)
    {
        ParticleSystem particleSystem = GetComponent<ParticleSystem>();
        if (particleSystem)
        {
            GetComponent<ParticleSystemRenderer>().material = material;
            short min = (short)(5 +( Size * 2));
            short max = (short)(min + 2); 
            var emission = particleSystem.emission;
            emission.SetBursts(new ParticleSystem.Burst[] { new ParticleSystem.Burst(0.0f, min, max)});
            transform.localScale += (transform.localScale * Size * 0.05f);

        }
        gameObject.SetActive(true);
    }
}
