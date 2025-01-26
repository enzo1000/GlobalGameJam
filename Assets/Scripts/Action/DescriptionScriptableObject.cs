using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct actionDescription
{
    [SerializeField]
    public int vendorIndex;
    public int actionTypeIndex;
    public string actionDescriptionText;
}

[CreateAssetMenu(fileName = "DescriptionScriptableObject", menuName = "Scriptable Objects/DescriptionScriptableObject")]
public class DescriptionScriptableObject : ScriptableObject
{
    [SerializeField]
    public List<actionDescription> actionDescriptionList;
}
