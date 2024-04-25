using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;   
    
    [SerializeField] private TextMeshProUGUI xpText;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private GameObject pauseScreen;
    [SerializeField] private GameObject gameOverScreen;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        SetPauseComponent(false);
    }

    private void Update()
    {
        SetXPText();
        SetLevelText();
    }

    public void SetXPText()
    {
        xpText.text = PlayerCharacterStatus.Instance.currentXP.ToString() + "/" + XPSystem.Instance.GetLimitXP();
    }
    
    public void SetLevelText()
    {
        levelText.text = PlayerCharacterStatus.Instance.currentLevel.ToString();
    }

    public void SetPauseComponent(bool isActive)
    {
        pauseScreen.gameObject.SetActive(isActive);
    }
}
