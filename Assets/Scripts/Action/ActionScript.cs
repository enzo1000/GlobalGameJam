using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ActionScript : MonoBehaviour
{
    //Scripting Only
    private float playerInvested = 0;   //Number of bubbles invested in the current Action
    private float currentBubbleValue;   //Current cost of the Action with the variations of the Bubbles
    private float globalVariation;      //Variation of the action depending of the baseBubbleValue
    private bool asExplosed = false;    //Is the action crashing or not

    public ActionScriptableObject ASO;

    //ASO Assignement
    private string actionName;             //Name of the action
    private string actionSeller;           //SellerName
    private float baseBubbleValue;         //Base cost of the Action for first appearance and futur variation
    private float visibilityCooldown;      //The visibility Cooldown of the Action 
    private int availableQuantity;         //Numbers of action buyable
    private float minThreshold;           //???
    private float maxThreshold;           //???
    private float speculativeBubbleChance; //A percent of chances for the Action to crash

    //UI
    public GameObject UI_actionName;
    public GameObject UI_actionSeller;
    public GameObject UI_PSV;
    public GameObject UI_Quantity;

    private void Start()
    {
       actionName = ASO.actionParamList[0].actionName;
        actionSeller = ASO.actionParamList[0].actionSeller;
        baseBubbleValue = ASO.actionParamList[0].baseBubbleValue;
        visibilityCooldown = ASO.actionParamList[0].visibilityCooldown;    //Not displayed
        availableQuantity = ASO.actionParamList[0].availableQuantity;
        minThreshold = ASO.actionParamList[0].minThreshold;
        maxThreshold = ASO.actionParamList[0].maxThreshold;
        speculativeBubbleChance = ASO.actionParamList[0].speculativeBubbleChance;

        UI_actionName.GetComponent<TMP_Text>().text = actionName;
        UI_actionSeller.GetComponent<TMP_Text>().text = actionSeller;
        UI_PSV.GetComponent<TMP_Text>().text = baseBubbleValue.ToString();

        //GameObject.Find("ActionName").GetComponent<Text>().text = actionName;
    }

    private void Update()
    {
        
    }
}
