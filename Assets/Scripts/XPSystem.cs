using System;
using UnityEngine;

public class XPSystem : MonoBehaviour
{
    public static XPSystem Instance;

    [SerializeField] private int limitXP = 5;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Update()
    {
        CheckPlayerXP();
    }

    public int GetLimitXP()
    {
        return this.limitXP;
    }
    
    public void GainExp(int gainedExp)
    {
        PlayerCharacterStatus.Instance.currentXP += gainedExp;
    }

    public void CheckPlayerXP()
    {
        if (PlayerCharacterStatus.Instance.currentXP >= limitXP)
        {
            ChangeLimitXP();
            LevelUP();
        }
    }
    
    public void ChangeLimitXP()
    {
        limitXP += (int)Math.Ceiling(limitXP * 0.5f);
    }

    public void LevelUP()
    {
        PlayerCharacterStatus.Instance.currentLevel++;
        PlayerCharacterStatus.Instance.health.IncreaseHealth(5f);
    }
}
