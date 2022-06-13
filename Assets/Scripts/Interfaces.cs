using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    void Interact();

}

public interface ILaserTarget
{
    void OnTargetHit();
    void OnTargetExit();

}
