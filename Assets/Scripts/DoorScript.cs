using System.Linq;
using UnityEngine;

public class DoorScript : MonoBehaviour {
    private bool isOpen, isLocked;
    private float inTime = 2.0f, OutTime = 20.0f, openTime;

    private AudioSource[] audioSources;

    void Start() {
        isLocked = true;
        isOpen = false;
        openTime = 0.0f;
        audioSources = GetComponents<AudioSource>();
        GameState.Subscribe(OnEffectsVolumeChanged, nameof(GameState.effectsVolume));
        OnEffectsVolumeChanged();
    }
    void Update() {
        if (openTime > 0.0f && !isLocked && !isOpen) {
            openTime -= Time.deltaTime;
            transform.Translate(Time.deltaTime / openTime * Vector3.up);
            if (openTime <= 0.0f) isOpen = true;
        }
    }
    private void OnCollisionEnter(Collision collision) {
        if (GameState.collectedKeys.Keys.Contains("1"))
        {
            bool isInTime = GameState.collectedKeys["1"];
            openTime = isInTime ? inTime : OutTime;
            isLocked = false;
            ToastScript.ShowToast("����� ������� " + (isInTime ? "�������" : "�� �������"));
            (isInTime ? audioSources[1] : audioSources[2]).Play();
        }
        else
        {
            ToastScript.ShowToast("��� �������� ����� ��������� ���� \"1\"!");
            audioSources[0].Play();
        }
    }
    public void OnEffectsVolumeChanged()
    {
        foreach (var audioSource in audioSources) audioSource.volume = GameState.isMuted ? 0.0f : GameState.effectsVolume;
    }
    private void OnDestroy() => GameState.Unsubscribe(OnEffectsVolumeChanged, nameof(GameState.effectsVolume));
}