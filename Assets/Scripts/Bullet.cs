using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20.0f;
    public float punchMultiplierX = 1.0f;
    public float punchMultiplierY = 1.0f;
    public Rigidbody2D rb;
    
    void Start()
    {
        rb.velocity = transform.right * speed;
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Destroy(gameObject);
        
        if(hitInfo.tag == "Box")
        {
            Rigidbody2D box = hitInfo.gameObject.GetComponent<Rigidbody2D>();
            box.AddForce(new Vector2(rb.velocity.x * punchMultiplierX, 20f * punchMultiplierY), ForceMode2D.Impulse);
        }
    }
}
