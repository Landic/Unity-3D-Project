using NUnit.Framework.Internal;
using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;

public class GameState
{
    public static bool isFpv { get; set; }
    public static float flashCharge { get; set; }
    public static Dictionary<string, bool> collectedKeys { get; } = new Dictionary<string, bool>();
    private static readonly Dictionary<string, List<Action>> subscribers = new Dictionary<string, List<Action>>();
    private static Dictionary<string, List<Action<string, object>>> eventListeners = new();

<<<<<<< HEAD
    private static float effectsvolume = 1.0f, ambientvolume = 1.0f, sensitivityx = 3.5f, sensitivityy = 3.5f;
=======
>>>>>>> 2e4d346ead394ad923b673a3753f8fb68dd2b106


    #region sensitivityLook

    private static float _sensitivityLookX = 0.5f;

    public static float sensitivityLookX
    {
        get => _sensitivityLookX;
        set
        {
            if (_sensitivityLookX != value)
            {
                _sensitivityLookX = value;
                Notify(nameof(_sensitivityLookX));
            }
        }
    }

    private static float _sensitivityLookY = 0.5f;

    public static float sensitivityLookY
    {
        get => _sensitivityLookY;
        set
        {
            if (_sensitivityLookY != value)
            {
                _sensitivityLookY = value;
                Notify(nameof(_sensitivityLookY));
            }
        }
    }

    #endregion


    #region effectsVolume
    private static float EffectsVolume = 1.0f;
    public static float effectsVolume
    {
        get => effectsvolume;
        set
        {
            if (effectsvolume != value)
            {
                effectsvolume = value;
                Notify(nameof(effectsVolume));
            }
        }
    }

    #endregion

    #region ambientVolume
    private static float AmbientVolume = 1.0f;
    public static float ambientVolume
    {
        get => ambientvolume;
        set
        {
            if (ambientvolume != value)
            {
                ambientvolume = value;
                Notify(nameof(ambientVolume));
            }
        }
    }
    #endregion

    #region isMuted
    private static bool ismuted = false;
    public static bool isMuted
    {
        get => ismuted;
        set
        {
            if (ismuted != value)
            {
                ismuted = value;
                Notify(nameof(isMuted));
                Notify(nameof(effectsVolume));
            }
        }
    }

    public static float sensitivityLookX
    {
        get => sensitivityx;
        set
        {
            if (sensitivityx != value)
            {
                sensitivityx = value;
                Notify(nameof(sensitivityLookX));
            }
        }
    }
    public static float sensitivityLookY
    {
        get => sensitivityy;
        set
        {
            if (sensitivityy != value)
            {
                sensitivityy = value;
                Notify(nameof(sensitivityLookY));
            }
        }
    }

    public static void TriggerKeyEvent(string keyName, bool isInTime)
    {
        var payload = new Dictionary<string, object> {
            { "KeyName", keyName },
            { "IsInTime", isInTime }
        };
        TriggerEvent("KeyCollected", payload);
    }
    public static void TriggerEvent(string type, object payload = null)
    {
        if (eventListeners.ContainsKey(type))
        {
            foreach (var eventListener in eventListeners[type]) eventListener(type, payload);
        }
        if (eventListeners.ContainsKey("Broadcast"))
        {
            foreach (var eventListener in eventListeners["Broadcast"]) eventListener(type, payload);
        }
    }

    public static void SubscribeTrigger(Action<string, object> action, params string[] types)
    {
        if (types.Length == 0) types = new string[1] { "Broadcast" };
        foreach (var type in types)
        {
            if (!eventListeners.ContainsKey(type)) eventListeners[type] = new List<Action<string, object>>();
            eventListeners[type].Add(action);
        }
    }
    public static void UnsubscribeTrigger(Action<string, object> action, params string[] types)
    {
        if (types.Length == 0) types = new string[1] { "Broadcast" };
        foreach (var type in types)
        {
            if (!eventListeners.ContainsKey(type))
            {
                eventListeners[type].Remove(action);
                if (eventListeners[type].Count == 0) eventListeners.Remove(type);
            }
        }
    }

    #endregion


    #region Change Notifier
    private static void Notify(string propertyName)
    {
        if (subscribers.ContainsKey(propertyName)) subscribers[propertyName].ForEach(action => action());
    }
    public static void Subscribe(Action action, params string[] propertyNames)
    {
        if (propertyNames.Length == 0) throw new ArgumentException($"{nameof(propertyNames)} must have at least 1 value");
        foreach (var propertyName in propertyNames)
        {
            if (!subscribers.ContainsKey(propertyName)) subscribers[propertyName] = new List<Action>();
            subscribers[propertyName].Add(action);
        }
    }
<<<<<<< HEAD
    public static void Unsubscribe(Action action, params string[] propertyNames)
=======
    public static void Subscribe(Action action, params string[] propertyNames)
    {
        if (propertyNames.Length == 0) throw new ArgumentException($"{nameof(propertyNames)} must have at least 1 value");
        foreach(var item in propertyNames)
        {
            Subscribe(item, action);
        }
    }
    public static void Unsubscribe(string propertyName, Action action)
>>>>>>> 2e4d346ead394ad923b673a3753f8fb68dd2b106
    {
        if (propertyNames.Length == 0) throw new ArgumentException($"{nameof(propertyNames)} must have at least 1 value");
        foreach (var propertyName in propertyNames)
        {
            if (subscribers.ContainsKey(propertyName)) subscribers[propertyName].Remove(action);
        }
    }

    public static void Unsubscribe(Action action, params string[] propertyNames)
    {
        if (propertyNames.Length == 0) throw new ArgumentException($"{nameof(propertyNames)} must have at least 1 value");
        foreach (var item in propertyNames)
        {
            Unsubscribe(item, action);
        }
    }
    #endregion
}
