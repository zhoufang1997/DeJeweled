using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;

public class GenerateGemOnStart : MonoBehaviour
{

    [HideInInspector] public bool center;

    public GameObject playerPrefab;
    public GameObject gemPrefab;
    
    // Start is called before the first frame update
    void Start()
    {
        var gem = Instantiate(center ? playerPrefab : gemPrefab, Vector3.zero, Quaternion.identity);
        gem.transform.SetParent(transform);
        gem.transform.localPosition = new Vector3(0,0,-0.1f);
        gem.transform.localScale = Vector3.one;
    }
    
}
