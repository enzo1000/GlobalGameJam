using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct vendorFaces
{
    [SerializeField]
    public Sprite actionSellerImage;
}

[CreateAssetMenu(fileName = "VendorFaceScriptableObject", menuName = "Scriptable Objects/VendorFaceScriptableObject")]
public class VendorFaceScriptableObject : ScriptableObject
{
    [SerializeField]
    public List<vendorFaces> vendorFacesList;
}
