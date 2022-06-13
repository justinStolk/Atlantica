using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeBackpack : MonoBehaviour
{

    public bool upgraded = false;


    
    // Start is called before the first frame update
    void Start()
    {
        EventSystem.SubscribeEvent(EventSystem.EventType.ON_BACKPACK_UPGRADE, Upgrade);  
    }
    
    void Upgrade()
    {
        upgraded = true;
        EventSystem.UnsubscribeEvent(EventSystem.EventType.ON_BACKPACK_UPGRADE, Upgrade);
    }
}
