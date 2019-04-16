using UnityEngine;

namespace View.ViewAnimationStateBehaviors
{
    public class AnimateMove : StateMachineBehaviour
    {
        private ViewVarsTracker _varsTracker;

        private float _animationTimePosition;

        private Vector3 _targetPosition;
        private Vector3 _startingPosition;

        private static readonly int Moved = Animator.StringToHash("Moved");

        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            _varsTracker = animator.gameObject.GetComponent<ViewVarsTracker>();
            _targetPosition = new Vector3(0, 0, 0);
            _startingPosition = animator.gameObject.transform.GetChild(0).localPosition;
            _animationTimePosition = 0;
        }

        // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            _animationTimePosition += Time.deltaTime;
            animator.gameObject.transform.GetChild(0).localPosition = Vector3.Lerp(_startingPosition, _targetPosition,
                _varsTracker.moveCurve.Evaluate(_animationTimePosition));
            if (animator.gameObject.transform.GetChild(0).localPosition == _targetPosition)
            {
                animator.gameObject.transform.GetChild(0).localPosition = _targetPosition;
                animator.SetBool(Moved, true);
            }
        }
    }
}
