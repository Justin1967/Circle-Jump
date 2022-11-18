using UnityEngine;

public class CircleEffectsScript : MonoBehaviour
{
    private void DestroyWhenAnimationFinished()
    {
        Destroy(gameObject);
    }
}
