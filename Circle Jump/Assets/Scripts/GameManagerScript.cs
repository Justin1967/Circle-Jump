using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public static GameManagerScript instance;

    private float circleY = -3.0f;

    private float circleMinimumX = -2.0f;
    private float circleMaximumX = 2.0f;

    [SerializeField]
    private GameObject circlePrefab;

    [SerializeField]
    private GameObject jumperPrefab;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        float circleXPosition = Random.Range(circleMaximumX, circleMinimumX);
        Instantiate(circlePrefab, new Vector3(circleXPosition, circleY, transform.position.z), Quaternion.identity);
        Instantiate(jumperPrefab, new Vector3(circleXPosition, circleY, transform.position.z), Quaternion.identity);
    }

    //Call this method when Jumper has landed on a Circle
    public void CreateCircle()
    {
        circleY += 6.0f;
        float circleXPosition = Random.Range(circleMaximumX, circleMinimumX);
        Instantiate(circlePrefab, new Vector3(circleXPosition, circleY, transform.position.z), Quaternion.identity);

    }
}
