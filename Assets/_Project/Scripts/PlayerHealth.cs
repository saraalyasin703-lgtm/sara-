using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    public int health = 3;
    public TMP_Text healthText;

    void Start()
    {
        UpdateHealthText();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name.Contains("Obstacle"))
        {
            health--;
            UpdateHealthText();

            if (health <= 0)
            {
                Debug.Log("Game Over");
            }
        }
    }

    void UpdateHealthText()
    {
        healthText.text = "Health: " + health;
    }
}
