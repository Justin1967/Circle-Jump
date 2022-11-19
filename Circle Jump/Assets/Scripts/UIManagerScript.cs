using TMPro;
using UnityEngine;

public class UIManagerScript : MonoBehaviour
{
    public static UIManagerScript instance;

    public TextMeshProUGUI scoreText;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
}
