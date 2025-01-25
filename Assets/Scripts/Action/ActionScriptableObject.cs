using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct actionParam
{
    [SerializeField]
    public string actionName;
    [SerializeField]
    public Sprite actionSellerImage;
    public string actionSellerName;
    public float baseBubbleValue;         //Base cost of the Action for first appearance and futur variation
    public float visibilityCooldown;      //The visibility Cooldown of the Action 
    public int availableQuantity;         //Numbers of action buyable
    public float minThreshold;            //???
    public float maxThreshold;            //???
    public float speculativeBubbleChance;
}

[CreateAssetMenu(fileName = "ActionScriptableObject", menuName = "Scriptable Objects/ActionScriptableObject")]
public class ActionScriptableObject : ScriptableObject
{
    [SerializeField]
    public List<actionParam> actionParamList;
}