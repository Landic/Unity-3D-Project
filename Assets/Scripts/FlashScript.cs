using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class FlashScript : MonoBehaviour
{
    //private GameObject character;
    private Rigidbody playerRb;
    [SerializeField]
    private float chargeTimeout = 5.0f, flashCharge;
    private Light spotLight;


    void Start()
    {
        //character = GameObject.Find("Character");
        playerRb = GameObject.Find("CharacterPlayer").GetComponent<Rigidbody>();
        spotLight = GetComponent<Light>();
        GameState.SubscribeTrigger(BatteryTriggerListener, "Battery");
    }

    void Update()
    {
        if (GameState.flashCharge > 0)
        {
            GameState.flashCharge -= Time.deltaTime / chargeTimeout;
            if (GameState.flashCharge < 0)
            {
                GameState.flashCharge = 0;
            }
            spotLight.intensity = GameState.flashCharge;
        }

        if (GameState.isFpv)
        {
            this.transform.rotation = Camera.main.transform.rotation;
        }
        else
        {
            if (playerRb.linearVelocity.magnitude > 0.01f)
                this.transform.forward = playerRb.linearVelocity.normalized;
        }
    }

    private void BatteryTriggerListener(string type, object payload)
    {
        if (type == "Battery") flashCharge += (float)payload;
    }
    private void OnDestroy() => GameState.UnsubscribeTrigger(BatteryTriggerListener, "Battery");
}
