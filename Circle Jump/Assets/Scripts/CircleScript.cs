using TMPro;
using UnityEngine;

public class CircleScript : MonoBehaviour
{
    [SerializeField]
    private Animator anim;

    [SerializeField]
    private Transform orbit;

    [SerializeField]
    private TextMeshPro numberOfOrbitsText;

    private int numberOfOrbits;

    private float rotationSpeed = 200.0f;

    private float rotationDirection;

    private float circleSize;

    void Start()
    {
        numberOfOrbits = Random.Range(2, 4);
        SpriteRenderer circleSpriteRenderer = GetComponent<SpriteRenderer>();
        circleSpriteRenderer.color = ColorManagerScript.instance.circleColor[GameManagerScript.instance.indexColor];
        circleSize = Random.Range(0.7f, 1.0f);
        transform.localScale = new Vector3(circleSize, circleSize, 1);
        rotationDirection = Mathf.Pow(-1, Random.Range(1, 3));
        numberOfOrbitsText.text = numberOfOrbits.ToString();
    }

    void Update()
    {
        orbit.transform.Rotate(rotationDirection * rotationSpeed * Time.deltaTime * Vector3.forward);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bounds"))
        {
            Destroy(gameObject);
        }
    }
}
