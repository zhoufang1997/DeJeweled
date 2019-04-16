using UnityEngine;

namespace Model.ModelAnimationStateBehaviors
{
    public class GenerateSpawnGridOnEntry : StateMachineBehaviour
    {
        private static readonly int SpawnGridGenerated = Animator.StringToHash("SpawnGridGenerated");

        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            var objsTracker = animator.gameObject.GetComponent<ViewObjsTracker>();
            var varsTracker = animator.gameObject.GetComponent<KeyVarsTracker>();
        
            objsTracker.spawnGridObj = new GameObject("SpawnGrid");
            objsTracker.spawnGridObj.transform.SetParent(objsTracker.backgroundObj.transform);
            objsTracker.spawnGridObj.transform.localPosition = new Vector3(varsTracker.gridCenter.x, varsTracker.gridCenter.y + 9, -0.1f);
            objsTracker.spawnGridObj.transform.localScale = Vector3.one;
        
            animator.SetBool(SpawnGridGenerated, true);
        }
    }
}
