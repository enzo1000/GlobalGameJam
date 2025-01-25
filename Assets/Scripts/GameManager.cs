using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum NewsType
{
    NaturalEvent,
    FollowerBuy,
    FollowerSell
}

public class GameManager : MonoBehaviour
{
    [SerializeField] NewsScriptableObject newsData;
    [SerializeField] GameObject newsPrefab;

    private Bubble bubble;
    private float bubbleTimer = 0.0f;

    [SerializeField] private PlayerManager player;

    private float victoryGoal;
    List<ActionScript> visibleActions = new List<ActionScript>();

    private float newsTimer = 0.0f;
    // [TODO] à équilibrer
    private float timeBetweenNews = 10.0f;
    private float newsProba = 0.5f;

    void Start()
    {
        bubble = new Bubble();
    }

    void Update()
    {
        // condition de victoire
        if (player.shellNumber >= victoryGoal)
            Debug.Log("GG tu as gagne un milleTplat + cul de la cadreuse");

        // cours de la bubble
        bubbleTimer += Time.deltaTime;
        if(bubbleTimer >= 1.0f)
        {
            bubbleTimer = 0.0f;
            bubble.UpdateValue();
        }


        // apparition des news naturelles
        newsTimer += Time.deltaTime;
        if(newsTimer >= timeBetweenNews)
        {
            newsTimer = 0.0f;
            if(Random.value < newsProba)
            {
                if(player.actions.Count > 0)
                {
                    ActionScript action = player.actions.ElementAt(Random.Range(0, player.actions.Count)).Key;
                    SummonNews(action, NewsType.NaturalEvent);
                }
            }
        }
    }

    // [TODO] à équilibrer
    public void UpdateTimeBetweenNews(float nbFollowers)
    {

    }

    // [TODO] à équilibrer
    public void UpdateNewProba(float nbFollowers)
    {

    }

    // --------------|-------------------------------|--------------
    // bulle explose  bulle n'explose pas et augmente  bulle n'explose pas et descend quand meme (mais peut remonter)

    public void SummonNews(ActionScript action, NewsType type)
    {
        float variation = computeVariationValue(action.speculativeBubbleChance);

        switch(type)
        {
            case NewsType.NaturalEvent:
                if (action.asExplosed || Random.value <= action.speculativeBubbleChance) // bulle explose
                {
                    variation *= -1.0f;
                }
                else
                {
                    if (Random.value >= 1 - action.speculativeBubbleChance) // [TODO] à voir
                    {
                        variation *= -1.0f;
                    }
                }
                break;
            case NewsType.FollowerBuy:
                if (action.asExplosed || Random.value <= action.speculativeBubbleChance) // bulle explose
                {
                    variation *= -1.0f;
                }
                break;
            case NewsType.FollowerSell:
                variation *= -1.0f;
                break;
            default:
                return;
        }

        NewsScript news = Instantiate(newsPrefab).GetComponent<NewsScript>();
        news.InitData(Random.Range(0, newsData.newsParamList.Count));
        news.UpdateAction(action, variation);
    }

    // [TODO] à équilibrer
    private float computeVariationValue(float speculativeBubbleChance)
    {
        float variation = 1f;
        return variation;
    }
}
