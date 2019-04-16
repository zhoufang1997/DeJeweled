using UnityEngine;
using UnityEngine.Experimental.UIElements;

namespace Model
{
    public class ViewObjsTracker : MonoBehaviour
    {
        public GameObject viewObj;
        
        [HideInInspector] public GameObject backgroundObj;
        
        [HideInInspector] public GameObject gemGridObj;
        [HideInInspector] public GameObject spawnGridObj;
        [HideInInspector] public GameObject killGridObj;
        
        [HideInInspector] public GameObject[,] gemSlotObjs;
        [HideInInspector] public GameObject[,] spawnSlotObjs;
        [HideInInspector] public GameObject[,] killSlotObjs;

        [HideInInspector] public GameObject[,] gemOBjs;
        [HideInInspector] public GameObject[,] spawnGemObjs;
        [HideInInspector] public GameObject[,] killGemObjs;

    }
}
