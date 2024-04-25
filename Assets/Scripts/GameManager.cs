using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public InputManager inputManager;

    private void Awake()
    {
        if(Instance != null) Destroy(this.gameObject);
        
        Instance = this;
        inputManager = new InputManager();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            PauseGame();
        }
    }

    public void PauseGame()
    {
        UIManager.Instance.SetPauseComponent(true);
        Time.timeScale = 0;
        Cursor.visible = true;
    }

    public void UnpauseGame()
    {
        UIManager.Instance.SetPauseComponent(false);
        Time.timeScale = 1;
        Cursor.visible = false;
    }
}
