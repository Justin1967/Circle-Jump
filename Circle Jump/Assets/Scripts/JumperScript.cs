using UnityEngine;

public class JumperScript : MonoBehaviour
{
    //Reference CircleScript
    private CircleScript circleScript;

    //Reference Orbit Position
    private Transform orbitPosition;

    //Jumper speed
    private float jumpSpeed = 20.0f;

    //Variable to check if Jumper has jumped off the circle
    private bool hasJumped = false;

    //Variable to check if Jumper has landed on a circle
    private bool hasLanded = false;

    // Start is called before the first frame update
    void Start()
    {
        circleScript = GameObject.FindGameObjectWithTag("Circle").GetComponent<CircleScript>();
    }

    // Update is called once per frame
    void Update()
    {
        //Jump when SPACE is pressed
        if (Input.GetKeyDown(KeyCode.Space) && !hasJumped)
        {
            hasJumped = true;
            hasLanded = false;
        }

        JumperMovement();
        RotateAroundCircle();
    }

    private void JumperMovement()
    {
        if (hasJumped)
        {
            transform.Translate(jumpSpeed * Time.deltaTime * Vector2.up);
        }
    }

    private void RotateAroundCircle()
    {
        if (hasLanded)
        {
            transform.SetPositionAndRotation(orbitPosition.position, orbitPosition.rotation);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Circle"))
        {
            //Get reference OrbitPosition of the Circle
            orbitPosition = other.gameObject.transform.GetChild(0).transform.GetChild(0);

            hasJumped = false;
            hasLanded = true;

            //Set the OrbitPosition to the point where the Jumper landed



        }
    }
}
