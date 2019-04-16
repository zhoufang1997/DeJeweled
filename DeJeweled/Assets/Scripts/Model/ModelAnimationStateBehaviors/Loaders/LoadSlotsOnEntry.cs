using UnityEngine;

namespace Model.ModelAnimationStateBehaviors.Loaders
{
    public class LoadSlotsOnEntry : StateMachineBehaviour
    {
        
        public enum SlotType {GemSlot, SpawnSlot, KillSlot}

        public SlotType currentSlotType;
        
        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            var objsTracker = animator.gameObject.GetComponent<ViewObjsTracker>();
            var varsTracker = animator.gameObject.GetComponent<KeyVarsTracker>();
            var prefTracker = animator.gameObject.GetComponent<PrefabsTracker>();

            LoadSlots(currentSlotType, CalculateStartingPosition(varsTracker), varsTracker, objsTracker, prefTracker);
            
        }

        private static void LoadSlots(SlotType slotType, Vector2 startingPosition, KeyVarsTracker varsTracker, ViewObjsTracker objsTracker, PrefabsTracker prefTracker)
        {
            var slots = new GameObject[varsTracker.gridSize.y, varsTracker.gridSize.x];
            for (var row = 0; row < varsTracker.gridSize.y; row++)
            {
                for (var column = 0; column < varsTracker.gridSize.x; column++)
                {
                    switch (slotType)
                    {
                        case SlotType.GemSlot:
                            slots[row, column] = Instantiate(prefTracker.gemSlotPrefab, objsTracker.gemGridObj.transform, false);
                            slots[row, column].name = "GemSlot" + row + "-" + column;
                            break;
                        case SlotType.SpawnSlot:
                            slots[row, column] = new GameObject();
                            slots[row, column].transform.SetParent(objsTracker.spawnGridObj.transform, false);
                            slots[row, column].name = "SpawnSlot" + row + "-" + column;
                            break;
                        default:
                            slots[row, column] = new GameObject();
                            slots[row, column].transform.SetParent(objsTracker.killGridObj.transform, false);
                            slots[row, column].name = "KillSlot" + row + "-" + column;
                            break;
                    }
                    
                    slots[row, column].transform.localPosition =
                        CalculateSlotPosition(varsTracker, startingPosition, new Vector2Int(column, row));
                    slots[row, column].transform.localScale = Vector3.one;
                }
            }

            switch (slotType)
            {
                case SlotType.GemSlot:
                    objsTracker.gemSlotObjs = slots;
                    objsTracker.gemOBjs = new GameObject[varsTracker.gridSize.y, varsTracker.gridSize.x];
                    break;
                case SlotType.SpawnSlot:
                    objsTracker.spawnSlotObjs = slots;
                    objsTracker.spawnGemObjs = new GameObject[varsTracker.gridSize.y, varsTracker.gridSize.x];
                    break;
                default:
                    objsTracker.killSlotObjs = slots;
                    objsTracker.killGemObjs = new GameObject[varsTracker.gridSize.y, varsTracker.gridSize.x];
                    break;
            }
        }
        
        private static Vector2 CalculateStartingPosition(KeyVarsTracker varsTracker)
        {
            var gridWidth = varsTracker.gridSize.x * varsTracker.cellSize.x;
            var gridHeight = varsTracker.gridSize.y * varsTracker.cellSize.y;
            var startingX = varsTracker.gridCenter.x - gridWidth / 2 + varsTracker.cellSize.x / 2;
            var startingY = varsTracker.gridCenter.y + gridHeight / 2 - varsTracker.cellSize.y / 2;
            return new Vector2(startingX, startingY);
        }
        
        private static Vector3 CalculateSlotPosition(KeyVarsTracker varsTracker, Vector2 startingPosition, Vector2Int gridPosition)
        {
            return new Vector3(startingPosition.x + gridPosition.x * varsTracker.cellSize.x,
                startingPosition.y - gridPosition.y * varsTracker.cellSize.y, -0.1f);
        }
    }
}
