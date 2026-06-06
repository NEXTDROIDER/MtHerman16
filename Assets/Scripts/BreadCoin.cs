using UnityEngine;

namespace Assets.Scripts
{
    public class BreadCoin : MonoBehaviour
    {
        private bool collected = false;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (collected) return;

            if (other.CompareTag("Player"))
            {
                collected = true;

                Debug.Log("Bread collected!");

                ScoreManager scoreManager = FindObjectOfType<ScoreManager>();
                if (scoreManager != null)
                {
                    scoreManager.AddScore(1);
                }
                else
                {
                    Debug.LogError("ScoreManager not found. Make sure a ScoreManager exists in the scene.");
                }

                Destroy(gameObject);
            }
        }
    }
}