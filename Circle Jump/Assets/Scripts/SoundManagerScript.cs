using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{
    public static SoundManagerScript instance;

    [SerializeField]
    private AudioSource musicSource, effectsSource;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void PlaySound(AudioClip clip)
    {
        effectsSource.PlayOneShot(clip);
    }
}
