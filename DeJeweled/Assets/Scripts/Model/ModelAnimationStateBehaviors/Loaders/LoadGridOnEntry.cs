using UnityEngine;

namespace Model.ModelAnimationStateBehaviors.Loaders
{
    public class LoadGridOnEntry : StateMachineBehaviour
    {
    
        public enum GridType {GemGrid, SpawnGrid, KillGrid}

        public GridType currentGridType;
    
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            var objsTracker = animator.gameObject.GetComponent<ViewObjsTracker>();
            var varsTracker = animator.gameObject.GetComponent<KeyVarsTracker>();
            var prefTracker = animator.gameObject.GetComponent<PrefabsTracker>();

            Vector3 gridPosition;
            GameObject grid;
            switch (currentGridType)
            {
                case GridType.GemGrid:
                    gridPosition = varsTracker.gridCenter;
                    grid = Instantiate(prefTracker.gemGridPrefab);
                    grid.name = "GemGrid";
                    break;
                case GridType.SpawnGrid:
                    gridPosition = varsTracker.gridCenter + new Vector3(0, 9, 0);
                    grid = new GameObject("SpawnGrid");
                    break;
                default:
                    gridPosition = varsTracker.gridCenter + new Vector3(0, -9, 0);
                    grid = new GameObject("KillGrid");
                    break;
            }
            
            grid.transform.SetParent(objsTracker.backgroundObj.transform, false);
            grid.transform.localPosition = gridPosition;
            

            switch (currentGridType)
            {
                case GridType.GemGrid:
                    objsTracker.gemGridObj = grid;
                    break;
                case GridType.SpawnGrid:
                    objsTracker.spawnGridObj = grid;
                    break;
                default:
                    objsTracker.killGridObj = grid;
                    break;
            }
        }
    }
}
