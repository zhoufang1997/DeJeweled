using UnityEngine;

namespace Model.ModelAnimationStateBehaviors.Setters
{
    public class SetIntOnEntry : StateMachineBehaviour
    {
        public enum SetterType {Absolute, Increment}

        public string intName;
        public SetterType currentSetterType;
        [Range(-6,6)] public int value;
    
        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            var finalValue = currentSetterType == SetterType.Absolute ? value : animator.GetInteger(intName) + value;
            finalValue = Mathf.Clamp(finalValue, 0, 6);
            animator.SetInteger(intName, finalValue);
        }
    }
}
