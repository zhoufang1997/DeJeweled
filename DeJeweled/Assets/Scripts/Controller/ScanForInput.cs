using Model;
using UnityEngine;
using UnityEngine.Networking;

namespace Controller
{
    public class ScanForInput : MonoBehaviour
    {

        public PlayerGemTracker plygTracker;

        // Update is called once per frame
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                plygTracker.VerifyInput(Vector2Int.down);
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                plygTracker.VerifyInput(Vector2Int.up);
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                plygTracker.VerifyInput(Vector2Int.left);
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                plygTracker.VerifyInput(Vector2Int.right);
            }
        }
    }
}
