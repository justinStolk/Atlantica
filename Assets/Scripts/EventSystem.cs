using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventSystem
{
    public enum EventType
    {
        ON_BACKPACK_RELEASE,
        ON_BACKPACK_TAKE,
        ON_BACKPACK_UPGRADE,
        ON_GAME_START, 
        ON_SCREEN_FADE_OUT,
        ON_SCREEN_FADE_IN,
        ON_ZERO_SOLAR_HITS,
        ON_SINGLE_SOLAR_HIT,
        ON_DUAL_SOLAR_HIT
    }

    private static Dictionary<EventType, System.Action> eventActions = new();

    public static void SubscribeEvent(EventType eventType, System.Action eventToSubscribe)
    {
        if (!eventActions.ContainsKey(eventType))
        {
            eventActions.Add(eventType, null);
        }
        eventActions[eventType] += eventToSubscribe;
    }

    public static void UnsubscribeEvent(EventType eventType, System.Action eventToUnsubscribe)
    {
        if (eventActions.ContainsKey(eventType))
        {
            eventActions[eventType] -= eventToUnsubscribe;
        }
    }

    public static void CallEvent(EventType eventType)
    {
        eventActions[eventType]?.Invoke();
    }



}
