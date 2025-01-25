using System.Collections.Generic;
using UnityEngine;

public class Bubble
{
    public float shellValue { get; set; }

    float delta = 0.25f;
    float dt = 0.1f;

    public void UpdateValue()
    {
        float stdDev = delta * delta * dt;
        // stdDev *= Random.value * 2;
        shellValue = shellValue + GaussianRandom(stdDev);
    }

    // [TODO] à équilibrer
    public static float GaussianRandom(float stdDev)
    {
        float u1 = Random.value;
        float u2 = Random.value;
        float randomStdNormal = Mathf.Sqrt(-2f * Mathf.Log(u1, 10)) * Mathf.Sin(2 * Mathf.PI * u2);
        return randomStdNormal * stdDev;
    }
}
