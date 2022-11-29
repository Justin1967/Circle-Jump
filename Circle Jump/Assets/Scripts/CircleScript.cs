using TMPro;
using UnityEngine;

public class CircleScript : MonoBehaviour
{
    private JumperScript jumperScript; //Reference to the JumperScript

    [SerializeField]
    private Animator anim;

    [SerializeField]
    private Transform orbit;

    [SerializeField]
    private Transform orbitPosition;

    [SerializeField]
    private AudioClip circleClip;

    [SerializeField]
    private TextMeshPro numberOfOrbitsText;

    private GameObject target = null;

    public enum MODES { STATIC = 0, LIMITED = 1 }

    public MODES mode = MODES.STATIC;

    private int numberOfOrbits; //only for LIMITED circles
    private int currentOrbits = 0; //keeps track of completed orbits

    private float rotationSpeed = 200.0f; //orbit's rotation speed

    private float rotationDirection; //rotation direction - either -1 or 1

    private float circleSize; //circle size between 0.7 and 1

    public bool leftCircle;

    public bool hasLanded = false;

    public string expandName;

    void Start()
    {
        var chooseMode = Random.Range(0, 2);

        if (chooseMode == 0)
        {
            mode = MODES.STATIC;
            numberOfOrbitsText.text = " ";
        }
        else
        {
            mode = MODES.LIMITED;
            numberOfOrbits = Random.Range(2, 4);
            numberOfOrbitsText.text = numberOfOrbits.ToString();
        }

        SpriteRenderer circleSpriteRenderer = GetComponent<SpriteRenderer>();
        circleSpriteRenderer.color = ColorManagerScript.instance.circleColor[GameManagerScript.instance.indexColor];
        circleSize = Random.Range(0.7f, 1.0f);
        transform.localScale = new Vector3(circleSize, circleSize, 1);
        rotationDirection = Mathf.Pow(-1, Random.Range(1, 3));
    }

    void Update()
    {
        if (hasLanded)
        {
            target.transform.SetPositionAndRotation(orbitPosition.position, orbitPosition.rotation);
        }

        orbit.transform.Rotate(rotationDirection * rotationSpeed * Time.deltaTime * Vector3.forward);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Jumper"))
        {
            target = other.gameObject;
            jumperScript = target.GetComponent<JumperScript>();
            jumperScript.hasJumped = false;
            hasLanded = true;
            SoundManagerScript.instance.PlaySound(circleClip);

            Vector3 direction = target.transform.position - orbit.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
            orbit.eulerAngles = Vector3.forward * angle;

            CreateCircleEffect();

            GameManagerScript.instance.CreateNewCircle();
        }
    }

    private void CreateCircleEffect()
    {
        SpriteRenderer circleSpriteRenderer = GetComponent<SpriteRenderer>();

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

        Instantiate(circleExpand, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
    }

    void DestroyWhenAnimationFinished()
    {
        Destroy(gameObject);
    }
}
