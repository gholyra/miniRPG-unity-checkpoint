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
        Cursor.visible = false;
    }
}
