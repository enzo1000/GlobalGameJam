using TMPro;
using UnityEngine;

public class NewsScript : MonoBehaviour
{
    //News Script
    private string description; //Description of the news
    private float variation; //Influence in the variation of the related action

    public NewsScriptableObject NSO;

    //ASO Script
    private string newsName; //News name
    private string newsDescription; //Description of the news
    private float newsVariation;
    public int NSOindex = 0;

    //UI
    public GameObject UI_newsName;
    public GameObject UI_newsDescription;
    public GameObject UI_newsVariation;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        newsName = NSO.newsParamList[NSOindex].newsName;
        newsDescription = NSO.newsParamList[NSOindex].newsDescription;
        newsVariation = NSO.newsParamList[NSOindex].newsVariation;

        UI_newsName.GetComponent<TMP_Text>().text = newsName;
        UI_newsDescription.GetComponent<TMP_Text>().text = newsDescription;
        UI_newsVariation.GetComponent<TMP_Text>().text = newsVariation.ToString();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateAction(ActionScript action, float variation)
    {
        // [ToDo] a corriger
        newsVariation = variation;
        UI_newsVariation.GetComponent<TMP_Text>().text = newsVariation.ToString();

        action.UpdateBubbleValue(newsVariation);
    }

    public void InitData(int idx)
    {
        NSOindex = idx;

        newsName = NSO.newsParamList[NSOindex].newsName;
        newsDescription = NSO.newsParamList[NSOindex].newsDescription;
        newsVariation = NSO.newsParamList[NSOindex].newsVariation;

        UI_newsName.GetComponent<TMP_Text>().text = newsName;
        UI_newsDescription.GetComponent<TMP_Text>().text = newsDescription;
        UI_newsVariation.GetComponent<TMP_Text>().text = newsVariation.ToString();
    }
}
