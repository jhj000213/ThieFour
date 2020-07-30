using UnityEngine;
using System.Collections;

public class SortingLayerSet : MonoBehaviour {

    public MeshRenderer _MyMeshRenderer;

    void Start()
    {
        _MyMeshRenderer.sortingOrder = 4000;
        _MyMeshRenderer.sharedMaterial.renderQueue = 4000;
    }   
    
}
