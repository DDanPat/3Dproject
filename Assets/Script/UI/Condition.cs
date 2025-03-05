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

    private void Start()
    {
        curValue = startValue;
    }

    private void Update()
    {
        uiBar.fillAmount = GetPercentage();
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
}
