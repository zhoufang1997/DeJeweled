using UnityEngine;

namespace Model.ModelAnimationStateBehaviors.Setters
{
    public class SetControllerActiveDuringState : StateMachineBehaviour
    {
        private ControllerObjsTracker _objsTracker;
        
        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            _objsTracker = animator.gameObject.GetComponent<ControllerObjsTracker>();
            _objsTracker.controllerObj.SetActive(true);
        }

        // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            _objsTracker.controllerObj.SetActive(false);
        }
    }
}
