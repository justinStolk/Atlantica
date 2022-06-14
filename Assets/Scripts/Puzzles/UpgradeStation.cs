using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeStation : MonoBehaviour, ILaserTarget, IInteractable
{
    private bool Upgradeable = false;


    public void OnTargetExit()
    {
        Upgradeable = false;
    }

    public void OnTargetHit()
    {
        Upgradeable = true;
    }

    public void Interact()
    {
        if (!Upgradeable)
            return;

        EventSystem.CallEvent(EventSystem.EventType.ON_BACKPACK_UPGRADE);
        Destroy(this);
    }
    
}
