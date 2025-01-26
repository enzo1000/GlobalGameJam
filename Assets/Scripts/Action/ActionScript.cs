using Mono.Cecil.Cil;
using System.Xml;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ActionScript : MonoBehaviour
{
    //Scripting Only
    public float playerInvested = 0f;   //Number of bubbles invested in the current Action
    private float informationRiskRate = 20f;
    public bool asExplosed = false;
    //Tuple of data struct to identify Vendor
    private (StaticThreshold.Levels, StaticThreshold.Levels) vendorType;
    private int vendorIndex;
    private int actionTypeIndex;

    public ActionScriptableObject ASO;
    public VendorFaceScriptableObject VFSO;
    public ActionTypeScriptableObject ATSO;
    public int ASOIndex = 0;

    //First Panel
    private string actionName;          //Name of the action
    private string actionSellerName;    //SellerName
    private Sprite actionSellerImage;
    private float visibilityCooldown;   //The visibility Cooldown of the Action

    //Second Panel
    private string actionDescription;
    public float currentBubbleValue;
    public float baseBubbleValue;
    private int initialActionStock;
    public float investDanger;          //A percent of chances for the Action to crash

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
        processVendorFace();
        processActionType();
        InitFirstPanel();
        InitSecondPanel();
        InitThirdPanel();
    }

    private void Update()
    {
        //First Panel
        UI_Timer.GetComponent<TMP_Text>().text = visibilityCooldown.ToString();

        //Second Panel
        UI_Timer2.GetComponent<TMP_Text>().text = visibilityCooldown.ToString();
        UI_currentActionCost.GetComponent<TMP_Text>().text = currentBubbleValue.ToString();

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

    private void processVendorFace()
    {
        //investDanger
        float minDanger = ASO.actionParamList[ASOIndex].minInvestDanger;
        float maxDanger = ASO.actionParamList[ASOIndex].maxInvestDanger;
        investDanger = Mathf.RoundToInt(Random.Range(minDanger, maxDanger));

        //baseBubbleValue
        float minBasePrice = ASO.actionParamList[ASOIndex].minInitialActionCost;
        float maxBasePrice = ASO.actionParamList[ASOIndex].maxInitialActionCost;
        baseBubbleValue = Mathf.RoundToInt(Random.Range(minBasePrice, maxBasePrice));

        //Depending of the value of investDanger, assign it's level to vendorType.Item1
        if (investDanger < StaticThreshold.lowInvestDanger)
        {
            vendorType = (StaticThreshold.Levels.low, vendorType.Item2);
        }
        else if (investDanger < StaticThreshold.midInvestDanger)
        {
            vendorType = (StaticThreshold.Levels.medium, vendorType.Item2);
        }
        else
        {
            vendorType = (StaticThreshold.Levels.high, vendorType.Item2);
        }

        //Same but for baseBubbleValue to vendorType.Item2
        if (baseBubbleValue < StaticThreshold.lowActionCostThreshold || vendorType.Item1 == StaticThreshold.Levels.low)
        {
            vendorType = (vendorType.Item1, StaticThreshold.Levels.low);
        }
        else if (baseBubbleValue < StaticThreshold.midActionCostThreshold)
        {
            vendorType = (vendorType.Item1, StaticThreshold.Levels.medium);
        }
        else
        {
            vendorType = (vendorType.Item1, StaticThreshold.Levels.high);
        }
        
        Debug.Log(vendorType.Item1 + " | " + vendorType.Item2);
        //Basic things
        if (vendorType.Item1 == StaticThreshold.Levels.low && vendorType.Item2 == StaticThreshold.Levels.low)
        {
            actionSellerImage = VFSO.vendorFacesList[0].actionSellerImage;
            vendorIndex = 0;
        }
        else if (vendorType.Item1 == StaticThreshold.Levels.medium && vendorType.Item2 == StaticThreshold.Levels.low)
        {
            actionSellerImage = VFSO.vendorFacesList[1].actionSellerImage;
            vendorIndex = 1;
        }
        else if (vendorType.Item1 == StaticThreshold.Levels.medium && vendorType.Item2 == StaticThreshold.Levels.medium)
        {
            actionSellerImage = VFSO.vendorFacesList[2].actionSellerImage;
            vendorIndex = 2;
        }
        else if (vendorType.Item1 == StaticThreshold.Levels.medium && vendorType.Item2 == StaticThreshold.Levels.high)
        {
            actionSellerImage = VFSO.vendorFacesList[3].actionSellerImage;
            vendorIndex = 3;
        }
        else if (vendorType.Item1 == StaticThreshold.Levels.high && vendorType.Item2 == StaticThreshold.Levels.low)
        {
            actionSellerImage = VFSO.vendorFacesList[4].actionSellerImage;
            vendorIndex = 4;
        }
        else if (vendorType.Item1 == StaticThreshold.Levels.high && vendorType.Item2 == StaticThreshold.Levels.medium)
        {
            actionSellerImage = VFSO.vendorFacesList[5].actionSellerImage;
            vendorIndex = 5;
        }
        else if (vendorType.Item1 == StaticThreshold.Levels.high && vendorType.Item2 == StaticThreshold.Levels.high)
        {
            actionSellerImage = VFSO.vendorFacesList[6].actionSellerImage;
            vendorIndex = 6;
        }
    }
    private void processActionType() 
    {
        actionTypeIndex = Mathf.RoundToInt(Random.Range(0, 15));

        if (actionTypeIndex == 0)
        {
            //Inserer l'image associe
        }
        else
        {
            //...
        }
    }

    private void InitFirstPanel()
    {
        //First Panel
        actionName = ASO.actionParamList[ASOIndex].actionName;
        actionSellerName = ASO.actionParamList[ASOIndex].actionSellerName;
        visibilityCooldown = ASO.actionParamList[ASOIndex].visibilityCooldown;

        UI_actionName.GetComponent<TMP_Text>().text = actionName;
        UI_actionSellerName.GetComponent<TMP_Text>().text = actionSellerName;
        UI_actionSellerImage.GetComponent<Image>().sprite = actionSellerImage;
    }
    private void InitSecondPanel()
    {
        //Second Panel
        actionDescription = ASO.actionParamList[ASOIndex].actionDescription;
        initialActionStock = ASO.actionParamList[ASOIndex].initialActionStock;

        UI_actionDescription.GetComponent<TMP_Text>().text = actionDescription;
        UI_actionName2.GetComponent<TMP_Text>().text = actionName;
        currentBubbleValue = baseBubbleValue;
        UI_initialActionStock.GetComponent<TMP_Text>().text = "(" + initialActionStock.ToString() + ")";
        UI_investDanger.GetComponent<TMP_Text>().text = investDanger.ToString() + "%";
    }
    private void InitThirdPanel()
    {
        //Third Panel
        UI_actionName3.GetComponent<TMP_Text>().text = actionName;
        UI_actionSellerImage3.GetComponent<Image>().sprite = actionSellerImage;
        UI_informationRiskRate.GetComponent<TMP_Text>().text = informationRiskRate.ToString() + "%";
    }
    
    //Dedicated to open a custom canva in code
    public void OpenCanvaButton(GameObject canva, bool active)
    {
        canva.SetActive(active);
    }

    //Open communication canva specificali (OnClick function)
    public void OpenCommunicationCanva(GameObject canva)
    {
        canva.SetActive(true);

        // Trouver ActionVariation
        VariationActions actionVariation = FindFirstObjectByType<VariationActions>();


        // Calcul des nouveaux risques
        float newCrashChance = investDanger / 8; // a modifier c'est une valeur random
        float newSlightFluctuationChance = Mathf.Clamp(100f - newCrashChance - 5f, 0f, 100f);

        // Mise à jour des risques pour l'action actuelle
        actionVariation.AdjustActionRisks(actionName, newSlightFluctuationChance, newCrashChance);
        Debug.Log("Communication");

    }

    //To increment / decrement the risk warn value on third panel
    public void IncrementInformationRisk()
    {
        informationRiskRate += 5f;

        if (informationRiskRate > 100)
        {
            informationRiskRate = 100;
        }

        UI_informationRiskRate.GetComponent<TMP_Text>().text = informationRiskRate.ToString() + "%";
    }
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
        Destroy(gameObject);
    }
    public void OnClickIgnoreButton()
    {
        UI_communicationCanva.SetActive(false);
        Debug.Log("Ignore");
        Destroy(gameObject);

    }
}
