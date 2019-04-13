using UnityEngine;

namespace Model.ModelAnimationStateBehaviors
{
    public class NegateScannedOnEntry : StateMachineBehaviour
    {
        private static readonly int GemSlotsScanned = Animator.StringToHash("GemSlotsScanned");

        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.SetBool(GemSlotsScanned, false);
        }
    }
}
