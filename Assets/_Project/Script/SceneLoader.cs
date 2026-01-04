using UnityEngine;
using UnityEngine.SceneManagement;
// التحكم بتحميل المشاهد والخروج من اللعبه
public class SceneLoader : MonoBehaviour
{
    public void LoadGame()
    {
        SceneManager.LoadScene("GameScene");
    }
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit pressed");
    }
}
