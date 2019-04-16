using UnityEngine;

namespace Model.ModelAnimationStateBehaviors.Setters
{
    public class SetBoolOnEntry : StateMachineBehaviour
    {

        public string boolName;
        public bool value;
    
        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.SetBool(boolName, value);
        }
    }
}
