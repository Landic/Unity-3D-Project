using System.Linq;
using UnityEngine;

public class DoorScript : MonoBehaviour {
    private bool isOpen, isLocked;
    private float inTime = 2.0f, OutTime = 20.0f, openTime;

    void Start() {
        isLocked = true;
        isOpen = false;
        openTime = 0.0f;
    }
    void Update() {
        if (openTime > 0.0f && !isLocked && !isOpen) {
            openTime -= Time.deltaTime;
            transform.Translate(Time.deltaTime / openTime * Vector3.up);
            if (openTime <= 0.0f) isOpen = true;
        }
    }
    private void OnCollisionEnter(Collision collision) {
        if (GameState.collectedKeys.Keys.Contains("1")) {
            bool isInTime = GameState.collectedKeys["1"];
            openTime = isInTime ? inTime : OutTime;
            isLocked = false;
            ToastScript.ShowToast("����� ������� " + (isInTime ? "�������" : "�� �������"));
        } else ToastScript.ShowToast("��� �������� ����� ��������� ���� \"1\"!");
    }
}