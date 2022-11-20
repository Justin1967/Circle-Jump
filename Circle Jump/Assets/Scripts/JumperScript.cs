using UnityEngine;

public class JumperScript : MonoBehaviour
{
    private CircleScript circleScript;

    private Transform orbit;

    private Transform orbitPosition;

    private GameObject target;

    private float jumpSpeed = 20.0f;

    private float targetY = 6.0f;

    private bool hasJumped = false;

    public bool hasLanded = false;

    public string expandName;

    void Start()
    {
        SpriteRenderer jumperSpriteRenderer = GetComponent<SpriteRenderer>();
        jumperSpriteRenderer.color = ColorManagerScript.instance.jumperColor[GameManagerScript.instance.indexColor];

        TrailRenderer jumperTrailRenderer = GetComponent<TrailRenderer>();
        jumperTrailRenderer.material.color = ColorManagerScript.instance.trailColor[GameManagerScript.instance.indexColor];

        circleScript = GameObject.FindGameObjectWithTag("Circle").GetComponent<CircleScript>();

        target = GameObject.FindGameObjectWithTag("Target");
    }

    void Update()
    {
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
        if (other.gameObject.CompareTag("Bounds") && !hasLanded)
        {
            Destroy(gameObject);
            //Wait 1 second then restart the game
        }
        else if (other.gameObject.CompareTag("Circle"))
        {
            CreateCircleEffect(other);

            orbit = other.gameObject.transform.GetChild(0);
            orbitPosition = orbit.transform.GetChild(0);

            hasJumped = false;
            hasLanded = true;

            GameManagerScript.instance.score += 1;
            UIManagerScript.instance.scoreText.text = GameManagerScript.instance.score.ToString();

            if (GameManagerScript.instance.score > 1)
            {
                target.transform.position = new Vector3(0, target.transform.position.y + targetY, 0);
            }

            Vector3 direction = transform.position - orbit.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
            orbit.eulerAngles = Vector3.forward * angle;

            GameManagerScript.instance.CreateNewCircle();

            if (GameManagerScript.instance.score % 11 == 0)
            {
                GameManagerScript.instance.levelUp = true;
            }
        }
    }

    private void CreateCircleEffect(Collider2D other)
    {
        SpriteRenderer circleSpriteRenderer = other.gameObject.GetComponent<SpriteRenderer>();

        if (circleSpriteRenderer.sprite.name == "Single")
        {
            expandName = "Single Expand";
        }
        else
        {
            expandName = "Double Expand";
        }

        GameObject circleExpand = (GameObject)Resources.Load(expandName);
        SpriteRenderer circleExpandSpriteRenderer = circleExpand.GetComponent<SpriteRenderer>();
        circleExpandSpriteRenderer.sharedMaterial.color = ColorManagerScript.instance.circleColor[GameManagerScript.instance.indexColor];

        Instantiate(circleExpand, new Vector3(other.gameObject.transform.position.x, other.gameObject.transform.position.y, other.gameObject.transform.position.z), Quaternion.identity);
    }
}
