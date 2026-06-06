using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthUI : MonoBehaviour
{
    public Image[] hearts;
    public Sprite heart;
    public Sprite heartBroken;
    [Tooltip("Optional starting health. If 0, the number of hearts defines starting health.")]
    public int startingHealth = 0;

    private int health;

    private void Start()
    {
        if (hearts == null || hearts.Length == 0)
        {
            Debug.LogError("HealthUI needs heart UI Images assigned in the inspector.");
            return;
        }

        if (heart == null || heartBroken == null)
        {
            Debug.LogError("HealthUI needs both heart and heartBroken sprites assigned.");
            return;
        }

        health = startingHealth > 0 ? Mathf.Clamp(startingHealth, 0, hearts.Length) : hearts.Length;
        UpdateHearts();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Spike"))
        {
            TakeDamage();
        }
    }

    public void TakeDamage(int amount = 1)
    {
        if (hearts == null || hearts.Length == 0)
            return;

        if (health <= 0 || amount <= 0)
            return;

        health = Mathf.Max(health - amount, 0);
        UpdateHearts();

        if (health <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void UpdateHearts()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (hearts[i] == null)
            {
                Debug.LogError("HealthUI heart slot " + i + " is not assigned.");
                continue;
            }

            hearts[i].sprite = i < health ? heart : heartBroken;
        }
    }
}