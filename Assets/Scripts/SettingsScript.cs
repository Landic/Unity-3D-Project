using System;
using UnityEngine;
using UnityEngine.UI;

public class SettingsScript : MonoBehaviour
{
    private GameObject content;
    private Slider effectsSlider, ambientSlider;
    private Slider sensXSlider, sensYSlider;
    private Toggle muteAllToggle;

    private float defaultAmbientVolume;

    void Start()
    {
        Transform contentTransform = this.transform.Find("Content");
        content = contentTransform.gameObject;
        effectsSlider = contentTransform.Find("EffectsSlider").GetComponent<Slider>();
        GameState.effectsVolume = effectsSlider.value;
        ambientSlider = transform.Find("Content").Find("AmbientSlider").GetComponent<Slider>();
        sensXSlider = transform.Find("Content").Find("SensXSlider").GetComponent<Slider>();
        GameState.sensitivityLookX = sensXSlider.value;
        sensYSlider = transform.Find("Content").Find("SensYSlider").GetComponent<Slider>();
        GameState.sensitivityLookY = sensYSlider.value;
        muteAllToggle = transform.Find("Content").Find("MuteAll").GetComponent<Toggle>();
        GameState.isMuted = muteAllToggle.isOn;

        effectsSlider.onValueChanged.AddListener(OnEffectsVolumeChanged);
        ambientSlider.onValueChanged.AddListener(OnAmbientVolumeChanged);
        muteAllToggle.onValueChanged.AddListener(OnMuteAllChanged);
        sensXSlider.onValueChanged.AddListener(OnSensXChanged);
        sensYSlider.onValueChanged.AddListener(OnSensYChanged);

        defaultAmbientVolume = ambientSlider.value;

        // Try Restore Saved
        if (PlayerPrefs.HasKey(nameof(GameState.ambientVolume)))
        {
            GameState.ambientVolume = PlayerPrefs.GetFloat(nameof(GameState.ambientVolume));
            ambientSlider.value = GameState.ambientVolume;
        }

        else
        {
            GameState.ambientVolume = ambientSlider.value;
        }

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

    public void onDefaultsButtonClick()
    {
        ambientSlider.value = defaultAmbientVolume;
    }

    public void OnSaveButtonClick()
    {
        PlayerPrefs.SetFloat(nameof(GameState.ambientVolume), GameState.ambientVolume);
        PlayerPrefs.SetFloat(nameof(GameState.effectsVolume), GameState.effectsVolume);
        PlayerPrefs.SetInt(nameof(GameState.isMuted), GameState.isMuted ? 1 : 0);
        PlayerPrefs.Save();
    }

    public void OnSensXChanged(Single value)
    {
        Debug.Log("sens x " + value);
        GameState.sensitivityLookX = value;
    }

    public void OnSensYChanged(Single value)
    {
        Debug.Log("sens y " + value);
        GameState.sensitivityLookY = value;
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
