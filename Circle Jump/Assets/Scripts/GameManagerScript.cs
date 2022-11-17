using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    private float circleMinimumX = -2.0f;
    private float circleMaximumX = 2.0f;

    [SerializeField]
    private GameObject circlePrefab;


    // Start is called before the first frame update
    void Start()
    {
        float circleXPosition = Random.Range(circleMaximumX, circleMinimumX);
        Instantiate(circlePrefab, new Vector3(circleXPosition, transform.position.y, transform.position.z), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
