using UnityEngine;
using System.Collections;

public class DesktopControl : IControl {

    public void Init()
    {
        //TODO: Destroy buttons
        GameObject.FindGameObjectWithTag("GUI").SetActive(false);
    }

    public float GetHorizontal()
    {
        return Input.GetAxis("Horizontal");
    }

    public bool Jump()
    {
        return Input.GetKeyUp(KeyCode.Space);
    }
}
