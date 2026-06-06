using UnityEngine;

namespace MTHermanTF2
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

                if (ScoreManager.instance != null)
                {
                    ScoreManager.instance.AddScore(1);
                }
                else
                {
                    Debug.LogError("ScoreManager instance not found. Make sure ScoreManager exists in the scene.");
                }

                Destroy(gameObject);
            }
        }
    }
}