using UnityEngine;
using System.Collections.Generic;
using TMPro;


public class ActionVariation : MonoBehaviour
{
    public float interval = 5f; // Intervalle en secondes pour chaque variation
    public List<ActionData> actions = new List<ActionData>(); // Liste des actions

    private DynamicMusicManager musicManager;//Ame pour la musique


    public NewsScriptableObject newsScriptableObject;

    // attention ca risque de casser on veut generer plus news differentes
    public GameObject UI_newsName;
    public GameObject UI_newsDescription;
    public GameObject UI_newsVariation;

    private float timer;

    void Start()
    {
        timer = interval;
        musicManager = FindFirstObjectByType<DynamicMusicManager>();
    }

    void Update()
    {
        timer -= Time.deltaTime;

        // Si le timer atteint 0, g�n�rer des variations pour toutes les actions
        if (timer <= 0f)
        {
            GenerateVariations();
            timer = interval;
        }
    }

    void GenerateVariations()
    {
        foreach (ActionData action in actions)
        {
            float randomValue = Random.Range(0f, 100f); // G�n�rer un nombre al�atoire entre 0 et 100

            if (randomValue <= action.slightFluctuationChance) // Fluctuation l�g�re
            {
                float variation = Random.Range(-action.slightFluctuation, action.slightFluctuation);
                action.currentValue += variation;
                Debug.Log($"[{action.name}] Fluctuation l�g�re : {variation} -> Nouvelle valeur : {action.currentValue}");
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
            else // Mont�e en fl�che
            {
                action.currentValue *= action.surgeFactor;
                Debug.Log($"[{action.name}] Mont�e en fl�che ! Nouvelle valeur : {action.currentValue}");
                if (musicManager != null)
                {
                    musicManager.PlayGoodVariation(); // Play good news variation
                }
                SetNews("Mont�e en fl�che", action.surgeFactor);
            }
        }
    }




    public void SetNews(string actionName, float newsVariation)
    {
        foreach (var news in newsScriptableObject.newsParamList)
        {
            if (news.newsName == actionName)
            {
                UI_newsName.GetComponent<TMP_Text>().text = actionName;
                UI_newsDescription.GetComponent<TMP_Text>().text = news.newsDescription;
                UI_newsVariation.GetComponent<TMP_Text>().text = newsVariation.ToString();

                Debug.Log($"News : {actionName} - {news.newsDescription} - {newsVariation}");
                return;
            }
        }
        //test de secours a enlever
        UI_newsName.GetComponent<TMP_Text>().text = actionName;
        UI_newsDescription.GetComponent<TMP_Text>().text = "aaaaaaaaaaa";
        UI_newsVariation.GetComponent<TMP_Text>().text = newsVariation.ToString();
        Debug.Log($"News : {actionName} - {"aaaaaaaaa"} - {newsVariation}");
    }
}
