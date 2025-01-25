using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ActionScript : MonoBehaviour
{
    //Scripting Only
    public float playerInvested = 0;   //Number of bubbles invested in the current Action
    public float currentBubbleValue;   //Current cost of the Action with the variations of the Bubbles
    private float globalVariation;      //Variation of the action depending of the baseBubbleValue
    public bool asExplosed = false;    //Is the action crashing or not

    public ActionScriptableObject ASO;
    public int ASOIndex = 0;

    //ASO Assignement
    private string actionName;             //Name of the action
    private string actionSellerName;           //SellerName
    private Sprite actionSellerImage;

    public float baseBubbleValue;         //Base cost of the Action for first appearance and futur variation
    private float visibilityCooldown;      //The visibility Cooldown of the Action 
    private float minThreshold;            //???
    private float maxThreshold;            //???
    public float speculativeBubbleChance; //A percent of chances for the Action to crash

    //UI
    public GameObject UI_actionName;
    public GameObject UI_actionSellerName;
    public GameObject UI_actionSellerImage;
    public GameObject UI_Timer;

    public GameObject UI_detailedCanva;

    private void Start()
    {
        actionName = ASO.actionParamList[ASOIndex].actionName;
        actionSellerName = ASO.actionParamList[ASOIndex].actionSellerName;
        actionSellerImage = ASO.actionParamList[ASOIndex].actionSellerImage;
        visibilityCooldown = ASO.actionParamList[ASOIndex].visibilityCooldown;    //Not displayed

        UI_actionName.GetComponent<TMP_Text>().text = actionName;
        UI_actionSellerName.GetComponent<TMP_Text>().text = actionSellerName;
        UI_actionSellerImage.GetComponent<Image>().sprite = actionSellerImage;

        /*baseBubbleValue = ASO.actionParamList[ASOIndex].baseBubbleValue;
        availableQuantity = ASO.actionParamList[ASOIndex].availableQuantity;
        minThreshold = ASO.actionParamList[ASOIndex].minThreshold;
        maxThreshold = ASO.actionParamList[ASOIndex].maxThreshold;
        speculativeBubbleChance = ASO.actionParamList[ASOIndex].speculativeBubbleChance;*/
    }

    private void Update()
    {
        UI_Timer.GetComponent<TMP_Text>().text = visibilityCooldown.ToString();

        if (UI_actionName.transform.parent.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Pressed"))
        {
            OpenCanvaButton(UI_detailedCanva, true);
        }
        else if (!UI_actionName.transform.parent.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Selected"))
        {
            OpenCanvaButton(UI_detailedCanva, false);
        }
    }

    public void OpenCanvaButton(GameObject canva, bool active)
    {
        canva.SetActive(active);
    }

    public void UpdateBubbleValue(float variation)
    {
        currentBubbleValue = currentBubbleValue + (currentBubbleValue * variation);
        UpdateSpeculativeBubbleChance();
    }

    // [TODO] à équilibrer
    public void UpdateSpeculativeBubbleChance()
    {

    }
}
