using UnityEngine;
using View;

namespace Model.ModelAnimationStateBehaviors
{
    public class GenerateOnEntry : StateMachineBehaviour
    {

        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            var varsTracker = animator.gameObject.GetComponent<KeyVarsTracker>();
            var objsTracker = animator.gameObject.GetComponent<ViewObjsTracker>();
            var prefTracker = animator.gameObject.GetComponent<PrefabsTracker>();
        
            GenerateGems(varsTracker, objsTracker, prefTracker, animator);
        }

        private void GenerateGems(KeyVarsTracker varsTracker, ViewObjsTracker objsTracker, PrefabsTracker prefTracker, Animator animator)
        {
            objsTracker.spawnGemObjs = new GameObject[varsTracker.gridSize.y, varsTracker.gridSize.x];
            for (var column = 0; column < varsTracker.gridSize.x; column++)
            {
                GenerateGemsByColumn(varsTracker, objsTracker, prefTracker, column, animator);
            }
        }

        private void GenerateGemsByColumn(KeyVarsTracker varsTracker, ViewObjsTracker objsTracker, PrefabsTracker prefTracker, int column, Animator animator)
        {
            var startingIndex = varsTracker.gridSize.y - CheckColumn(varsTracker, column);
            for (var row = startingIndex; row < varsTracker.gridSize.y; row++)
            {
                objsTracker.spawnGemObjs[row, column] = GenerateGem(prefTracker, animator);
                objsTracker.spawnGemObjs[row, column].transform.SetParent(objsTracker.spawnSlotObjs[row, column].transform, false);
            }
        }

        private GameObject GenerateGem(PrefabsTracker prefTracker, Animator animator)
        {
            var gem = Instantiate(prefTracker.gemPrefab);
            gem.GetComponent<ModelObjsTracker>().modelAnimator = animator;
            var sprite = RandomColorPicker(prefTracker);
            sprite.transform.SetParent(gem.transform.GetChild(0), false);
            return gem;
        }

        private GameObject RandomColorPicker(PrefabsTracker prefTracker)
        {
            var randomNumber = Random.Range(0, 60);
            GameObject sprite;
            if (randomNumber < 10)
            {
                sprite = Instantiate(prefTracker.redPrefab);
            }
            else if (randomNumber >= 10 && randomNumber < 20)
            {
                sprite = Instantiate(prefTracker.yellowPrefab);
            }
            else if (randomNumber >= 20 && randomNumber < 30)
            {
                sprite = Instantiate(prefTracker.greenPrefab);
            }
            else if (randomNumber >= 30 && randomNumber < 40)
            {
                sprite = Instantiate(prefTracker.cyanPrefab);
            }
            else if (randomNumber >= 40 && randomNumber < 50)
            {
                sprite = Instantiate(prefTracker.bluePrefab);
            }
            else
            {
                sprite = Instantiate(prefTracker.magentaPrefab);
            }
            return sprite;
        }
        
        private static int CheckColumn(KeyVarsTracker varsTracker, int column)
        {
            var empty = 0;
            for (var row = 0; row < varsTracker.gridSize.y; row++)
            {
                if (varsTracker.emptyGemSlots[row, column] == true)
                {
                    empty++;
                }
            }

            return empty;
        }
    }
}
