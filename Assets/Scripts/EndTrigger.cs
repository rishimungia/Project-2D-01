using UnityEngine;

public class EndTrigger : MonoBehaviour
{
    public GameManager GM;
    void OnTriggerEnter2D (Collider2D col)
    {
        if(col.name == "Player")
            GM.CompleteLevel();
    }
}
