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
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        EnterActionWindow();
    }

    void EnterActionWindow()
    {
        List<GameObject> childrensPanel = new List<GameObject>();

        foreach (Transform child in actionListPanel.transform) 
        {
            childrensPanel.Add(child.gameObject);
        }

        for(int i = 0; i < childrensPanel.Count; i++)
        {
            Debug.Log(childrensPanel[i].name);
        }

        for (int i = 0; i < playerManager.actions.Count; i++)
        {
            string name, quantity;
            TMP_Text actionName = childrensPanel[i].GetComponentInChildren<TMP_Text>();
            name = playerManager.actions.ElementAt(i).Key.actionName;
            quantity = playerManager.actions.ElementAt(i).Value.ToString();
            actionName.text = name + "(" + quantity + ")";
            childrensPanel[i].SetActive(true);
            int index = i;
            childrensPanel[i].GetComponent<Button>().onClick.AddListener(()=>ShowDetails(index, playerManager));
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
            currentActionTMP.text = playerManager.actions.ElementAt(actionIndex).Key.initialActionStock.ToString();
            purchaseShareActionTMP.text = "Purchase Shares : $" + playerManager.actions.ElementAt(actionIndex).Key.baseBubbleValue.ToString();
            currentValueActionTMP.text = "Current Value Shares : $" + playerManager.actions.ElementAt(actionIndex).Key.currentBubbleValue.ToString();

            panel.SetActive(true);


        }
    }

    private void Update()
    {
        //Recupere variation pour changer pourcentage
    }
}
