using UnityEngine;

public class SceneManager : MonoBehaviour
{
    public static SceneManager Instance;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Update()
    {
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name.Contains("Menu"))
        {
            Cursor.visible = true;
        }
    }

    public void LoadGameplayScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Scenes/Gameplay");
    }
    
    public void LoadMenuScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Scenes/MenuScene");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
