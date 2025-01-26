using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct actionParam
{
    //First Panel Information
    [SerializeField]
    public string actionName;
    [SerializeField]
    public Sprite actionSellerImage;
    public string actionSellerName;
    public float visibilityCooldown;

    //SecondPanel Information
    public string actionDescription;
    public float minInitialActionCost;
    public float maxInitialActionCost;
    public int initialActionStock;
    public float minInvestDanger;
    public float maxInvestDanger;
}

[CreateAssetMenu(fileName = "ActionScriptableObject", menuName = "Scriptable Objects/ActionScriptableObject")]
public class ActionScriptableObject : ScriptableObject
{
    [SerializeField]
    public List<actionParam> actionParamList;
}