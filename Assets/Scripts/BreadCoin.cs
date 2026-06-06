using UnityEngine;

namespace MTHermanTF2
{
    public class BreadCoin : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                Debug.Log("Bread collected!");

                if (global::ScoreManager.instance != null)
                {
                    global::ScoreManager.instance.AddScore(1);
                }
                else
                {
                    Debug.LogError("ScoreManager instance not found. Add a ScoreManager object to the scene.");
                }

                Destroy(gameObject);
            }
        }
    }
}