using UnityEngine;

namespace Legacy
{
    public class SetCompleteTrueAfterAnimation : StateMachineBehaviour
    {
    
        private float _timeRemaining;
    
        private static readonly int Completed = Animator.StringToHash("Completed");

        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            _timeRemaining = stateInfo.length;
        }

        // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            _timeRemaining -= Time.deltaTime;
            if (_timeRemaining <= 0)
            {
                animator.SetBool(Completed, true);
            }
        }
    }
}
