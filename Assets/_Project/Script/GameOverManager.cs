using UnityEngine;
using UnityEngine.SceneManagement;
// التحكم بالمشهد عند انتهاء اللعبه
public class GameOverManager : MonoBehaviour
{
    public void RestartGame()
    {// اعاده تحميل المشهد 
        SceneManager.LoadScene("GameScene");
    }
    public void GoToMenu()
    {// الذهاب الى القائمه الرئيسيه 
        SceneManager.LoadScene("MainMenu");
    }
}

