using System.Xml;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ActionScript : MonoBehaviour
{
    //Scripting Only
    private float playerInvested = 0;   //Number of bubbles invested in the current Action
    private float informationRiskRate = 20f;

    public ActionScriptableObject ASO;
    public int ASOIndex = 0;

    //ASO Assignement
    private float initialActionCost;    //Base cost of the Action for first appearance and futur variation

    //First Panel
    private string actionName;          //Name of the action
    private string actionSellerName;    //SellerName
    private Sprite actionSellerImage;
    private float visibilityCooldown;   //The visibility Cooldown of the Action

    //Second Panel
    private string actionDescription;
    private int initialActionStock;
    private float investDanger;         //A percent of chances for the Action to crash

    //UI
    [Header("First Panel")]
    public GameObject UI_actionName;
    public GameObject UI_actionSellerName;
    public GameObject UI_actionSellerImage;
    public GameObject UI_Timer;

    [Header("Second Panel")]
    public GameObject UI_detailedCanva;
    public GameObject UI_actionName2;
    public GameObject UI_Timer2;
    public GameObject UI_actionDescription;
    public GameObject UI_currentActionCost;
    public GameObject UI_initialActionStock;
    public GameObject UI_investDanger;

    [Header("Third Panel")]
    public GameObject UI_communicationCanva;
    public GameObject UI_actionName3;
    public GameObject UI_Timer3;
    public GameObject UI_actionSellerImage3;
    public GameObject UI_informationRiskRate;

    private void Start()
    {
        //First Panel
        actionName = ASO.actionParamList[ASOIndex].actionName;
        actionSellerName = ASO.actionParamList[ASOIndex].actionSellerName;
        actionSellerImage = ASO.actionParamList[ASOIndex].actionSellerImage;
        visibilityCooldown = ASO.actionParamList[ASOIndex].visibilityCooldown;

        UI_actionName.GetComponent<TMP_Text>().text = actionName;
        UI_actionSellerName.GetComponent<TMP_Text>().text = actionSellerName;
        UI_actionSellerImage.GetComponent<Image>().sprite = actionSellerImage;

        //Second Panel
        actionDescription = ASO.actionParamList[ASOIndex].actionDescription;
        initialActionCost = ASO.actionParamList[ASOIndex].initialActionCost;
        initialActionStock = ASO.actionParamList[ASOIndex].initialActionStock;
        investDanger = ASO.actionParamList[ASOIndex].investDanger;

        UI_actionDescription.GetComponent<TMP_Text>().text = actionDescription;
        UI_actionName2.GetComponent<TMP_Text>().text = actionName;
        UI_currentActionCost.GetComponent<TMP_Text>().text = initialActionCost.ToString();
        UI_initialActionStock.GetComponent<TMP_Text>().text = "(" + initialActionStock.ToString() + ")";
        UI_investDanger.GetComponent<TMP_Text>().text = investDanger.ToString() + "%";

        //Third Panel
        UI_actionName3.GetComponent<TMP_Text>().text = actionName;
        UI_actionSellerImage3.GetComponent<Image>().sprite = actionSellerImage;
        UI_informationRiskRate.GetComponent<TMP_Text>().text = informationRiskRate.ToString() + "%";
    }

    private void Update()
    {
        //First Panel
        UI_Timer.GetComponent<TMP_Text>().text = visibilityCooldown.ToString();

        //Second Panel
        UI_Timer2.GetComponent<TMP_Text>().text = visibilityCooldown.ToString();

        //Third Panel
        UI_Timer3.GetComponent<TMP_Text>().text = visibilityCooldown.ToString();

        //Navigation behavior
        if (UI_actionName.transform.parent.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Selected"))
        {
            OpenCanvaButton(UI_detailedCanva, true);
        }
        else if (!UI_actionName.transform.parent.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Selected"))
        {
            OpenCanvaButton(UI_detailedCanva, false);
        }
    }

    //Dedicated to open a custom canva in code
    public void OpenCanvaButton(GameObject canva, bool active)
    {
        canva.SetActive(active);
    }

    //Open communication canva spicicali (OnClick function)
    public void OpenCommunicationCanva(GameObject canva)
    {
        canva.SetActive(true);
    }

    //To increment the risk warn value on third panel
    public void IncrementInformationRisk()
    {
        informationRiskRate += 5f;

        if (informationRiskRate > 100)
        {
            informationRiskRate = 100;
        }

        UI_informationRiskRate.GetComponent<TMP_Text>().text = informationRiskRate.ToString() + "%";
    }

    //To decrement the risk warn value on third panel
    public void DecrementInformationRisk()
    {
        informationRiskRate -= 5f;

        if (informationRiskRate < 0)
        {
            informationRiskRate = 0;
        }

        UI_informationRiskRate.GetComponent<TMP_Text>().text = informationRiskRate.ToString() + "%";
    }

    public void OnClickCommunicateButton()
    {
        UI_communicationCanva.SetActive(false);
        Debug.Log("Communication");
    }

    public void OnClickIgnoreButton()
    {
        UI_communicationCanva.SetActive(false);
        Debug.Log("Ignore");
    }
}
