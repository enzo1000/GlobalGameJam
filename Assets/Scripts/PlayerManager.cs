using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public enum Order
{
    Buy,
    Sell
}

public class PlayerManager : MonoBehaviour
{
    public float shellNumber { get; set; }
    private float bubbleNumber;
    private List<Follower> followers = new List<Follower>();
    public Dictionary<ActionScript, int> actions = new Dictionary<ActionScript, int>();
    private ActionScript selectedAction;
    public ActionScriptableObject selectedActionObject;
    public List<ActionScript> listActionScript;

    private void Start()
    {
        shellNumber = .0f;
        bubbleNumber = 30 000f;
        actions.Add(listActionScript[0], 10);
        actions.Add(listActionScript[1], 20);
        actions.Add(listActionScript[2], 30);
    }

    public void OnBuyAction(ActionScript action, int quantity)
    {
        if (quantity * action.currentBubbleValue < bubbleNumber)
        {
            if (actions.ContainsKey(action))
                actions[action] += quantity;
            else
                actions.Add(action, quantity);
               
            action.playerInvested += quantity * action.currentBubbleValue;
        }
    }

    public int OnSellAction(ActionScript action, int quantity)
    {
        if (!actions.ContainsKey(action) || actions[action] < quantity)
            return -1;

        bubbleNumber += quantity * action.currentBubbleValue;
        actions[action] -= quantity;
        if (actions[action] <= 0)
        {
            actions.Remove(action);
            return 0;
        }
        else
            return actions[action];
    }

    // lancer des corroutines pour le faire dans le temps
    public void OnCreatePost(ActionScript action, Order order, float announcedRisk)
    {
        if (order == Order.Buy)
        {
            float participationProbability = computeParticipationProbability(action, announcedRisk);
            foreach(var follower in followers)
            {
                if(Random.value <= participationProbability)
                    follower.Buy(action);
            }
        }
        if (order == Order.Sell)
        {
            foreach(var follower in followers)
            {
                if(follower.actions.Contains(action))
                {
                    follower.Sell(action, this);
                }
            }
        }

    }

    // [TODO] à équilibrer
    private float computeParticipationProbability(ActionScript action, float announcedRisk)
    {
        return .5f;
    }

    public void earnFollowers(int value)
    {
        for (int i = 0; i < value; i++)
            followers.Add(new Follower());
    }

    //AME pour l'ui
    public int GetFollowersCount()
    {
        return followers.Count;
    }

    //AME pour l'ui
    public float GetBubbleCount()
    {
        return bubbleNumber;
    }

    public void OnMarketQuit()
    {
        shellNumber = GameObject.Find("Bubble").GetComponent<Bubble>().shellValue * bubbleNumber;
        GetComponent<GameManager>().EndGame();
    }
}
