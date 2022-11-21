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

    public enum MODES { STATIC = 0, LIMITED = 1 }

    public int myMode;

    private int numberOfOrbits; //only for LIMITED circles

    private float rotationSpeed = 200.0f; //orbit's rotation speed

    private float rotationDirection; //rotation direction - either -1 or 1

    private float circleSize; //circle size between 0.7 and 1

    void Start()
    {
        var chooseMode = Random.Range(0, 2);

        if (chooseMode == 0)
        {
            myMode = (int)MODES.STATIC;
            numberOfOrbitsText.text = " ";
        }
        else
        {
            myMode = (int)MODES.LIMITED;
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
