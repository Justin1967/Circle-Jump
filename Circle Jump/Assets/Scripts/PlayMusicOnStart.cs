using UnityEngine;

public class PlayMusicOnStart : MonoBehaviour
{
    [SerializeField]
    private AudioClip clip;
    // Start is called before the first frame update
    void Start()
    {
        SoundManagerScript.instance.PlaySound(clip);
    }
}
