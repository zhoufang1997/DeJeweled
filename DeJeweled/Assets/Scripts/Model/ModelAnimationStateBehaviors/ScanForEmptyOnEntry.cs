using UnityEngine;

namespace Model.ModelAnimationStateBehaviors
{
    public class ScanForEmptyOnEntry : StateMachineBehaviour
    {
        private static readonly int HasEmpty = Animator.StringToHash("HasEmpty");

        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            var objsTracker = animator.gameObject.GetComponent<ViewObjsTracker>();
            var varsTracker = animator.gameObject.GetComponent<KeyVarsTracker>();
            
            ScanForEmptyGemSlots(animator, objsTracker, varsTracker);
        }

        private static void ScanForEmptyGemSlots(Animator animator, ViewObjsTracker objsTracker, KeyVarsTracker varsTracker)
        {
            varsTracker.emptyGemSlots = new bool[varsTracker.gridSize.y, varsTracker.gridSize.x];
            animator.SetBool(HasEmpty, false);
            for (var row = 0; row < varsTracker.gridSize.y; row++)
            {
                for (var column = 0; column < varsTracker.gridSize.x; column++)
                {
                    varsTracker.emptyGemSlots[row, column] =
                        objsTracker.gemSlotObjs[row, column].transform.childCount == 0;
                    if (objsTracker.gemSlotObjs[row, column].transform.childCount == 0)
                    {
                        animator.SetBool(HasEmpty, true);
                    }
                }
            }
        }
    
    }
}
