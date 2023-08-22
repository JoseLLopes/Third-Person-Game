using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDirectionCalculator
{

    public Vector3 CalculateRelativeMoveDir(Transform relativeTransform, Vector2 Input){

        Vector3 camForward = relativeTransform.transform.forward;
        Vector3 camRight = relativeTransform.transform.right;

        camForward.y = 0;
        camRight.y =0;

        Vector3 forwardRelative = Input.y * camForward;
        Vector3 rightRelative = Input.x * camRight;
        
        Vector3 moveDir = forwardRelative + rightRelative;
        return moveDir;
    }
}
