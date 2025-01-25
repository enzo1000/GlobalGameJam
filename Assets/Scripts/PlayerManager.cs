using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerManager : MonoBehaviour
{
    public float shellNumber { get; set; }
    private float bubbleNumber;
    private List<Follower> followers = new List<Follower>();
    // private Dictionary<Action, int> actions = new Dictionary<Action, int>();
    private int selectedAction;

    private void Start()
    {
        shellNumber = .0f;
        bubbleNumber = 500.0f;
    }

    /*public void OnSwitchSelectedAction(Action newAction)
    {
        selectedAction = newAction;
    }*/

    /*public void OnBuyAction(Action action, int quantity)
    {
        if (quantity * action.currentBubbleValue < bubbleNumber)
        {
            if (actions.ContainsKey(action))
                actions[action] += quantity;
            else
                actions.Add(action, quantity);
               
            action.playerInvest += quantity * action.currentBubbleValue;
            action.availableQuantity -= quantity;
        }
    }*/

    /*public void OnSellAction(Action action, int quantity)
    {
        bubbleNumber += quantity * action.currentBubbleValue;
        actions.Remove(action);
    }*/

    public void OnCreatePost()
    {

    }
}
