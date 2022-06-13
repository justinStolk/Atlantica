using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OldSolarPanel : MonoBehaviour, ILaserTarget
{
    public UnityEvent OnSolarPanelPowered;

    [SerializeField] private int lasersRequired = 2;
    
    private int lasersHit = 0;

    public void OnTargetHit()
    {
        lasersHit++;
        if(lasersHit == lasersRequired)
        {
            OnSolarPanelPowered.Invoke();
            OnSolarPanelPowered.RemoveAllListeners();
        }
    }
    public void OnTargetExit()
    {
        lasersHit--;
    }

}
