using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckSpike : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player hit a spike. Restarting scene...");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}