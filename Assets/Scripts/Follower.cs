using UnityEngine;
using System.Collections.Generic;
public class Follower : MonoBehaviour
{
    public List<ActionScript> actions = new List<ActionScript>();
    GameManager gameManager;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public void Buy(ActionScript action)
    {
        gameManager.SummonNews(action, NewsType.FollowerBuy);
        actions.Add(action);
    }

    public void Sell(ActionScript action, PlayerManager playerManager)
    {
        gameManager.SummonNews(action, NewsType.FollowerSell);
        actions.Remove(action);
        if (action.currentBubbleValue <= action.baseBubbleValue + (action.baseBubbleValue * 15 / 100)) // [TODO] � �quilibrer
        {
            Destroy(this);
        } 
        else if (action.currentBubbleValue >= action.baseBubbleValue + (action.baseBubbleValue * 15 / 100)) // [TODO] � �quilibrer
        {
            playerManager.earnFollowers(computeNewFollower(action.investDanger));
        }
    }

    // [TODO] � �quilibrer
    private int computeNewFollower(float speculativeBubbleChance)
    {
        return 1;
    }
}
