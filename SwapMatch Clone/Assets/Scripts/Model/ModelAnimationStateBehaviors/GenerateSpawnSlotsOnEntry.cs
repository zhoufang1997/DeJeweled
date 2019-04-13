using UnityEngine;

namespace Model.ModelAnimationStateBehaviors
{
    public class GenerateSpawnSlotsOnEntry : StateMachineBehaviour
    {
        private static readonly int SpawnSlotsGenerated = Animator.StringToHash("SpawnSlotsGenerated");

        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            var objsTracker = animator.gameObject.GetComponent<ViewObjsTracker>();
            var varsTracker = animator.gameObject.GetComponent<KeyVarsTracker>();

            var startingPosition = CalculateStartingPosition(varsTracker);
            
            GenerateSpawnSlots(objsTracker, varsTracker, startingPosition);

            animator.SetBool(SpawnSlotsGenerated, true);
        }
        
        private void GenerateSpawnSlots(ViewObjsTracker objsTracker, KeyVarsTracker varsTracker, Vector2 startingPosition)
        {
            objsTracker.spawnSlotObjs = new GameObject[varsTracker.gridSize.y,varsTracker.gridSize.x];
            for (var row = 0; row < varsTracker.gridSize.y; row++)
            {
                for (var column = 0; column < varsTracker.gridSize.x; column++)
                {
                    objsTracker.spawnSlotObjs[row, column] = new GameObject("SpawnSlot" + row + "-" + column);
                    objsTracker.spawnSlotObjs[row, column].transform.SetParent(objsTracker.spawnGridObj.transform);
                    objsTracker.spawnSlotObjs[row, column].transform.localPosition =
                        CalculateGemSlotPosition(varsTracker, startingPosition, new Vector2Int(column, row));
                    objsTracker.spawnSlotObjs[row, column].transform.localScale = Vector3.one;
                }
            }
        }
        
        private Vector2 CalculateStartingPosition(KeyVarsTracker varsTracker)
        {
            var gridWidth = varsTracker.gridSize.x * varsTracker.cellSize.x;
            var gridHeight = varsTracker.gridSize.y * varsTracker.cellSize.y;
            var startingX = varsTracker.gridCenter.x - gridWidth / 2 + varsTracker.cellSize.x / 2;
            var startingY = varsTracker.gridCenter.y + gridHeight / 2 - varsTracker.cellSize.y / 2;
            return new Vector2(startingX, startingY);
        }
        
        private Vector2 CalculateGemSlotPosition(KeyVarsTracker varsTracker, Vector2 startingPosition, Vector2Int gridPosition)
        {
            return new Vector2(startingPosition.x + gridPosition.x * varsTracker.cellSize.x,
                startingPosition.y - gridPosition.y * varsTracker.cellSize.y);
        }
    }
}
