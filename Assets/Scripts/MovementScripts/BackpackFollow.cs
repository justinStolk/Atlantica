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
        EventSystem.SubscribeEvent(EventSystem.EventType.ON_BACKPACK_TOGGLED, Tog);
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

    void Tog()
    {
        followPlayer = !followPlayer;
    }
}
