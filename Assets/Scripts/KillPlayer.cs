using UnityEngine;

public class KillPlayer : MonoBehaviour
{
    // kills player when fallen into lava
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.name == "Player")
        {
            FindObjectOfType<GameManager>().EndGame();

            // disables player sliding due to no friction
            Rigidbody2D player = col.GetComponent<Rigidbody2D>();
            player.velocity = new Vector2(0f, 0f);
            player.gravityScale = 0.0f;
        }
    }

    
}
