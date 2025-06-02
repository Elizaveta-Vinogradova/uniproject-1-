using UnityEngine;

public class TrapSound : MonoBehaviour
{
    [SerializeField] public AudioSource audioSource;
    [SerializeField] AudioClip movementSound;

    private void Start() => audioSource.clip = movementSound;
    

    public void PlayMovementSound()
    {
        audioSource.volume = SoundManager.instance.sfxSlider.value;
        audioSource.Play();
    }
}