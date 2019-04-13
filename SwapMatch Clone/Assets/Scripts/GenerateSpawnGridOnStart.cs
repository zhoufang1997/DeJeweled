using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateSpawnGridOnStart : MonoBehaviour
{
    public GameObject spawnSlotPrefab;
    public GenerateGemGridOnStart generateGemGridOnStartObj;
    
    [HideInInspector] public GameObject[] spawnGrid;

    private Vector2 _startingPos;
    
    
    // Start is called before the first frame update
    private void Start()
    {
        _startingPos = CalculateStartingPosition();
        GenerateSpawnSlots();
    }

    private void GenerateSpawnSlots()
    {
        spawnGrid = new GameObject[generateGemGridOnStartObj.columns];
        for (var column = 0; column < generateGemGridOnStartObj.columns; column++)
        {
            spawnGrid[column] = Instantiate(spawnSlotPrefab, Vector3.zero, Quaternion.identity);
            spawnGrid[column].name = "SpawnSlot" + column;
            spawnGrid[column].transform.SetParent(transform);
            spawnGrid[column].transform.localPosition = CalculateSpawnSlotPosition(column);
            spawnGrid[column].transform.localScale = Vector3.one;
            spawnGrid[column].GetComponent<GenerateGemOnEmpty>().generateGemGridOnStartObj = generateGemGridOnStartObj;
        }
    }

    private Vector3 CalculateSpawnSlotPosition(int column)
    {
        var posX = _startingPos.x + column * generateGemGridOnStartObj.columnWidth;
        return new Vector3(posX, _startingPos.y, -0.1f);
    }

    private Vector2 CalculateStartingPosition()
    {
        var startingPosX = generateGemGridOnStartObj.startingPos.x;
        var startingPosY = 4.5f + generateGemGridOnStartObj.rowHeight / 2;
        return new Vector2(startingPosX, startingPosY);
    }
}
