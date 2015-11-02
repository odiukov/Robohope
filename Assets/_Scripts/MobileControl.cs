using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MobileControl : IControl
{
    public void Init()
    {
    }

    public float GetHorizontal()
    {
        return ControlsInteraction.HInput;
    }

    public bool Jump()
    {
        if (ControlsInteraction.Jump)
        {
            ControlsInteraction.Jump = false;
            return true;
        }
        return false;
    }
}

