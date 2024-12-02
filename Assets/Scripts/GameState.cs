using NUnit.Framework.Internal;
using System;
using System.Collections.Generic;

public class GameState
{
    public static bool isFpv { get; set; }
    public static float flashCharge { get; set; }
    public static Dictionary<string, bool> collectedKeys { get; } = new Dictionary<string, bool>();
    private static readonly Dictionary<string, List<Action>> subscribers = new Dictionary<string, List<Action>>();



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
        get => EffectsVolume;
        set
        {
            if (EffectsVolume != value)
            {
                EffectsVolume = value;
                Notify(nameof(effectsVolume));
            }
        }
    }

    #endregion

    #region ambientVolume
    private static float AmbientVolume = 1.0f;
    public static float ambientVolume
    {
        get => AmbientVolume;
        set
        {
            if (AmbientVolume != value)
            {
                AmbientVolume = value;
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
            }
        }
    }

    #endregion


    #region Change Notifier
    private static void Notify(string propertyName)
    {
        if (subscribers.ContainsKey(propertyName)) subscribers[propertyName].ForEach(action => action());
    }
    public static void Subscribe(string propertyName, Action action)
    {
        if (!subscribers.ContainsKey(propertyName)) subscribers[propertyName] = new List<Action>();
        subscribers[propertyName].Add(action);
    }
    public static void Subscribe(Action action, params string[] propertyNames)
    {
        if (propertyNames.Length == 0) throw new ArgumentException($"{nameof(propertyNames)} must have at least 1 value");
        foreach(var item in propertyNames)
        {
            Subscribe(item, action);
        }
    }
    public static void Unsubscribe(string propertyName, Action action)
    {
        if (subscribers.ContainsKey(propertyName)) subscribers[propertyName].Remove(action);
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
