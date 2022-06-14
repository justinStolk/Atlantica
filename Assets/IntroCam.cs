using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroCam : MonoBehaviour
{

    private void Start()
    {
        EventSystem.SubscribeEvent(EventSystem.EventType.ON_SCREEN_FADE_IN, DestroyIntroCamera);
    }

    public void OnIntroPlayed()
    {
        EventSystem.CallEvent(EventSystem.EventType.ON_GAME_START);
    }

    public void DestroyIntroCamera()
    {
        EventSystem.UnsubscribeEvent(EventSystem.EventType.ON_SCREEN_FADE_IN, DestroyIntroCamera);
        Destroy(this.gameObject);
    }

}
