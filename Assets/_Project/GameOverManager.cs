using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverManager : MonoBehaviour
{
    // هذا المربع سنربط فيه نص النتيجة داخل Unity
    public TextMeshProUGUI finalScoreText;

    void Start()
    {
        // استرجاع النتيجة التي خزنها اللاعب عند الموت
        int score = PlayerPrefs.GetInt("FinalScore", 0);
        finalScoreText.text = "Final Score: " + score;
    }

    // وظيفة زر إعادة اللعب
    public void RestartGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    // وظيفة زر القائمة الرئيسية
    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
