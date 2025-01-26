using UnityEngine;

[System.Serializable] // Rend la classe sérialisable pour l'Inspector
public class ActionData
{
    public string name; // Nom de l'action
    public float currentValue = 100f; // Valeur actuelle de l'action
    public float slightFluctuation = 1f; // Amplitude de fluctuation légère
    public float crashFactor = 0.5f; // Facteur pour une chute/crash
    public float surgeFactor = 1.2f; // Facteur pour une montée en flèche

    // Pourcentages de chances (ils doivent totaliser 100%)
    [Range(0, 100)] public float slightFluctuationChance = 90f; // Chance de fluctuation légère
    [Range(0, 100)] public float crashChance = 7f; // Chance de chute/crash
    //[Range(0, 100)] public float surgeChance = 3f; // Chance de montée en flèche   sert a rien
}