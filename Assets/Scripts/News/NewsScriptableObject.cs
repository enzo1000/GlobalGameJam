using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct newsParam
{
    [SerializeField]
    public string newsName; //News name
    public string newsDescription; //Description of the news
    public float newsVariation;
}

[CreateAssetMenu(fileName = "NewsScriptableObject", menuName = "Scriptable Objects/NewsScriptableObject")]
public class NewsScriptableObject : ScriptableObject
{
    [SerializeField]
    public List<newsParam> newsParamList;
}
