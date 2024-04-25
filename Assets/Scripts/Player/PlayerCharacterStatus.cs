using UnityEngine;

public class PlayerCharacterStatus : MonoBehaviour
{
    public static PlayerCharacterStatus Instance;

    public int currentXP { get; set; } = 0;
    public int currentLevel { get; set; } = 1;

    public Health health;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        health = GetComponent<Health>();
    }
}
