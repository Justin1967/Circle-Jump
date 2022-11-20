using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public static GameManagerScript instance;

    private float circleY = -3.0f;

    private float circleMinimumX = -2.0f;
    private float circleMaximumX = 2.0f;

    public int score = 0;
    private int best = 0;
    public int level = 1;
    public int indexColor = 0;

    public bool levelUp = false;

    [SerializeField]
    private GameObject circlePrefab;

    [SerializeField]
    private GameObject jumperPrefab;

    private GameObject[] circles;

    private JumperScript jumperScript;

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
        jumperScript = jumperPrefab.GetComponent<JumperScript>();

        float circleXPosition = Random.Range(circleMaximumX, circleMinimumX);
        Instantiate(circlePrefab, new Vector3(circleXPosition, circleY, transform.position.z), Quaternion.identity);
        Instantiate(jumperPrefab, new Vector3(circleXPosition, circleY, transform.position.z), Quaternion.identity);
    }

    private void Update()
    {
        if (levelUp)
        {
            levelUp = false;
            level += 1;
            indexColor += 1;
            ChangeCircleColor();
            ChangeJumperColor();
            ChangeEffectColor();
        }
    }

    public void CreateNewCircle()
    {
        circleY += 6.0f;
        float circleXPosition = Random.Range(circleMaximumX, circleMinimumX);
        Instantiate(circlePrefab, new Vector3(circleXPosition, circleY, transform.position.z), Quaternion.identity);
    }

    public void ChangeCircleColor()
    {
        circles = GameObject.FindGameObjectsWithTag("Circle");

        foreach (GameObject go in circles)
        {
            SpriteRenderer circleSpriteRenderer = go.GetComponent<SpriteRenderer>();
            circleSpriteRenderer.color = ColorManagerScript.instance.circleColor[indexColor];
        }
    }

    public void ChangeJumperColor()
    {
        SpriteRenderer jumperSpriteRenderer = GameObject.FindGameObjectWithTag("Jumper").GetComponent<SpriteRenderer>();
        jumperSpriteRenderer.color = ColorManagerScript.instance.jumperColor[indexColor];

        TrailRenderer jumperTrailRenderer = GameObject.FindGameObjectWithTag("Jumper").GetComponent<TrailRenderer>();
        jumperTrailRenderer.material.color = ColorManagerScript.instance.trailColor[indexColor];
    }

    public void ChangeEffectColor()
    {
        if (jumperScript.expandName == "Single Expand")
        {
            GameObject circleExpand = (GameObject)Resources.Load("Single Expand");
            SpriteRenderer circleExpandSpriteRenderer = circleExpand.GetComponent<SpriteRenderer>();
            circleExpandSpriteRenderer.color = ColorManagerScript.instance.circleColor[indexColor];
        }
        else if (jumperScript.expandName == "Double Expand")
        {
            GameObject circleExpand = (GameObject)Resources.Load("Double Expand");
            SpriteRenderer circleExpandSpriteRenderer = circleExpand.GetComponent<SpriteRenderer>();
            circleExpandSpriteRenderer.color = ColorManagerScript.instance.circleColor[indexColor];
        }
    }

    public void GameOver()
    {
        //What should happen when player loses
    }
}
