using UnityEngine;
using System.Collections.Generic;


public class ActionVariation : MonoBehaviour
{
    public float interval = 5f; // Intervalle en secondes pour chaque variation
    public List<ActionData> actions = new List<ActionData>(); // Liste des actions

    private DynamicMusicManager musicManager;//Ame pour la musique

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

    void GenerateVariations()
    {
        foreach (ActionData action in actions)
        {
            float randomValue = Random.Range(0f, 100f); // Générer un nombre aléatoire entre 0 et 100

            if (randomValue <= action.slightFluctuationChance) // Fluctuation légère
            {
                float variation = Random.Range(-action.slightFluctuation, action.slightFluctuation);
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
            }
            else // Montée en flèche
            {
                action.currentValue *= action.surgeFactor;
                Debug.Log($"[{action.name}] Montée en flèche ! Nouvelle valeur : {action.currentValue}");
                if (musicManager != null)
                {
                    musicManager.PlayGoodVariation(); // Play good news variation
                }
            }
        }
    }
}
