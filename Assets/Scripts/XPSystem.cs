using UnityEngine;

public class XPSystem : MonoBehaviour
{
    private int actualExp;

    public void GainExp(int gainedExp)
    {
        actualExp += gainedExp;
    }
}
