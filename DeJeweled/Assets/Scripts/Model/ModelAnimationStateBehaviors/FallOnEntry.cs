using UnityEngine;

namespace Model.ModelAnimationStateBehaviors
{
    public class FallOnEntry : StateMachineBehaviour
    {
        private static readonly int MoveCalled = Animator.StringToHash("MoveCalled");

        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            var objsTracker = animator.gameObject.GetComponent<ViewObjsTracker>();
            var varsTracker = animator.gameObject.GetComponent<KeyVarsTracker>();
            var plygTracker = animator.gameObject.GetComponent<PlayerGemTracker>();
        
            Fall(varsTracker, objsTracker, plygTracker);
        }

        private static void Fall(KeyVarsTracker varsTracker, ViewObjsTracker objsTracker, PlayerGemTracker plygTracker)
        {
            for (var column = 0; column < varsTracker.gridSize.x; column++)
            {
                FallByColumn(varsTracker, objsTracker, plygTracker, column);
            }
        }

        private static void FallByColumn(KeyVarsTracker varsTracker, ViewObjsTracker objsTracker, PlayerGemTracker plygTracker, int column)
        {
            var spacesToFall = 0;
            for (var row = varsTracker.gridSize.y - 1; row >= 0; row--)
            {
                if (varsTracker.emptyGemSlots[row, column])
                {
                    spacesToFall++;
                }
                else if (!varsTracker.emptyGemSlots[row, column] && spacesToFall > 0)
                {
                    GemFall(row, column, spacesToFall, objsTracker, plygTracker);
                }
            }
            if (spacesToFall > 0)
            {
                SpawnGemsFall(column, spacesToFall, objsTracker, varsTracker);
            }
        }

        private static void GemFall(int gemRow, int gemColumn, int spacesToFall, ViewObjsTracker objsTracker, PlayerGemTracker plygTracker)
        {
            var gem = objsTracker.gemOBjs[gemRow, gemColumn];
            gem.transform.SetParent(objsTracker.gemSlotObjs[gemRow + spacesToFall, gemColumn].transform);
            gem.transform.GetChild(0).localPosition = gem.transform.localPosition;
            gem.transform.localPosition = new Vector3(0, 0, -0.1f);
            gem.GetComponent<Animator>().SetBool(MoveCalled, true);
            objsTracker.gemOBjs[gemRow + spacesToFall, gemColumn] = gem;
            objsTracker.gemOBjs[gemRow, gemColumn] = null;
            if (gem.transform.GetChild(0).GetChild(0).tag.Equals("Player"))
            {
                plygTracker.playerPosition = new Vector2Int(gemColumn, gemRow + spacesToFall);
            }
        }

        private static void SpawnGemsFall(int column, int spacesToFall, ViewObjsTracker objsTracker, KeyVarsTracker varsTracker)
        {
            for (var row = varsTracker.gridSize.y - 1; row >= 0; row--)
            {
                if (objsTracker.spawnGemObjs[row, column] == null) return;
                var gem = objsTracker.spawnGemObjs[row, column];
                gem.transform.SetParent(objsTracker.gemSlotObjs[spacesToFall + (row - varsTracker.gridSize.y), column].transform);
                gem.transform.GetChild(0).localPosition = gem.transform.localPosition;
                gem.transform.localPosition = new Vector3(0, 0, -0.1f);
                gem.GetComponent<Animator>().SetBool(MoveCalled, true);
                objsTracker.gemOBjs[spacesToFall + (row - varsTracker.gridSize.y), column] = gem;
                objsTracker.spawnGemObjs[row, column] = null;
            }
        }
    }
}
