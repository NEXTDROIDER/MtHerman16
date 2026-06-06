using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthUI : MonoBehaviour
{
    [Header("Hearts UI")]
    public Image[] hearts;

    [Header("Heart Sprites")]
    public Sprite heart;
    public Sprite heartBroken;

    private int health;

    private void Start()
    {
        health = hearts.Length;

        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].sprite = heart;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Spike"))
        {
            TakeDamage();
        }
    }

    private void TakeDamage()
    {
        health--;

        if (health >= 0)
        {
            hearts[health].sprite = heartBroken;
        }

        if (health <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}