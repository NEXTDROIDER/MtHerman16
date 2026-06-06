using UnityEngine;

public class BreadCoin : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Touched: " + other.name);

        if (other.CompareTag("Player"))
        {
            Debug.Log("Bread collected!");
            ScoreManager.instance.AddScore(1);
            Destroy(gameObject);
        }
    }
}