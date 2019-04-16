using UnityEngine;

namespace Model.ModelAnimationStateBehaviors
{
    public class MatchOnEntry : StateMachineBehaviour
    {
        private static readonly int MatchCalled = Animator.StringToHash("MatchCalled");

        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            var varsTracker = animator.gameObject.GetComponent<KeyVarsTracker>();
            var objsTracker = animator.gameObject.GetComponent<ViewObjsTracker>();
            
            Match(varsTracker, objsTracker);
        }

        private static void Match(KeyVarsTracker varsTracker, ViewObjsTracker objsTracker)
        {
            for (var row = 0; row < varsTracker.gridSize.y; row++)
            {
                for (var column = 0; column < varsTracker.gridSize.x; column++)
                {
                    if (varsTracker.matchedGems[row, column])
                    {
                        objsTracker.gemOBjs[row, column].GetComponent<Animator>().SetBool(MatchCalled, true);
                    }
                }
            }
        }
    }
}
