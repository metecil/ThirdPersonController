using UnityEngine;
using TMPro; // Use TMPro if you prefer TextMeshPro

public class GameManager : MonoBehaviour {
    public static GameManager instance;
    public int coinCount = 0;
    [SerializeField] private TextMeshProUGUI coinText;  // Assign your UI Text element here

    void Awake() {
        if (instance == null) {
            instance = this;
            // Optionally, if you want the GameManager to persist between scenes:
            // DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }

    public void AddCoin() {
        coinCount++;
        UpdateCoinUI();
    }

    void UpdateCoinUI() {
        if (coinText != null)
            coinText.text = "Coins: " + coinCount;
    }
}
