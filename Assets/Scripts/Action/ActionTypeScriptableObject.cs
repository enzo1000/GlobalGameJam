using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct actionType
{
    [SerializeField]
    public Sprite actionSpriteType;
}

[CreateAssetMenu(fileName = "ActionTypeScriptableObject", menuName = "Scriptable Objects/ActionTypeScriptableObject")]
public class ActionTypeScriptableObject : ScriptableObject
{
    [SerializeField]
    public List<actionType> actionTypeList;
}
