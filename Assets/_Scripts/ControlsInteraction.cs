using UnityEngine;
using System.Collections;

public class ControlsInteraction : MonoBehaviour {

    public static float HInput = 0;
    public static bool Jump = false;
    public void StartMoving(float horizonalInput)
    {
        HInput = horizonalInput;
    }
    public void Jumping(bool isJump)
    {
        Jump = isJump;
    }
}
