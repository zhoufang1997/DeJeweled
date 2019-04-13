using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOnFunctionCall : MonoBehaviour
{

    public Animator gemViewAnimator;
    
    private LayerMask _slotMask;
    
    private static readonly int UpCalled = Animator.StringToHash("UpCalled");
    private static readonly int DownCalled = Animator.StringToHash("DownCalled");
    private static readonly int LeftCalled = Animator.StringToHash("LeftCalled");
    private static readonly int RightCalled = Animator.StringToHash("RightCalled");

    private void Start()
    {
        _slotMask = LayerMask.GetMask("Slot");
    }

    public void MoveGem(Vector3 direction)
    {
        var startingPosition = transform.parent.position + direction;
        var hit = Physics2D.Raycast(startingPosition, direction, 1, _slotMask);
        Move(hit.collider.transform);
        if (direction == Vector3.up)
        {
            gemViewAnimator.SetBool(UpCalled, true);
        }
        else if (direction == Vector3.down)
        {
            gemViewAnimator.SetBool(DownCalled, true);
        }
        else if (direction == Vector3.left)
        {
            gemViewAnimator.SetBool(LeftCalled, true);
        }
        else
        {
            gemViewAnimator.SetBool(RightCalled, true);
        }
        
    }
    
    private void Move(Transform targetParent)
    {
        Transform transform1;
        (transform1 = transform.parent).SetParent(targetParent);
        transform1.localPosition = new Vector3(0, 0, -0.1f);
    }
}
