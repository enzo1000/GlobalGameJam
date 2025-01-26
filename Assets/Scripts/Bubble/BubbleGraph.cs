using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BubbleGraph : MonoBehaviour
{
    [SerializeField] private Sprite circleSprite;
    [SerializeField] private TMP_Text lastValue;
    [SerializeField] private GameObject o_O;
    private RectTransform graphContainer;

    private void Awake()
    {
        graphContainer = transform.Find("graphContainer").GetComponent<RectTransform>();
    }

    private GameObject CreateCircle(Vector2 anchoredPosition)
    {
        GameObject gameObject = new GameObject("circle", typeof(Image));
        gameObject.transform.SetParent(graphContainer, false);
        gameObject.GetComponent<Image>().sprite = circleSprite;
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = anchoredPosition;
        rectTransform.sizeDelta = new Vector2(11, 11);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        return gameObject;
    }

    public void ShowGraph(List<float> valueList)
    {
        ClearGraph();

        float grapHeight = graphContainer.sizeDelta.y;
        float yMaximum = 30f;
        float xMinimum = 0.2f;
        float xSize = 30f;

        GameObject lastCircleGameObject = null;
        for (int i = 0; i < valueList.Count; i++)
        {
            float xPosition = xSize + i * xSize;
            float yPosition = (xMinimum + (valueList[i] / yMaximum)) * grapHeight;

            GameObject circleGameObject = CreateCircle(new Vector2(xPosition, yPosition));
            if (lastCircleGameObject != null)
            {
                CreateDotConnection(lastCircleGameObject.GetComponent<RectTransform>().anchoredPosition, circleGameObject.GetComponent<RectTransform>().anchoredPosition);
            }
            lastCircleGameObject = circleGameObject;
        }

        if(valueList[valueList.Count - 1] > 9)
        {
            o_O.SetActive(true);
        }
        else
        {
            o_O.SetActive(false);
        }

        String text = Round(valueList[valueList.Count - 1], 3).ToString();
        while(text.Length < 5)
        {
            text += "0";
        }
        lastValue.text = text;
    }

    private void ClearGraph()
    {
        foreach(Transform t in graphContainer.transform)
        {
            if(t.name == "circle" || t.name == "dotConnection" || t.name == "text")
                Destroy(t.gameObject);
        }
    }

    private void CreateDotConnection(Vector2 dotPositionA, Vector2 dotPositionB)
    {
        GameObject gameObject = new GameObject("dotConnection", typeof(Image));
        gameObject.transform.SetParent(graphContainer, false);
        if(dotPositionA.y > dotPositionB.y)
            gameObject.GetComponent<Image>().color = new Color(253.0f/256.0f, 157.0f/256.0f, 206.0f/256.0f, 1f);
        else
            gameObject.GetComponent<Image>().color = new Color(18.0f / 256.0f, 205.0f / 256.0f, 212.0f / 256.0f, 1f);
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        Vector2 dir = (dotPositionB - dotPositionA).normalized;
        float distance = Vector2.Distance(dotPositionA, dotPositionB);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        rectTransform.sizeDelta = new Vector2(distance, 3f);
        rectTransform.anchoredPosition = dotPositionA + dir * distance * 0.5f;
        rectTransform.localEulerAngles = new Vector3(0, 0, GetAngleFromVectorFloat(dir));
    }

    public static float GetAngleFromVectorFloat(Vector3 dir)
    {
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;
        return n;
    }

    public static float Round(float value, int digits)
    {
        float mult = Mathf.Pow(10.0f, (float)digits);
        return Mathf.Round(value * mult) / mult;
    }
}
