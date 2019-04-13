using System.Collections;
using System.Collections.Generic;
using Boo.Lang.Environments;
using UnityEngine;

public class RandomColorPicker : MonoBehaviour
{
    public GameObject redGemPrefab;
    public GameObject yellowGemPrefab;
    public GameObject greenGemPrefab;
    public GameObject cyanGemPrefab;
    public GameObject blueGemPrefab;
    public GameObject magentaPrefab;
    
    // Start is called before the first frame update
    void Start()
    {
        PickRandomColorGem();
    }

    private void PickRandomColorGem()
    {
        var randomNumber = Random.Range(0, 60);
        if (randomNumber < 10)
        {
            InstantiateGem(redGemPrefab);
        }
        else if (randomNumber >= 10 && randomNumber < 20)
        {
            InstantiateGem(yellowGemPrefab);
        }
        else if (randomNumber >= 20 && randomNumber < 30)
        {
            InstantiateGem(greenGemPrefab);
        }
        else if (randomNumber >= 30 && randomNumber < 40)
        {
            InstantiateGem(cyanGemPrefab);
        }
        else if (randomNumber >= 40 && randomNumber < 50)
        {
            InstantiateGem(blueGemPrefab);
        }
        else
        {
            InstantiateGem(magentaPrefab);
        }
    }

    private void InstantiateGem(GameObject prefab)
    {
        GameObject gemSprite = Instantiate(prefab, Vector3.zero, Quaternion.identity);
        gemSprite.transform.SetParent(transform);
        gemSprite.transform.localPosition = Vector3.zero;
        gemSprite.transform.localScale = Vector3.one;
    }
}
