using System.Collections.Generic;
using UnityEngine;

public class Bubble: MonoBehaviour
{
    [SerializeField] BubbleGraph graph;

    List<float> lastData = new List<float>();

    public float shellValue { get; set; }

    float delta = 0.25f;
    float dt = 0.1f;

    int compteur = 0;

    private void Start()
    {
        shellValue = 1.0f;
    }

    public void UpdateValue()
    {
        compteur += 1;
        float stdDev = delta * delta * dt;
        // stdDev *= Random.value * 2;
        shellValue = shellValue + GaussianRandom(stdDev) * 100;

        if (shellValue <= 0)
            shellValue = Random.Range(0.01f, 0.07f);
        else if (shellValue >= 10)
            shellValue = Random.Range(9.01f, 9.06f);

        lastData.Add(shellValue);
        if (lastData.Count > 30)
        {
            lastData.RemoveAt(0);
        }

        graph.ShowGraph(lastData);
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
