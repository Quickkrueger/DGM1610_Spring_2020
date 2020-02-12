using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingRod : MonoBehaviour
{
    public ItemScriptableObject fishingRodData;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<MeshFilter>().mesh = fishingRodData.itemMesh;
    }
}
