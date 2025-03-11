using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Condition : MonoBehaviour
{
    public float curValue;
    public float maxValue;
    public float startValue;
    public float passiveValue;
    public Image uiBar;
    public GameObject StatBar;

    private void Start()
    {
        curValue = startValue;
    }

    private void Update()
    {
        uiBar.fillAmount = GetPercentage();
        SetUI();
    }

    public void Add(float value)
    {
        // 둘 중 작은 값 적용
        curValue = Mathf.Min(curValue + value, maxValue);
    }

    public void Subtract(float value)
    {
        // 둘 중 큰 값 적용
        curValue = Mathf.Max(curValue - value, 0.0f);
    }

    public float GetPercentage()
    {
        return curValue / maxValue;
    }

    public void ColorChange()
    {
        if (CharacterManager.Instance.Player.condition.uiCondition.stamina.uiBar.fillAmount > 0.7)
        {
            CharacterManager.Instance.Player.condition.uiCondition.stamina.uiBar.color = new Color(255, 255, 255);
        }
        else if (CharacterManager.Instance.Player.condition.uiCondition.stamina.uiBar.fillAmount > 0.4)
        {
            CharacterManager.Instance.Player.condition.uiCondition.stamina.uiBar.color = new Color(255, 255, 0);
        }
        else
        {
            CharacterManager.Instance.Player.condition.uiCondition.stamina.uiBar.color = new Color(255, 0, 0);
        }
    }

    public void SetUI()
    {
        if (CharacterManager.Instance.Player.condition.uiCondition.stamina.curValue == CharacterManager.Instance.Player.condition.uiCondition.stamina.maxValue)
        {
            CharacterManager.Instance.Player.condition.uiCondition.stamina.StatBar.SetActive(false);
        }
        else
        {
            CharacterManager.Instance.Player.condition.uiCondition.stamina.StatBar.SetActive(true);
            ColorChange();
        }
    }
}
