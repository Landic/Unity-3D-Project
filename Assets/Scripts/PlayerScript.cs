using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    [SerializeField]
    private float forceFactor = 5.0f;

    private InputAction moveAction;
    private Rigidbody rb;
    private AudioSource[] audioSources;

    private void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        rb = GetComponent<Rigidbody>();
        audioSources = GetComponents<AudioSource>();
        GameState.Subscribe( OnEffectsVolumeChanged,nameof(GameState.effectsVolume), nameof(GameState.isMuted));
        OnEffectsVolumeChanged();
    }

    void Update()
    {
        Vector2 moveValue = moveAction.ReadValue<Vector2>();
        Vector3 correctedForward = Camera.main.transform.forward;
        correctedForward.y = 0.0f;
        correctedForward.Normalize();
        Vector3 forceValue = forceFactor *
            (  
            Camera.main.transform.right * moveValue.x +
            correctedForward * moveValue.y
            );
        rb.AddForce(forceValue);
    }

    private void OnEffectsVolumeChanged()
    {
        foreach (var audioSource in audioSources) audioSource.volume = GameState.effectsVolume;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall") && !audioSources[0].isPlaying) 
            audioSources[0].Play();

    }

    private void OnDestroy()
    {
        GameState.Unsubscribe(OnEffectsVolumeChanged, nameof(GameState.effectsVolume), nameof(GameState.isMuted));
    }
}
