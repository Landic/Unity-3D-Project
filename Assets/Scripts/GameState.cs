using System;
using System.Collections.Generic;

public class GameState
{
    public static bool isFpv { get; set; }
    public static float flashCharge { get; set; }
    public static Dictionary<string, bool> collectedKeys { get; } = new Dictionary<string, bool>();
    private static readonly Dictionary<string, List<Action>> subscribers = new Dictionary<string, List<Action>>();

    private static float EffectsVolume = 1.0f;
    private static float AmbientVolume = 1.0f;

    private static bool ismuted = false;
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

    private static void Notify(string propertyName)
    {
        if (subscribers.ContainsKey(propertyName)) subscribers[propertyName].ForEach(action => action());
    }
    public static void Subscribe(string propertyName, Action action)
    {
        if (!subscribers.ContainsKey(propertyName)) subscribers[propertyName] = new List<Action>();
        subscribers[propertyName].Add(action);
    }
    public static void Unsubscribe(string propertyName, Action action)
    {
        if (subscribers.ContainsKey(propertyName)) subscribers[propertyName].Remove(action);
    }
}
