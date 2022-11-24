using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public static GameManagerScript instance;

    //Script references 
    private CircleScript circleScript;
    private JumperScript jumperScript;

    //GameObject references
    [SerializeField]
    private GameObject gameUI;
    [SerializeField]
    private GameObject settingsUI;
    [SerializeField]
    private GameObject titleUI;
    [SerializeField] private GameObject audioOn, audioOff, musicOn, musicOff;
    [SerializeField] private AudioSource soundSource, musicSource;
    [SerializeField]
    private GameObject circlePrefab;
    [SerializeField]
    private GameObject jumperPrefab;
    private GameObject[] circles;

    //Audio references
    public AudioClip clickClip;
    public AudioClip musicClip;

    //Variables
    private float circleY = -3.0f;
    private float circleMinimumX = -1.8f;
    private float circleMaximumX = 1.8f;

    public int score = 0;
    private int best = 0;
    public int level = 1;
    public int indexColor = 0;

    public bool levelUp = false;
    private bool playGame = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
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
        circleScript = GameObject.FindGameObjectWithTag("Circle").GetComponent<CircleScript>();

        if (circleScript.expandName == "Single Expand")
        {
            GameObject circleExpand = (GameObject)Resources.Load("Single Expand");
            SpriteRenderer circleExpandSpriteRenderer = circleExpand.GetComponent<SpriteRenderer>();
            circleExpandSpriteRenderer.material.color = ColorManagerScript.instance.circleColor[indexColor];
        }
        else if (circleScript.expandName == "Double Expand")
        {
            GameObject circleExpand = (GameObject)Resources.Load("Double Expand");
            SpriteRenderer circleExpandSpriteRenderer = circleExpand.GetComponent<SpriteRenderer>();
            circleExpandSpriteRenderer.material.color = ColorManagerScript.instance.circleColor[indexColor];
        }
    }

    public void GameOver()
    {
        //What should happen when player loses
    }

    public void OnPlayButtonPressed()
    {
        SoundManagerScript.instance.PlaySound(clickClip);
        SoundManagerScript.instance.PlayMusic(musicClip);

        titleUI.SetActive(false);

        gameUI.SetActive(true);

        jumperScript = jumperPrefab.GetComponent<JumperScript>();

        float circleXPosition = Random.Range(circleMaximumX, circleMinimumX);
        Instantiate(circlePrefab, new Vector3(circleXPosition, circleY, transform.position.z), Quaternion.identity);
        Instantiate(jumperPrefab, new Vector3(circleXPosition, circleY, transform.position.z), Quaternion.identity);
    }

    public void OnSettingsButtonPressed()
    {
        SoundManagerScript.instance.PlaySound(clickClip);

        GameObject uiTitle = GameObject.Find("Title UI");
        uiTitle.SetActive(false);

        settingsUI.SetActive(true);
    }

    public void OnSoundButtonOnPressed()
    {
        SoundManagerScript.instance.PlaySound(clickClip);
        soundSource.volume = 0;
        audioOn.SetActive(false);
        audioOff.SetActive(true);
    }

    public void OnSoundButtonOffPressed()
    {
        soundSource.volume = 1;
        audioOff.SetActive(false);
        audioOn.SetActive(true);
    }

    public void OnMusicButtonOnPressed()
    {
        SoundManagerScript.instance.PlaySound(clickClip);
        musicSource.volume = 0;
        musicOn.SetActive(false);
        musicOff.SetActive(true);
    }

    public void OnMusicButtonOffPressed()
    {
        SoundManagerScript.instance.PlaySound(clickClip);
        musicSource.volume = 1;
        musicOff.SetActive(false);
        musicOn.SetActive(true);
    }

    public void OnReturnButtonPressed()
    {
        SoundManagerScript.instance.PlaySound(clickClip);

        settingsUI.SetActive(false);

        titleUI.SetActive(true);
    }
}