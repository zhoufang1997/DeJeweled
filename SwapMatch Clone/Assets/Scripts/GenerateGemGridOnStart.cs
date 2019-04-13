using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateGemGridOnStart : MonoBehaviour
{

    public int columns;
    public int rows;
    public float columnWidth;
    public float rowHeight;
    public GameObject gemSlotPrefab;

    [HideInInspector] public GameObject[,] gemGrid;
    [HideInInspector] public Vector2 startingPos;
        
    // Start is called before the first frame update
    private void Start()
    {
        startingPos = CalculateStartingPosition();
        GenerateGemSlots();
    }

    private void GenerateGemSlots()
    {
        gemGrid = new GameObject[rows, columns];
        for (var row = 0; row < rows; row++)
        {
            for (var column = 0; column < columns; column++)
            {
                gemGrid[row, column] = Instantiate(gemSlotPrefab, Vector3.zero, Quaternion.identity);
                gemGrid[row, column].name = "GemSlot" + row + "-" + column;
                gemGrid[row, column].transform.SetParent(transform);
                gemGrid[row, column].transform.localPosition = CalculateGemSlotPosition(row, column);
                gemGrid[row, column].transform.localScale = Vector3.one;
                gemGrid[row, column].GetComponentInChildren<GenerateGemOnStart>().center = CheckCenterGemSlot(row, column);
            }
        }
    }

    private Vector2 CalculateStartingPosition()
    {
        float startingPosX;
        if (columns % 2 != 0)
        {
            startingPosX = -((columns - 1) / 2) * columnWidth;
        }
        else
        {
            startingPosX = -(columns / 2) * columnWidth + columnWidth / 2;
        }

        float startingPosY;
        if (rows % 2 != 0)
        {
            startingPosY = ((rows - 1) / 2f) * rowHeight;
        }
        else
        {
            startingPosY = (rows / 2f) * rowHeight - rowHeight / 2;
        }
        return new Vector2(startingPosX, startingPosY);
    }

    private Vector3 CalculateGemSlotPosition(int row, int column)
    {
        var posX = startingPos.x + column * columnWidth;
        var posY = startingPos.y - row * rowHeight;
        return new Vector3(posX, posY, -0.1f);
    }

    private bool CheckCenterGemSlot(int currentRow, int currentColumn)
    {
        bool rowCheck;
        if (rows % 2 != 0)
        {
            rowCheck = currentRow + 1 == ((rows - 1) / 2) + 1;
        }
        else
        {
            rowCheck = currentRow + 1 == rows / 2;
        }

        bool columnCheck;
        if (columns % 2 != 0)
        {
            columnCheck = currentColumn + 1 == ((columns - 1) / 2) + 1;
        }
        else
        {
            columnCheck = currentColumn + 1 == columns / 2;
        }

        return rowCheck && columnCheck;
    }
}
