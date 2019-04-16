using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AdjustBackgroundSize : MonoBehaviour
{

    public Vector2 referenceSize;
    
    // Start is called before the first frame update
    void Start()
    {
        Vector2 rawMin = Camera.main.ScreenToWorldPoint(Vector3.zero);
        Vector2 rawMax = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height));
        Vector2 rawRange = rawMax - rawMin;
        float scale = rawRange.x / referenceSize.x;
        transform.localScale = new Vector3(scale, scale, 1);
    }
    
}
