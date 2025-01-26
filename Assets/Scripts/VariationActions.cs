using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;


public class VariationActions : MonoBehaviour
{
    public float interval = 4f; // Intervalle en secondes pour chaque variation
    public List<ActionData> actions = new List<ActionData>(); // Liste des actions

    private DynamicMusicManager musicManager;//Ame pour la musique


    public NewsScriptableObject newsScriptableObject;

    // attention ca risque de casser on veut generer plus news differentes
    public GameObject UI_newsName;
    public GameObject UI_newsDescription;
    public GameObject UI_newsVariation;

    public GameObject UI_news;
    private List<newsParam> newsToShow = new List<newsParam>();

    private float timer;

    void Start()
    {
        timer = interval;
        musicManager = FindFirstObjectByType<DynamicMusicManager>();
    }

    void Update()
    {
        timer -= Time.deltaTime;

        // Si le timer atteint 0, générer des variations pour toutes les actions
        if (timer <= 0f)
        {
            GenerateVariations();
            timer = interval;
        }
    }

    enum operation {plus, fois}

    void updatePlayerActions(string name, float variation, operation op)
    {
        PlayerManager playerManager = GameObject.Find("testmanager").GetComponent<PlayerManager>();
        
        foreach(var action in playerManager.actions)
        {
            if(action.Key.actionName == name)
            {
                
                switch (op)
                {
                    case operation.plus:
                        variation *= action.Key.initialActionStock;
                        action.Key.currentBubbleValue += variation;

                        break;
                    case operation.fois:
                        action.Key.currentBubbleValue *= variation;
                        break;

                    default: break;
                }
                
            }
        }
    }

    void GenerateVariations()
    {
        foreach (ActionData action in actions)
        {
            float randomValue = Random.Range(0f, 100f); // Générer un nombre aléatoire entre 0 et 100

            if (randomValue <= action.slightFluctuationChance) // Fluctuation légère
            {
                float variation = Random.Range(-(int)action.slightFluctuation, (int)action.slightFluctuation);
                action.currentValue += variation;
                Debug.Log($"[{action.name}] Fluctuation légère : {variation} -> Nouvelle valeur : {action.currentValue}");
            }
            else if (randomValue <= action.slightFluctuationChance + action.crashChance) // Chute/crash
            {
                action.currentValue *= action.crashFactor;
                Debug.Log($"[{action.name}] Chute/crash ! Nouvelle valeur : {action.currentValue}");
                if (musicManager != null)
                {
                    musicManager.PlayBadVariation(); // Play bad news variation
                }
                SetNews("Chute/crash", action.crashFactor);
            }
            else // Montée en flèche
            {
                action.currentValue *= action.surgeFactor;
                Debug.Log($"[{action.name}] Montée en flèche ! Nouvelle valeur : {action.currentValue}");
                if (musicManager != null)
                {
                    musicManager.PlayGoodVariation(); // Play good news variation
                }
                SetNews("Montée en flèche", action.surgeFactor);
            }
        }
    }


    public void SetNews(string actionName, float newsVariation)
    {
        Debug.Log("NEEEEEEEWS" + actionName);


        foreach (var news in newsScriptableObject.newsParamList)
        {
            if (news.newsName == actionName)
            {
                if (newsToShow.Count >= 5)
                    newsToShow.RemoveAt(0);

                newsToShow.Add(news);

                UpdateNewsUI();
                return;
            }
        }
    }

    private void UpdateNewsUI()
    {
        List<GameObject> GO_UInews = new List<GameObject>();
        foreach(Transform t in UI_news.transform)
        {
            GO_UInews.Add(t.gameObject);
        }

        for(int i = 0; i < GO_UInews.Count; i++)
        {
            if(newsToShow.Count > i)
            {
                GO_UInews[i].GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 0.1f);
                TMP_Text[] texts = GO_UInews[i].GetComponentsInChildren<TMP_Text>();
                texts[0].text = newsToShow[i].newsName;
                texts[1].text = newsToShow[i].newsDescription;
                // texts[2].text = newsToShow[i].newsVariation.ToString();
            }
            else
            {
                TMP_Text[] texts = GO_UInews[i].GetComponentsInChildren<TMP_Text>();
                texts[0].text = "";
                texts[1].text = "";
                texts[2].text = "";
            }
        }
    }

    public void AdjustActionRisks(string actionName, float newSlightFluctuationChance, float newCrashChance)
    {
        foreach (ActionData action in actions)
        {
            if (action.name == actionName)
            {
                // Ajuster les chances
                action.slightFluctuationChance = newSlightFluctuationChance;
                action.crashChance = newCrashChance;

                Debug.Log($"Risques mis à jour pour {actionName}:");
                Debug.Log($"Fluctuation légère: {action.slightFluctuationChance}%");
                Debug.Log($"Crash: {action.crashChance}%");
                return;
            }
        }

        Debug.LogWarning($"Action {actionName} non trouvée pour ajustement des risques.");
    }
}
