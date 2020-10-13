using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortingLayers : MonoBehaviour {
    public string sortLayerName;

    private void Start()
    {
        foreach(SpriteRenderer sr in GetComponentsInChildren<SpriteRenderer>())
        {
            sr.GetComponent<Renderer>().sortingLayerName = sortLayerName;
        }
    }
}
