using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int health = 3;
    public TMP_Text healthText;

    AudioSource hitSound;
    bool canTakeHit = true;

    void Start()
    {
        hitSound = GetComponent<AudioSource>();
        UpdateHealthText();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!canTakeHit) return;

        if (collision.gameObject.name.Contains("Obstacle"))
        {
            canTakeHit = false;

            if (hitSound != null)
                hitSound.Play();

            health = Mathf.Max(health - 1, 0);
            UpdateHealthText();

            if (health <= 0)
            {
                SceneManager.LoadScene("GameOver");
            }
            else
            {
                Invoke(nameof(EnableHit), 0.5f);
            }
        }
    }

    void EnableHit()
    {
        canTakeHit = true;
    }

    void UpdateHealthText()
    {
        healthText.text = "Health: " + health;
    }
}
