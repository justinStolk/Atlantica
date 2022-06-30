using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackpackFollow : MonoBehaviour
{
    public bool FollowPlayer = true;
    public GameObject BackpackHolder;
    
    // Start is called before the first frame update
    void Start()
    {
        EventSystem.SubscribeEvent(EventSystem.EventType.ON_BACKPACK_RELEASE, ReleaseBackPack);
        EventSystem.SubscribeEvent(EventSystem.EventType.ON_BACKPACK_TAKE, TakeBackPack);
    }

    // Update is called once per frame
    void Update()
    {
        if(FollowPlayer)
        {
            transform.rotation = BackpackHolder.transform.rotation;
            transform.position = BackpackHolder.transform.position;
        }
    }

    void ReleaseBackPack()
    {
            FollowPlayer = false;
        
    }

    void TakeBackPack()
    {
            FollowPlayer = true;
    }

}
