using UnityEngine;
using UnityEngine.SceneManagement;

public class Spike : MonoBehaviour
{
    [SerializeField] private int damage = 1;
    [SerializeField] private HealthUI healthUI;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;

        if (healthUI == null)
            healthUI = FindObjectOfType<HealthUI>();

        if (healthUI != null)
        {
            healthUI.TakeDamage(damage);
        }
        else
        {
            Debug.LogWarning("HealthUI not found. Reloading scene because the player hit a spike.");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}