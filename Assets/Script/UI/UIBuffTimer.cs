using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBuffTimer : MonoBehaviour
{
    public Image[] buffUI;
    private Coroutine[] activeBuffCoroutines;


    private void Start()
    {
        activeBuffCoroutines = new Coroutine[buffUI.Length]; // 각 버프 UI에 대한 코루틴 배열 초기화
    }

    private void Update()
    {
        for (int i = 0; i < buffUI.Length; i++)
        {
            if (buffUI[i].sprite != null)
            {
                buffUI[i].gameObject.SetActive(true);
            }
            else
            {
                buffUI[i].gameObject.SetActive(false);
            }
        }
    }

    public void StartBuff(Sprite icon, float buffTime, float amount, BuffType type)
    {
        switch (type)
        {
            case BuffType.Speed:
                break;
            case BuffType.Jump:
                break;
        }
        for (int i = 0; i < buffUI.Length; i++)
        {
            if (buffUI[i].sprite == icon)
            {
                // 기존 코루틴 중단 후 재시작
                if (activeBuffCoroutines[i] != null)
                {
                    StopCoroutine(activeBuffCoroutines[i]);
                    EndBuff(amount, type);
                }

                TakeBuff(amount, type);
                activeBuffCoroutines[i] = StartCoroutine(BuffTimer(amount, buffTime, i, type));
                return;
            }
            else
            {
                if (buffUI[i].sprite == null)
                {
                    buffUI[i].sprite = icon;
                    activeBuffCoroutines[i] = StartCoroutine(BuffTimer(amount, buffTime, i, type));
                    TakeBuff(amount, type);
                    return;
                }
            }
        }
    }
    IEnumerator BuffTimer(float amount, float time, int indexNum, BuffType type)
    {
        buffUI[indexNum].type = Image.Type.Filled;
        buffUI[indexNum].fillAmount = 1;

        float elapsedTime = 0f;
        
        while (elapsedTime < time)
        {   
            buffUI[indexNum].fillAmount = 1 - (elapsedTime/time);

            elapsedTime += Time.deltaTime;
            yield return null;
        }


        EndBuff(amount, type);
        buffUI[(indexNum)].sprite = null;
    }

    void TakeBuff(float amount, BuffType type)
    {
        switch (type)
        {
            case BuffType.Speed:
                CharacterManager.Instance.Player.controller.moveSpeed += amount;
                CharacterManager.Instance.Player.controller.runSpeed += amount;
                CharacterManager.Instance.Player.controller.walkSpeed += amount;
                break;
            case BuffType.Jump:
                CharacterManager.Instance.Player.controller.jumpPower += amount;
                break;
            case BuffType.DoubleJump:
                CharacterManager.Instance.Player.controller.jumpCount += amount;
                break;
        }
    }

    void EndBuff(float amount, BuffType type)
    {
        switch (type)
        {
            case BuffType.Speed:
                CharacterManager.Instance.Player.controller.moveSpeed -= amount;
                CharacterManager.Instance.Player.controller.runSpeed -= amount;
                CharacterManager.Instance.Player.controller.walkSpeed -= amount;
                break;
            case BuffType.Jump:
                CharacterManager.Instance.Player.controller.jumpPower -= amount;
                break;
            case BuffType.DoubleJump:
                CharacterManager.Instance.Player.controller.jumpCount -= amount;
                break;
        }
    }
}
