using UnityEngine;
using View;

namespace Model.ModelAnimationStateBehaviors
{
    public class ReplaceCenterGemWithPlayerOnEntry : StateMachineBehaviour
    {
        private static readonly int Initialized = Animator.StringToHash("Initialized");

        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (animator.GetBool(Initialized)) return;
            var varsTracker = animator.gameObject.GetComponent<KeyVarsTracker>();
            var objsTracker = animator.gameObject.GetComponent<ViewObjsTracker>();
            var plygTracker = animator.gameObject.GetComponent<PlayerGemTracker>();
            var prefTracker = animator.gameObject.GetComponent<PrefabsTracker>();
        
            ReplaceCenterGemWithPlayer(varsTracker, objsTracker, prefTracker, plygTracker, animator);
        }

        private void ReplaceCenterGemWithPlayer(KeyVarsTracker varsTracker, ViewObjsTracker objsTracker, PrefabsTracker prefTracker, PlayerGemTracker plygTracker, Animator animator)
        {
            var center = FindCenterGem(varsTracker);
            Destroy(objsTracker.gemOBjs[center.y, center.x]);
            plygTracker.playerGemObj = Instantiate(prefTracker.gemPrefab, objsTracker.gemSlotObjs[center.y, center.x].transform, false);
            plygTracker.playerPosition = center;
            plygTracker.playerGemObj.GetComponent<ModelObjsTracker>().modelAnimator = animator;
            Instantiate(prefTracker.playerPrefab, plygTracker.playerGemObj.transform.GetChild(0), false);
            objsTracker.gemOBjs[center.y, center.x] = plygTracker.playerGemObj;
        }

        private static Vector2Int FindCenterGem(KeyVarsTracker varsTracker)
        {
            var row = varsTracker.gridSize.y % 2 == 0 ? varsTracker.gridSize.y / 2 - 1 : (varsTracker.gridSize.y - 1) / 2;
            var column = varsTracker.gridSize.x % 2 == 0 ? varsTracker.gridSize.x / 2 - 1 : (varsTracker.gridSize.x - 1) / 2;
            return new Vector2Int(column, row);
        }
    
    }
}
