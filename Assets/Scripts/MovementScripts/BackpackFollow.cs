using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackpackFollow : MonoBehaviour
{
    public bool followPlayer = true;
    public GameObject backpackHolder;
    
    // Start is called before the first frame update
    void Start()
    {
        EventSystem.SubscribeEvent(EventSystem.EventType.ON_BACKPACK_RELEASE, ReleaseBackPack);
        EventSystem.SubscribeEvent(EventSystem.EventType.ON_BACKPACK_TAKE, TakeBackPack);
    }

    // Update is called once per frame
    void Update()
    {
        if(followPlayer)
        {
            transform.rotation = backpackHolder.transform.rotation;
            transform.position = backpackHolder.transform.position;
        }
    }

    void ReleaseBackPack()
    {
            followPlayer = false;
        
    }

    void TakeBackPack()
    {
            followPlayer = true;
    }

}
