using UnityEngine;

namespace Model.ModelAnimationStateBehaviors
{
    public class GenerateGemGridOnEntry : StateMachineBehaviour
    {
        private static readonly int GemGridGenerated = Animator.StringToHash("GemGridGenerated");

        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            var objsTracker = animator.gameObject.GetComponent<ViewObjsTracker>();
            var varsTracker = animator.gameObject.GetComponent<KeyVarsTracker>();
        
            objsTracker.gemGridObj = new GameObject("GemGrid");
            objsTracker.gemGridObj.transform.SetParent(objsTracker.backgroundObj.transform);
            objsTracker.gemGridObj.transform.localPosition = varsTracker.gridCenter;
            objsTracker.gemGridObj.transform.localScale = Vector3.one;
        
            animator.SetBool(GemGridGenerated, true);
        }
    }
}
