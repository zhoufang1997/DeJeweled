using UnityEngine;

namespace Model.ModelAnimationStateBehaviors.Loaders
{
    public class LoadBackgroundOnEntry : StateMachineBehaviour
    {

        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            var objsTracker = animator.gameObject.GetComponent<ViewObjsTracker>();
            var prefTracker = animator.gameObject.GetComponent<PrefabsTracker>();
        
            objsTracker.backgroundObj = Instantiate(prefTracker.backgroundPrefab, objsTracker.viewObj.transform, false);
            objsTracker.backgroundObj.name = "BackGround";
        }
    }
}
