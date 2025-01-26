using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using TMPro;
using System.Linq;
using UnityEngine.UI;

public class ActionUse : MonoBehaviour
{
    public PlayerManager playerManager;
    public GameObject actionListPanel;
    private ActionScript action;
    private int numberOfAction;
    public GameObject panel;
    public GameObject currentAction;
    public GameObject puchaseShareAction;
    public GameObject currentValueAction;
    public GameObject totalSellingActions;
    public int numberToSell = 0;
    public int currentWindowAction = 0;
    public TMP_Text totalSellActionTMP;
    List<GameObject> childrensPanel = new List<GameObject>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        totalSellActionTMP = totalSellingActions.GetComponentInChildren<TMP_Text>();
    }

    private void Update()
    {
        EnterActionWindow();
        totalSellActionTMP.text = numberToSell.ToString();
    }

    void EnterActionWindow()
    {
        childrensPanel.Clear();
        foreach (Transform child in actionListPanel.transform) 
        {
            childrensPanel.Add(child.gameObject);
        }

        Debug.Log("Player Manager count : " + playerManager.actions.Count);
        Debug.Log("Children Panel count : " + playerManager.actions.Count);

        for (int i = 0; i < childrensPanel.Count; i++)
        {
            Debug.Log("i : " + i);
            if(playerManager.actions.Count > i)
            {
                Debug.Log("Je suis dans le if");
                string name, quantity;
                TMP_Text actionName = childrensPanel[i].GetComponentInChildren<TMP_Text>();
                name = playerManager.actions.ElementAt(i).Key.actionName;
                quantity = playerManager.actions.ElementAt(i).Value.ToString();
                actionName.text = name + "(" + quantity + ")";
                childrensPanel[i].SetActive(true);
                int index = i;
                childrensPanel[i].GetComponent<Button>().onClick.AddListener(() => ShowDetails(index, playerManager));
            }
            else
            {
                Debug.Log("Je suis dans le else");
                childrensPanel[i].SetActive(false);
            }
        }
    }
    public void ShowDetails(int actionIndex, PlayerManager playerManager)
    {
        if(panel != null)
        {
            //string initialActionStock = actionScript.initialActionStock.ToString();
            TMP_Text currentActionTMP = currentAction.GetComponentInChildren<TMP_Text>();
            TMP_Text purchaseShareActionTMP = puchaseShareAction.GetComponentInChildren<TMP_Text>();
            TMP_Text currentValueActionTMP = currentValueAction.GetComponentInChildren<TMP_Text>();
            currentActionTMP.text = playerManager.actions.ElementAt(actionIndex).Value.ToString();
            purchaseShareActionTMP.text = "Purchase Shares : $" + playerManager.actions.ElementAt(actionIndex).Key.baseBubbleValue.ToString();
            currentValueActionTMP.text = "Current Value Shares : $" + playerManager.actions.ElementAt(actionIndex).Key.currentBubbleValue.ToString();
            numberToSell = 0;
            currentWindowAction = actionIndex;
            panel.SetActive(true);


        }
    }

    public void OnClickIncrementSellAmount()
    {
        numberToSell += 1;

        if (numberToSell > playerManager.actions.ElementAt(currentWindowAction).Key.initialActionStock)
        {
            numberToSell = playerManager.actions.ElementAt(currentWindowAction).Key.initialActionStock;
        }
    }
    public void OnClickBigIncrementSellAmount()
    {
        numberToSell += 10;

        if (numberToSell > playerManager.actions.ElementAt(currentWindowAction).Key.initialActionStock)
        {
            numberToSell = playerManager.actions.ElementAt(currentWindowAction).Key.initialActionStock;
        }
    }
    public void OnClickDecrementSellAmount()
    {
        numberToSell -= 1;

        if (numberToSell < 0)
        {
            numberToSell = 0;
        }
    }
    public void OnClickBigDecrementSellAmount()
    {
        numberToSell -= 10;

        if (numberToSell < 0)
        {
            numberToSell = 0;
        }
    }
    public void OnClickMaxIncrementSellAmount()
    {
        numberToSell = playerManager.actions.ElementAt(currentWindowAction).Value;
    }
    public void OnClickMinIncrementSellAmount()
    {
        numberToSell = 0;
    }

    public void OnClickSell() 
    {
        int leftQuantity;
        leftQuantity = playerManager.OnSellAction(playerManager.actions.ElementAt(currentWindowAction).Key, numberToSell);

        if (leftQuantity == 0)
        {
            panel.SetActive(false);
        }
        else
            ShowDetails(currentWindowAction, playerManager);
    }
}
