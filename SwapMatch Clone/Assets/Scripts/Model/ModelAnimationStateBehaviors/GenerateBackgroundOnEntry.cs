using UnityEngine;

namespace Model.ModelAnimationStateBehaviors
{
    public class GenerateBackgroundOnEntry : StateMachineBehaviour
    {
        public GameObject backgroundPrefab;
        private static readonly int BackgroundGenerated = Animator.StringToHash("BackgroundGenerated");

        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            var tracker = animator.gameObject.GetComponent<ViewObjsTracker>();
        
            tracker.backgroundObj = Instantiate(backgroundPrefab, Vector3.zero, Quaternion.identity);
            tracker.backgroundObj.name = "BackGround";
            tracker.backgroundObj.transform.SetParent(tracker.viewObj.transform);
        
            animator.SetBool(BackgroundGenerated,true);
        }
    }
}
