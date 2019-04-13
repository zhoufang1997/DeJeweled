using UnityEngine;

namespace Model.ModelAnimationStateBehaviors
{
    public class GenerateGemsOnEntry : StateMachineBehaviour
    {
        public GameObject gemPrefab;

        public GameObject red;
        public GameObject yellow;
        public GameObject green;
        public GameObject cyan;
        public GameObject blue;
        public GameObject magenta;

        public GameObject player;
        private static readonly int GemsGenerated = Animator.StringToHash("GemsGenerated");

        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            var varsTracker = animator.gameObject.GetComponent<KeyVarsTracker>();
            var objsTracker = animator.gameObject.GetComponent<ViewObjsTracker>();
        
            GenerateGems(varsTracker, objsTracker);
            CheckPlayer(objsTracker, varsTracker);
        
            animator.SetBool(GemsGenerated, true);
        }

        private void CheckPlayer(ViewObjsTracker objsTracker, KeyVarsTracker varsTracker)
        {
            if (objsTracker.playerGemObj != null) return;
            var center = CalculateCenterSlot(varsTracker);
            Destroy(objsTracker.spawnGemObjs[center.y, center.x]);
            objsTracker.playerGemObj = GeneratePlayerGem();
            objsTracker.spawnGemObjs[center.y, center.x] = objsTracker.playerGemObj;
            objsTracker.playerGemObj.transform.SetParent(objsTracker.spawnSlotObjs[center.y, center.x].transform);
            objsTracker.playerGemObj.transform.localPosition = new Vector3(3, 0, -0.1f);
            objsTracker.playerGemObj.transform.localScale = Vector3.one;
        }

        private GameObject GeneratePlayerGem()
        {
            var gem = Instantiate(gemPrefab);
            var playerSprite = Instantiate(player, gem.transform.GetChild(0), true);
            playerSprite.transform.localPosition = Vector3.zero;
            playerSprite.transform.localScale = Vector3.one;
            return gem;
        }

        private static Vector2Int CalculateCenterSlot(KeyVarsTracker varsTracker)
        {
            var centerX = varsTracker.gridSize.x % 2 == 0 ? varsTracker.gridSize.x / 2 - 1 : (varsTracker.gridSize.x - 1) / 2;
            var centerY = varsTracker.gridSize.y % 2 == 0 ? varsTracker.gridSize.y / 2 - 1 : (varsTracker.gridSize.y - 1) / 2;
            return new Vector2Int(centerX, centerY);
        }

        private void GenerateGems(KeyVarsTracker varsTracker, ViewObjsTracker objsTracker)
        {
            objsTracker.spawnGemObjs = new GameObject[varsTracker.gridSize.y, varsTracker.gridSize.x];
            for (var column = 0; column < varsTracker.gridSize.x; column++)
            {
                GenerateGemsInColumn(varsTracker, objsTracker, column);
            }
        }

        private void GenerateGemsInColumn(KeyVarsTracker varsTracker, ViewObjsTracker objsTracker, int column)
        {
            var startingIndex = varsTracker.gridSize.y - CheckColumn(varsTracker, column);
            for (var row = startingIndex; row < varsTracker.gridSize.y; row++)
            {
                objsTracker.spawnGemObjs[row, column] = GenerateGem();
                objsTracker.spawnGemObjs[row, column].transform.SetParent(objsTracker.spawnSlotObjs[row, column].transform);
                objsTracker.spawnGemObjs[row, column].transform.localPosition = new Vector3(0, 0, -0.1f);
                objsTracker.spawnGemObjs[row, column].transform.localScale = Vector3.one;
            }
        }

        private GameObject GenerateGem()
        {
            var gem = Instantiate(gemPrefab);
            var sprite = RandomColorPicker();
            sprite.transform.SetParent(gem.transform.GetChild(0));
            sprite.transform.localPosition = Vector3.zero;
            sprite.transform.localScale = Vector3.one;
            return gem;
        }

        private GameObject RandomColorPicker()
        {
            var randomNumber = Random.Range(0, 60);
            GameObject sprite;
            if (randomNumber < 10)
            {
                sprite = Instantiate(red);
            }
            else if (randomNumber >= 10 && randomNumber < 20)
            {
                sprite = Instantiate(yellow);
            }
            else if (randomNumber >= 20 && randomNumber < 30)
            {
                sprite = Instantiate(green);
            }
            else if (randomNumber >= 30 && randomNumber < 40)
            {
                sprite = Instantiate(cyan);
            }
            else if (randomNumber >= 40 && randomNumber < 50)
            {
                sprite = Instantiate(blue);
            }
            else
            {
                sprite = Instantiate(magenta);
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
