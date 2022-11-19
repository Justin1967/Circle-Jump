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

    public string expandName;

    // Start is called before the first frame update
    void Start()
    {
        SpriteRenderer jumperSpriteRenderer = GetComponent<SpriteRenderer>();
        jumperSpriteRenderer.color = ColorManagerScript.instance.jumperColor[GameManagerScript.instance.indexColor];

        circleScript = GameObject.FindGameObjectWithTag("Circle").GetComponent<CircleScript>();

        TrailRenderer jumperTrailRenderer = GetComponent<TrailRenderer>();
        jumperTrailRenderer.material.color = ColorManagerScript.instance.trailColor[GameManagerScript.instance.indexColor];
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
            CreateCircleEffect(other);

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
            UIManagerScript.instance.scoreText.text = GameManagerScript.instance.score.ToString();

            if (GameManagerScript.instance.score % 11 == 0)
            {
                GameManagerScript.instance.levelUp = true;
            }

            //Instantiate a new Circle
            GameManagerScript.instance.CreateCircle();
        }
    }

    private void CreateCircleEffect(Collider2D other)
    {
        //Get reference Circle Sprite
        SpriteRenderer circleSpriteRenderer = other.gameObject.GetComponent<SpriteRenderer>();

        if (circleSpriteRenderer.sprite.name == "Single")
        {
            expandName = "Single Expand";
        }
        else
        {
            expandName = "Double Expand";
        }

        //Reference Circle Expand
        GameObject circleExpand = Resources.Load(expandName) as GameObject;
        SpriteRenderer circleExpandSpriteRenderer = circleExpand.GetComponent<SpriteRenderer>();
        circleExpandSpriteRenderer.sharedMaterial.color = ColorManagerScript.instance.circleColor[GameManagerScript.instance.indexColor];

        Instantiate(circleExpand, new Vector3(other.gameObject.transform.position.x, other.gameObject.transform.position.y, other.gameObject.transform.position.z), Quaternion.identity);
    }
}
