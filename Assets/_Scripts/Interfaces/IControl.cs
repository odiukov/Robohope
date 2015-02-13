using UnityEngine;
using System.Collections;

public interface IControl
{
    void Init();
    float GetHorizontal();
    bool Jump();
}
