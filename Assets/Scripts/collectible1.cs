using UnityEngine;

public class Collectible : MonoBehaviour
{
    public int value = 1;  // The value of the collectible, you can change this based on your needs

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Hook"))
        {
            // Add to the player's score or inventory
            GameManager.instance.AddScore(value);

            // Destroy the collectible object
            Destroy(gameObject);
        }
    }
}
