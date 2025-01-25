using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class CoursBubulle : MonoBehaviour
{
    void Start()
    {
        float delta = 0.25f;
        float dt = 0.1f;
        float x = 0f;

        int n = 100;

        List<Color> col = new List<Color>();
        col.Add(Color.white);
        col.Add(Color.yellow);
        col.Add(Color.green);
        col.Add(Color.blue);
        col.Add(Color.red);

        for(int j = 0; j < 5; j++)
        {
            for (int i = 0; i < n; i++)
            {
                x = x + GaussianRandom(delta * delta * dt * Random.value * 2);
                GameObject point = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                point.transform.position = new Vector3(i, x * 100, 0);
                point.GetComponent<Renderer>().material.color = col[j];
            }
            x = 0.0f;
        }
        
    }

    public static float GaussianRandom(float stdDev)
    {
        float u1 = Random.value;
        float u2 = Random.value;
        float randomStdNormal = Mathf.Sqrt(-2f * Mathf.Log(u1, 10)) * Mathf.Sin(2 * Mathf.PI * u2);
        return randomStdNormal * stdDev;
    }
}
