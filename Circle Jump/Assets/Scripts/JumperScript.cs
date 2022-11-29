using UnityEngine;

public class JumperScript : MonoBehaviour
{
    //Script references
    private CircleScript circleScipt;

    //GameObject references
    private GameObject cameraTarget;

    //Audio references
    [SerializeField]
    private AudioClip jumperClip;

    //Variables
    private float jumpSpeed = 20.0f;
    private float cameraTargetY = 6.0f;

    public bool hasJumped = false;

    void Start()
    {
        circleScipt = GameObject.FindGameObjectWithTag("Circle").GetComponent<CircleScript>();

        SpriteRenderer jumperSpriteRenderer = GetComponent<SpriteRenderer>();
        jumperSpriteRenderer.color = ColorManagerScript.instance.jumperColor[GameManagerScript.instance.indexColor];

        TrailRenderer jumperTrailRenderer = GetComponent<TrailRenderer>();
        jumperTrailRenderer.material.color = ColorManagerScript.instance.trailColor[GameManagerScript.instance.indexColor];

        cameraTarget = GameObject.FindGameObjectWithTag("Target");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !hasJumped)
        {
            SoundManagerScript.instance.PlaySound(jumperClip);
            GameManagerScript.instance.playGame = true;
            hasJumped = true;
            circleScipt.hasLanded = false;
        }
        JumperMovement();
    }

    private void JumperMovement()
    {
        if (hasJumped)
        {
            transform.Translate(jumpSpeed * Time.deltaTime * Vector2.up);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bounds") && hasJumped == true)
        {
            Destroy(gameObject);
        }

        if (GameManagerScript.instance.playGame == true)
        {
            GameManagerScript.instance.score += 1;
            UIManagerScript.instance.scoreText.text = GameManagerScript.instance.score.ToString();

            if (GameManagerScript.instance.score > 0)
            {
                cameraTarget.transform.position = new Vector3(0, cameraTarget.transform.position.y + cameraTargetY, 0);
            }

            if (GameManagerScript.instance.score % 11 == 0)
            {
                GameManagerScript.instance.levelUp = true;
            }
        }
    }
}
