using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SettingsScript : MonoBehaviour {
    private GameObject content;
<<<<<<< HEAD
    private Slider effectsSlider, ambientSlider, sensXSlider, sensYSlider;
    private Toggle muteToggle;
    private List<object> defalutValues = new List<object>();

    private void Start() {
        content = transform.Find("Content").gameObject;

        muteToggle = transform.Find("Content").Find("SoundsToggle").GetComponent<Toggle>();
        defalutValues.Add(GameState.isMuted);
        if (PlayerPrefs.HasKey(nameof(GameState.isMuted))) {
            GameState.isMuted = PlayerPrefs.GetInt(nameof(GameState.isMuted)) == 1;
            muteToggle.isOn = GameState.isMuted;
        } else GameState.isMuted = muteToggle.isOn;

        effectsSlider = transform.Find("Content").Find("EffectsSlider").GetComponent<Slider>();
        defalutValues.Add(effectsSlider.value);
        if (PlayerPrefs.HasKey(nameof(GameState.effectsVolume))) {
            GameState.effectsVolume = PlayerPrefs.GetFloat(nameof(GameState.effectsVolume));
            effectsSlider.value = GameState.effectsVolume;
        } else GameState.effectsVolume = effectsSlider.value;
=======
    private Slider effectsSlider, ambientSlider;
    private Slider sensXSlider, sensYSlider;
    private Toggle muteAllToggle;

    private float defaultAmbientVolume;
>>>>>>> 2e4d346ead394ad923b673a3753f8fb68dd2b106

        ambientSlider = transform.Find("Content").Find("AmbientSlider").GetComponent<Slider>();
<<<<<<< HEAD
        defalutValues.Add(ambientSlider.value);
        if (PlayerPrefs.HasKey(nameof(GameState.ambientVolume))) {
            GameState.ambientVolume = PlayerPrefs.GetFloat(nameof(GameState.ambientVolume));
            ambientSlider.value = GameState.ambientVolume;
        } else GameState.ambientVolume = ambientSlider.value;

        sensXSlider = transform.Find("Content").Find("SensXSlider").GetComponent<Slider>();
        defalutValues.Add(sensXSlider.value);
        if (PlayerPrefs.HasKey(nameof(GameState.sensitivityLookX))) {
            GameState.sensitivityLookX = PlayerPrefs.GetFloat(nameof(GameState.sensitivityLookX));
            sensXSlider.value = GameState.sensitivityLookX;
        } else GameState.sensitivityLookX = sensXSlider.value;

        sensYSlider = transform.Find("Content").Find("SensYSlider").GetComponent<Slider>();
        defalutValues.Add(sensYSlider.value);
        if (PlayerPrefs.HasKey(nameof(GameState.sensitivityLookY))) {
            GameState.sensitivityLookY = PlayerPrefs.GetFloat(nameof(GameState.sensitivityLookY));
            sensYSlider.value = GameState.sensitivityLookY;
        } else GameState.sensitivityLookY = sensYSlider.value;
=======
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
>>>>>>> 2e4d346ead394ad923b673a3753f8fb68dd2b106

        Time.timeScale = content.activeInHierarchy ? 0.0f : 1.0f;
    }
    private void Update() {
        if (Input.GetKeyUp(KeyCode.Escape)) OnCloseButtonClick();
    }
<<<<<<< HEAD
    public void OnCloseButtonClick() {
        Time.timeScale = content.activeInHierarchy ? 1.0f : 0.0f;
        content.SetActive(!content.activeInHierarchy);
=======

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
>>>>>>> 2e4d346ead394ad923b673a3753f8fb68dd2b106
    }
    public void OnResetButtonClick() {
        muteToggle.isOn = (bool)defalutValues[0];
        effectsSlider.value = (float)defalutValues[1];
        ambientSlider.value = (float)defalutValues[2];
        sensXSlider.value = (float)defalutValues[3];
        sensYSlider.value = (float)defalutValues[4];
    }
    public void OnSaveButtonClick() {
        PlayerPrefs.SetFloat(nameof(GameState.ambientVolume), GameState.ambientVolume);
        PlayerPrefs.SetFloat(nameof(GameState.effectsVolume), GameState.effectsVolume);
        PlayerPrefs.SetFloat(nameof(GameState.sensitivityLookX), GameState.sensitivityLookX);
        PlayerPrefs.SetFloat(nameof(GameState.sensitivityLookY), GameState.sensitivityLookY);
        PlayerPrefs.SetInt(nameof(GameState.isMuted), GameState.isMuted ? 1 : 0);
        try { PlayerPrefs.Save(); }
        catch (Exception ex) { Debug.LogError($"Ошибка сохранения настроек: {ex.Message}"); }
        OnCloseButtonClick();
    }
    public void OnEffectsVolumeChanged(Single value) => GameState.effectsVolume = value;
    public void OnAmbientVolumeChanged(Single value) => GameState.ambientVolume = value;
    public void OnMuteAllChanged(bool value) => GameState.isMuted = value;
    public void OnSensitivityXChanged(Single value) => GameState.sensitivityLookX = value;
    public void OnSensitivityYChanged(Single value) => GameState.sensitivityLookY = value;
}