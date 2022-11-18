using UnityEngine;

public class JumperScript : MonoBehaviour
{
    //Reference CircleScript
    private CircleScript circleScript;

    //Reference Orbit
    private Transform orbit;

    //Reference OrbitPositon
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
        //Execute if Jumper misses the Circle
        if (other.gameObject.CompareTag("Bounds") && !hasLanded)
        {
            Destroy(gameObject);
            //Wait 1 second then restart the game
        }
        //Execute if Jumper hits the Circle
        else if (other.gameObject.CompareTag("Circle"))
        {
            //Get reference Circle Sprite
            SpriteRenderer circleSpriteRenderer = other.gameObject.GetComponent<SpriteRenderer>();

            if (circleSpriteRenderer.sprite.name == "Single")
            {
                //Reference Circle Expand1
                GameObject circleExpand1 = Resources.Load("Single Expand") as GameObject;
                SpriteRenderer circleExpand1SpriteRenderer = circleExpand1.GetComponent<SpriteRenderer>();
                circleExpand1SpriteRenderer.sharedMaterial.color = ColorManagerScript.instance.circleColor[GameManagerScript.instance.indexColor];

                Instantiate(circleExpand1, new Vector3(other.gameObject.transform.position.x, other.gameObject.transform.position.y, other.gameObject.transform.position.z), Quaternion.identity);
            }

            if (circleSpriteRenderer.sprite.name == "Double")
            {
                //Reference Circle Expand2
                GameObject circleExpand2 = Resources.Load("Double Expand") as GameObject;
                SpriteRenderer circleExpand2SpriteRenderer = circleExpand2.GetComponent<SpriteRenderer>();
                circleExpand2SpriteRenderer.sharedMaterial.color = ColorManagerScript.instance.circleColor[GameManagerScript.instance.indexColor];

                Instantiate(circleExpand2, new Vector3(other.gameObject.transform.position.x, other.gameObject.transform.position.y, other.gameObject.transform.position.z), Quaternion.identity);
            }

            //Get reference Orbit of the Circle
            orbit = other.gameObject.transform.GetChild(0);
            //Get reference OrbitPosition of the Circle
            orbitPosition = orbit.transform.GetChild(0);

            hasJumped = false;
            hasLanded = true;

            //Set the OrbitPosition to the point where the Jumper landed
            //Calculate direction = destination - source
            Vector3 direction = transform.position - orbit.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
            orbit.eulerAngles = Vector3.forward * angle;

            //Add 1 to score
            GameManagerScript.instance.score += 1;

            if (GameManagerScript.instance.score % 3 == 0)
            {
                GameManagerScript.instance.levelUp = true;
            }

            //Instantiate a new Circle
            GameManagerScript.instance.CreateCircle();
        }
    }
}
