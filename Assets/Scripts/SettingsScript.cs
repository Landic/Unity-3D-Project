using System;
using UnityEngine;
using UnityEngine.UI;

public class SettingsScript : MonoBehaviour
{
    private GameObject content;
    private Slider effectsSlider, ambientSlider;
    public Toggle myToggle;

    void Start()
    {
        Transform contentTransform = this.transform.Find("Content");
        content = contentTransform.gameObject;
        effectsSlider = contentTransform.Find("EffectsSlider").GetComponent<Slider>();
        GameState.effectsVolume = effectsSlider.value;
        ambientSlider = transform.Find("Content").Find("AmbientSlider").GetComponent<Slider>();
        GameState.ambientVolume = ambientSlider.value;
        effectsSlider.onValueChanged.AddListener(OnEffectsVolumeChanged);
        ambientSlider.onValueChanged.AddListener(OnAmbientVolumeChanged);
        myToggle.onValueChanged.AddListener(OnMuteAllChanged);
        Time.timeScale = content.activeInHierarchy ? 0.0f : 1.0f;
    }


    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Time.timeScale = content.activeInHierarchy ? 1.0f : 0.0f;
            content.SetActive(!content.activeInHierarchy);
        }
    }
    
    public void OnEffectsVolumeChanged(Single value)
    {
        Debug.Log("effects " + value);
        GameState.effectsVolume = value;
    }
    public void OnAmbientVolumeChanged(Single value)
    {
        Debug.Log("ambient - "+value);
        GameState.ambientVolume = value;
    }

    public void OnMuteAllChanged(bool value)
    {
        Debug.Log(value);
        GameState.isMuted = value;
    }
}
