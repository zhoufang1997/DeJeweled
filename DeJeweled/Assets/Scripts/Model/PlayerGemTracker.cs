using UnityEngine;

namespace Model
{
    public class PlayerGemTracker : MonoBehaviour
    {
        [HideInInspector] public GameObject playerGemObj;
        [HideInInspector] public Vector2Int playerPosition;

        private ViewObjsTracker _objsTracker;
        private KeyVarsTracker _varsTracker;
        private Animator _modelAnimator;
        private static readonly int InputReceived = Animator.StringToHash("InputReceived");
        private static readonly int Moved = Animator.StringToHash("Moved");
        private static readonly int MoveCalled = Animator.StringToHash("MoveCalled");

        private void Start()
        {
            _objsTracker = GetComponent<ViewObjsTracker>();
            _varsTracker = GetComponent<KeyVarsTracker>();
            _modelAnimator = GetComponent<Animator>();
        }

        public void VerifyInput(Vector2Int direction)
        {
            var targetPosition = playerPosition + direction;
            if (targetPosition.y < 0 || targetPosition.y >= _varsTracker.gridSize.y) return;
            if (targetPosition.x < 0 && targetPosition.x >= _varsTracker.gridSize.x) return;
            _modelAnimator.SetBool(InputReceived, true);
            Move(targetPosition);
        }

        private void Move(Vector2Int targetPosition)
        {
            var targetGem = _objsTracker.gemOBjs[targetPosition.y, targetPosition.x];
            
            var targetSlot = _objsTracker.gemSlotObjs[targetPosition.y, targetPosition.x].transform;
            var thisSlot = _objsTracker.gemSlotObjs[playerPosition.y, playerPosition.x].transform;
            
            playerGemObj.transform.SetParent(targetSlot);
            playerGemObj.transform.GetChild(0).localPosition = playerGemObj.transform.localPosition;
            playerGemObj.transform.localPosition = new Vector3(0, 0, -0.1f);
            playerGemObj.GetComponent<Animator>().SetBool(MoveCalled, true);
            
            
            targetGem.transform.SetParent(thisSlot);
            targetGem.transform.GetChild(0).localPosition = targetGem.transform.localPosition;
            targetGem.transform.localPosition = new Vector3(0, 0, -0.1f);
            targetGem.GetComponent<Animator>().SetBool(MoveCalled, true);
            
            
            _objsTracker.gemOBjs[targetPosition.y, targetPosition.x] = playerGemObj;
            _objsTracker.gemOBjs[playerPosition.y, playerPosition.x] = targetGem;

            playerPosition = targetPosition;
        }
    }
}
