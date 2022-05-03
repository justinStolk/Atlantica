using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventSystem
{
    public enum EventType
    {

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
