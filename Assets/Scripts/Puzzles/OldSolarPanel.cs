using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OldSolarPanel : MonoBehaviour, ILaserTarget
{
    public UnityEvent OnSolarPanelPowered;

    [SerializeField] private int lasersRequired = 2;
    
    private int lasersHit = 0;
    private bool cleared;

    public void OnTargetHit()
    {
        if (cleared)
            return;

        lasersHit++;
        switch (lasersHit) 
        {
            case 1:
                EventSystem.CallEvent(EventSystem.EventType.ON_SINGLE_SOLAR_HIT);
                break;
            case 2:
                EventSystem.CallEvent(EventSystem.EventType.ON_DUAL_SOLAR_HIT);
                cleared = true;
                break;
        }

        if (lasersHit == lasersRequired)
        {
            OnSolarPanelPowered.Invoke();
            OnSolarPanelPowered.RemoveAllListeners();

        }
    }
    public void OnTargetExit()
    {
        if (cleared)
            return;

        lasersHit--;
        switch (lasersHit)
        {
            case 0:
                EventSystem.CallEvent(EventSystem.EventType.ON_ZERO_SOLAR_HITS);
                break;
            case 1:
                EventSystem.CallEvent(EventSystem.EventType.ON_SINGLE_SOLAR_HIT);
                break;
        }
    }

}
