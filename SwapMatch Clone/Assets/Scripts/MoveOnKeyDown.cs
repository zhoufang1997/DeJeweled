using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MoveOnKeyDown : MonoBehaviour
{

    public Animator playerViewAnimator;

    [HideInInspector] public bool operable;
    
    private static readonly int UpPressed = Animator.StringToHash("UpPressed");
    private static readonly int DownPressed = Animator.StringToHash("DownPressed");
    private static readonly int LeftPressed = Animator.StringToHash("LeftPressed");
    private static readonly int RightPressed = Animator.StringToHash("RightPressed");

    private LayerMask _slotMask;
    private LayerMask _gemMask;

    private void Start()
    {
        _slotMask = LayerMask.GetMask("Slot");
        _gemMask = LayerMask.GetMask("Gem");
    }

    // Update is called once per frame
    private void Update()
    {

        operable = playerViewAnimator.GetCurrentAnimatorStateInfo(0).IsName("Standby");
        
        playerViewAnimator.SetBool(UpPressed, false);
        playerViewAnimator.SetBool(DownPressed, false);
        playerViewAnimator.SetBool(LeftPressed, false);
        playerViewAnimator.SetBool(RightPressed, false);

        if (!operable) return;
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            MoveGem(Vector3.up,Vector3.down);
            MovePlayer(Vector3.up);
            playerViewAnimator.SetBool(UpPressed, true);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            MoveGem(Vector3.down, Vector3.up);
            MovePlayer(Vector3.down);
            playerViewAnimator.SetBool(DownPressed, true);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MoveGem(Vector3.left, Vector3.right);
            MovePlayer(Vector3.left);
            playerViewAnimator.SetBool(LeftPressed, true);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveGem(Vector3.right, Vector3.left);
            MovePlayer(Vector3.right);
            playerViewAnimator.SetBool(RightPressed, true);
        }

    }

    private void MoveGem(Vector3 playerMovementDirection, Vector3 gemMovementDirection)
    {
        var startingPosition = transform.parent.position + playerMovementDirection;
        var hit = Physics2D.Raycast(startingPosition, playerMovementDirection, 1, _gemMask);
        hit.collider.gameObject.GetComponent<MoveOnFunctionCall>().MoveGem(gemMovementDirection);
    }

    private void MovePlayer(Vector3 direction)
    {
        var startingPosition = transform.parent.position + direction;
        var hit = Physics2D.Raycast(startingPosition, direction, 1, _slotMask);
        if (hit.collider == null) return;
        Move(hit.collider.transform);
    }

    private void Move(Transform targetParent)
    {
        Transform transform1;
        (transform1 = transform.parent).SetParent(targetParent);
        transform1.localPosition = new Vector3(0, 0, -0.1f);
    }
    
}
