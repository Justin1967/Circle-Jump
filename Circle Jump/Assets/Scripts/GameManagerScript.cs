using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public static GameManagerScript instance;

    private float circleY = -3.0f;

    private float circleMinimumX = -2.0f;
    private float circleMaximumX = 2.0f;

    public int score = -1;
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
            ChangeColor();
        }
    }

    //Call this method when Jumper has landed on a Circle
    public void CreateCircle()
    {
        UIManagerScript.instance.scoreText.text = score.ToString();
        circleY += 6.0f;
        float circleXPosition = Random.Range(circleMaximumX, circleMinimumX);
        Instantiate(circlePrefab, new Vector3(circleXPosition, circleY, transform.position.z), Quaternion.identity);
    }

    public void ChangeColor()
    {
        //Change Circle color
        circles = GameObject.FindGameObjectsWithTag("Circle");

        foreach (GameObject go in circles)
        {
            SpriteRenderer circleSpriteRenderer = go.GetComponent<SpriteRenderer>();
            circleSpriteRenderer.color = ColorManagerScript.instance.circleColor[indexColor];
        }

        //Change Jumper color
        SpriteRenderer jumperSpriteRenderer = GameObject.FindGameObjectWithTag("Jumper").GetComponent<SpriteRenderer>();
        jumperSpriteRenderer.color = ColorManagerScript.instance.jumperColor[indexColor];

        //Change Trail color
        TrailRenderer jumperTrailRenderer = GameObject.FindGameObjectWithTag("Jumper").GetComponent<TrailRenderer>();
        jumperTrailRenderer.material.color = ColorManagerScript.instance.trailColor[GameManagerScript.instance.indexColor];

        //Change Effect color
        if (jumperScript.expandName == "Single Expand")
        {
            GameObject circleExpand = Resources.Load("Single Expand") as GameObject;
            SpriteRenderer circleExpandSpriteRenderer = circleExpand.GetComponent<SpriteRenderer>();
            circleExpandSpriteRenderer.sharedMaterial.color = ColorManagerScript.instance.circleColor[indexColor];
        }
        else if (jumperScript.expandName == "Double Expand")
        {
            GameObject circleExpand = Resources.Load("Double Expand") as GameObject;
            SpriteRenderer circleExpandSpriteRenderer = circleExpand.GetComponent<SpriteRenderer>();
            circleExpandSpriteRenderer.sharedMaterial.color = ColorManagerScript.instance.circleColor[indexColor];
        }
    }

    public void GameOver()
    {
        //What should happen when player loses
    }
}
