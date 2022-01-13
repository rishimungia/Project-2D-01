using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public PlayerMovement movement;
    public float restartDelay = 1f;
    public GameObject LevelCompleteUI;
    bool isGameOver = false;

    public void CompleteLevel ()
    {
        LevelCompleteUI.SetActive(true);
        movement.enabled = false;
    }

    public void EndGame ()
    {
        if(isGameOver == false)
        {
            isGameOver = true;

            // disable player mavement
            movement.enabled = false;

            // restart the level
            Invoke("Restart", restartDelay);
        }
    }

    void Restart ()
    {
        // reset scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
