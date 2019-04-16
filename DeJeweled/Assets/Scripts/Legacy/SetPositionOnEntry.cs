using UnityEngine;

namespace View.ViewAnimationStateBehaviors.Setters
{
    public class SetPositionOnEntry : StateMachineBehaviour
    {
        private static readonly int MoveVectorX = Animator.StringToHash("MoveVectorX");
        private static readonly int MoveVectorY = Animator.StringToHash("MoveVectorY");

        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.gameObject.transform.GetChild(0).localPosition = new Vector2(-animator.GetFloat(MoveVectorX), -animator.GetFloat(MoveVectorY));
        }
    }
}
