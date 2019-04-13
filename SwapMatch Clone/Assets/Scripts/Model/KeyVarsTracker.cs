using UnityEngine;

namespace Model
{
    public class KeyVarsTracker : MonoBehaviour
    {

        public Vector3 gridCenter;
        public Vector2Int gridSize;
        public Vector2 cellSize;

        [HideInInspector] public float backgroundScale;

        [HideInInspector] public bool[,] emptyGemSlots;
    }
}
