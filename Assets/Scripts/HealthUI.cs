using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthUI : MonoBehaviour
{
    public Image[] hearts;

    public Sprite heart;
    public Sprite heartBroken;
    [Tooltip("Optional starting health. If 0, the heart count defines starting health.")]
    public int startingHealth = 0;

    private int health;

    private void Start()
    {
        if (hearts == null || hearts.Length == 0)
        {
            Debug.LogError("Hearts array is empty. Drag your heart UI Images into the HealthUI script.");
            return;
        }

        if (heart == null)
        {
            Debug.LogError("Normal heart sprite is missing.");
            return;
        }

        if (heartBroken == null)
        {
            Debug.LogError("Broken heart sprite is missing.");
            return;
        }

        health = startingHealth > 0 ? Mathf.Clamp(startingHealth, 0, hearts.Length) : hearts.Length;
        UpdateHearts();
    }

    private void UpdateHearts()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (hearts[i] == null)
            {
                Debug.LogError("Heart slot " + i + " is empty.");
                continue;
            }

            hearts[i].sprite = i < health ? heart : heartBroken;
        }
    }

    public void TakeDamage(int amount = 1)
    {
        if (health <= 0 || amount <= 0)
            return;

        health = Mathf.Max(health - amount, 0);
        UpdateHearts();

        if (health <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}