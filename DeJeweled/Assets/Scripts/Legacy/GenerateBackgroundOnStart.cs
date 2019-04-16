using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateBackgroundOnStart : MonoBehaviour
{
    public GameObject backgroundPrefab;

    private GameObject _background;
    
    // Start is called before the first frame update
    private void Start()
    {
        _background = Instantiate(backgroundPrefab, transform.position, Quaternion.identity);
        _background.name = "Background";
        _background.transform.SetParent(transform);
    }
}
