using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CoinManager : MonoBehaviour
{
    public static CoinManager instance;

    public int coins = 0;
    public TMP_Text coinText;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        UpdateCoinText();
    }

    public void AddCoin()
    {
        coins++;
        UpdateCoinText();
    }

    void UpdateCoinText()
    {
        coinText.text = "Coins: " + coins;
    }
}

