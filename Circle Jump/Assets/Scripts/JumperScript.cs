using UnityEngine;

public class JumperScript : MonoBehaviour
{
    [SerializeField]
    private AudioClip jumperClip;

    private GameObject cameraTarget;

    private float jumpSpeed = 20.0f;

    private float cameraTargetY = 6.0f;

    private bool hasJumped = false;

    void Start()
    {
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
            hasJumped = true;
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
        hasJumped = false;

        if (other.gameObject.CompareTag("Bounds"))
        {
            Destroy(gameObject);
        }

        GameManagerScript.instance.score += 1;
        UIManagerScript.instance.scoreText.text = GameManagerScript.instance.score.ToString();

        if (GameManagerScript.instance.score > 1)
        {
            cameraTarget.transform.position = new Vector3(0, cameraTarget.transform.position.y + cameraTargetY, 0);
        }

        GameManagerScript.instance.CreateNewCircle();

        if (GameManagerScript.instance.score % 11 == 0)
        {
            GameManagerScript.instance.levelUp = true;
        }
    }
}
