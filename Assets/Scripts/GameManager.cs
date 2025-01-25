using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public enum NewsType
{
    NaturalEvent,
    FollowerBuy,
    FollowerSell
}

public class GameManager : MonoBehaviour
{
    private Bubble bubble;
    private PlayerManager player;

    private float victoryGoal;
    List<ActionScript> visibleActions = new List<ActionScript>();

    private float newsTimer;
    // [TODO] � �quilibrer
    private float timeBetweenNews;
    private float newsProba;

    void Start()
    {
        bubble = new Bubble();
    }

    void Update()
    {
        
    }

    // [TODO] � �quilibrer
    public void UpdateTimeBetweenNews(float nbFollowers)
    {

    }

    // [TODO] � �quilibrer
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
                    if (Random.value >= 1 - action.speculativeBubbleChance) // [TODO] � voir
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
    }

    // [TODO] � �quilibrer
    private float computeVariationValue(float speculativeBubbleChance)
    {
        float variation = 1f;
        return variation;
    }
}
