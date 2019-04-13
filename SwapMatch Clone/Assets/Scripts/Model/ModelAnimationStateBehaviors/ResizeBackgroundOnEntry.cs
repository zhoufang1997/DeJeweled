using UnityEngine;

namespace Model.ModelAnimationStateBehaviors
{
    public class ResizeBackgroundOnEntry : StateMachineBehaviour
    {
        private static readonly int BackgroundResized = Animator.StringToHash("BackgroundResized");

        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            var varsTracker = animator.gameObject.GetComponent<KeyVarsTracker>();
            var objsTracker = animator.gameObject.GetComponent<ViewObjsTracker>();
        
            var screenMin = Camera.main.ScreenToWorldPoint(Vector3.zero);
            var screenMax = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height));
            var screenRange = screenMax - screenMin;
            var referenceRange = new Vector3(16, 9);
            varsTracker.backgroundScale = screenRange.x / referenceRange.x;
            objsTracker.backgroundObj.transform.localScale = new Vector3(varsTracker.backgroundScale, varsTracker.backgroundScale, 1);
        
            animator.SetBool(BackgroundResized,true);
        }
    }
}
