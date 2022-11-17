using UnityEngine;

public class CircleScript : MonoBehaviour
{
    //Reference Orbit
    [SerializeField]
    private Transform orbit;

    //Rotation speed Orbit
    private float rotationSpeed = 100.0f;

    //Rotation direction
    private float rotationDirection;

    //The size of the circle is between 0.6 and 1.0
    private float circleSize;

    // Start is called before the first frame update
    void Start()
    {
        circleSize = Random.Range(0.7f, 1.0f);
        transform.localScale = new Vector3(circleSize, circleSize, 1);
        rotationDirection = Mathf.Pow(-1, Random.Range(1, 3));
    }

    // Update is called once per frame
    void Update()
    {
        orbit.transform.Rotate(rotationDirection * rotationSpeed * Time.deltaTime * Vector3.forward);
    }
}
