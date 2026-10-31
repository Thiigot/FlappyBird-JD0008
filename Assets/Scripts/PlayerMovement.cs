using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    bool comecou;
    bool acabou;
    [SerializeField] private GameManager gameManager;
    Rigidbody2D rb;
    Vector2 force = new Vector2(0, 200f);


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow))
        {

            if (!comecou)
            {
                comecou = true;
                rb.isKinematic = false;
            }

            rb.linearVelocity = new Vector2(0, 0);
            rb.AddForce(force);
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Enemy" || other.gameObject.tag == "Baloon")
        {
            gameManager.GameOver();
        }
        else if(other.gameObject.tag == "Score")
        {
            gameManager.PointUP();
        }
    }
}
