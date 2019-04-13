using UnityEngine;

namespace Model.ModelAnimationStateBehaviors
{
    public class GenerateDestroyGridOnEntry : StateMachineBehaviour
    {
        private static readonly int DestroyGridGenerated = Animator.StringToHash("DestroyGridGenerated");

        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            var objsTracker = animator.gameObject.GetComponent<ViewObjsTracker>();
            var varsTracker = animator.gameObject.GetComponent<KeyVarsTracker>();
        
            objsTracker.destroyGridObj = new GameObject("DestroyGrid");
            objsTracker.destroyGridObj.transform.SetParent(objsTracker.backgroundObj.transform);
            objsTracker.destroyGridObj.transform.localPosition = new Vector3(varsTracker.gridCenter.x, varsTracker.gridCenter.y - 9, -0.1f);
            objsTracker.destroyGridObj.transform.localScale = Vector3.one;
        
            animator.SetBool(DestroyGridGenerated, true);
        }
    }
}
