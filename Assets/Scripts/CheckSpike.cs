using UnityEngine;
using UnityEngine.SceneManagement;

public class SpikeObstacle : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Touched: " + other.gameObject.name);

        if (other.CompareTag("spike"))
        {
            Debug.Log("Spike touched. Restarting scene...");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}