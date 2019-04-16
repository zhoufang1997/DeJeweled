using UnityEngine;

namespace Model.ModelAnimationStateBehaviors
{
    public class ScanForMatchOnEntry : StateMachineBehaviour
    {
        private static readonly int HasMatch = Animator.StringToHash("HasMatch");

        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            var varsTracker = animator.gameObject.GetComponent<KeyVarsTracker>();
            var objsTracker = animator.gameObject.GetComponent<ViewObjsTracker>();

            varsTracker.matchedGems = CheckMatch(varsTracker, objsTracker, out var hasMatch);
            
            animator.SetBool(HasMatch, hasMatch);
        }

        private static bool[,] CheckMatch(KeyVarsTracker varsTracker, ViewObjsTracker objsTracker, out bool hasMatch)
        {
            hasMatch = false;
            var matches = InitializeFalseArray(varsTracker.gridSize.y, varsTracker.gridSize.x);
            var rowMatches = CheckMatchByRow(varsTracker, objsTracker, out var hasRowMatch);
            var columnMatches = CheckMatchByColumn(varsTracker, objsTracker, out var hasColumnMatch);
            hasMatch = hasRowMatch || hasColumnMatch;
            for (var row = 0; row < varsTracker.gridSize.y; row++)
            {
                for (var column = 0; column < varsTracker.gridSize.x; column++)
                {
                    matches[row, column] = rowMatches[row, column] || columnMatches[row, column];
                }
            }
            return matches;
        }

        private static bool[,] CheckMatchByRow(KeyVarsTracker varsTracker, ViewObjsTracker objsTracker, out bool hasMatch)
        {
            var rowMatches = InitializeFalseArray(varsTracker.gridSize.y, varsTracker.gridSize.x);
            hasMatch = false;
            for (var row = 0; row < varsTracker.gridSize.y; row++)
            {
                var secondLastTag = "0";
                var lastTag = "0";
                for (var column = 0; column < varsTracker.gridSize.x; column++)
                {
                    var thisTag = objsTracker.gemOBjs[row, column].transform.GetChild(0).GetChild(0).gameObject.tag;
                    if (thisTag.Equals(lastTag) && lastTag.Equals(secondLastTag))
                    {
                        rowMatches[row, column] = true;
                        rowMatches[row, column - 1] = true;
                        rowMatches[row, column - 2] = true;
                        hasMatch = true;
                    }
                    secondLastTag = lastTag;
                    lastTag = thisTag;
                }
            }
            return rowMatches;
        }

        private static bool[,] CheckMatchByColumn(KeyVarsTracker varsTracker, ViewObjsTracker objsTracker, out bool hasMatch)
        {
            var columnMatches = InitializeFalseArray(varsTracker.gridSize.y, varsTracker.gridSize.x);
            hasMatch = false;
            for (var column = 0; column < varsTracker.gridSize.x; column++)
            {
                var secondLastTag = "0";
                var lastTag = "0";
                for (var row = 0; row < varsTracker.gridSize.y; row++)
                {
                    var thisTag = objsTracker.gemOBjs[row, column].transform.GetChild(0).GetChild(0).gameObject.tag;
                    if (thisTag.Equals(lastTag) && lastTag.Equals(secondLastTag))
                    {
                        columnMatches[row, column] = true;
                        columnMatches[row - 1, column] = true;
                        columnMatches[row - 2, column] = true;
                        hasMatch = true;
                    }
                    secondLastTag = lastTag;
                    lastTag = thisTag;
                }
            }
            return columnMatches;
        }

        private static bool[,] InitializeFalseArray(int rows, int columns)
        {
            var array = new bool[rows, columns];
            for (var row = 0; row < rows; row++)
            {
                for (var column = 0; column < columns; column++)
                {
                    array[row, column] = false;
                }
            }
            return array;
        }
    }
}
