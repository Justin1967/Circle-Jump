using UnityEngine;

public class ColorManagerScript : MonoBehaviour
{
    public static ColorManagerScript instance;

    public Color[] circleColor;
    public Color[] jumperColor;
    public Color[] trailColor;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
}
