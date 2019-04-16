using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCalledFalseOnEnter : StateMachineBehaviour
{
    private static readonly int UpCalled = Animator.StringToHash("UpCalled");
    private static readonly int DownCalled = Animator.StringToHash("DownCalled");
    private static readonly int LeftCalled = Animator.StringToHash("LeftCalled");
    private static readonly int RightCalled = Animator.StringToHash("RightCalled");

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool(UpCalled, false);
        animator.SetBool(DownCalled, false);
        animator.SetBool(LeftCalled, false);
        animator.SetBool(RightCalled, false);
    }
}
