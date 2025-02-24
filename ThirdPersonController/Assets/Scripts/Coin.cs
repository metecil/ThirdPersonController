using UnityEngine;

public class Coin : MonoBehaviour {
    public float rotationSpeed = 100f;

    void Update() {
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }

    // When the player collects the coin...
    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            // Notify the GameManager that a coin was collected.
            GameManager.instance.AddCoin();
            Destroy(gameObject);
        }
    }
}

