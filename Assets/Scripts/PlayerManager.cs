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
    private Dictionary<ActionScript, int> actions = new Dictionary<ActionScript, int>();
    private ActionScript selectedAction;

    private void Start()
    {
        shellNumber = .0f;
        bubbleNumber = 500.0f;
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

    public void OnSellAction(ActionScript action, int quantity)
    {
        if (!actions.ContainsKey(action) || actions[action] < quantity)
            return;

        bubbleNumber += quantity * action.currentBubbleValue;
        actions[action] -= quantity;
        if (actions[action] <= 0)
            actions.Remove(action);
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
}
