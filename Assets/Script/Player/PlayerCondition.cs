using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 데미지를 받을 때 필요한 인터페이스
// 어느 곳에서 사용 가능
public interface IDamagable
{
    void TakePhysicalDamage(int damageAmout);
}

public class PlayerCondition : MonoBehaviour, IDamagable
{
    public UICondition uiCondition;

    Condition health { get { return uiCondition.health; } }
    Condition hunger { get { return uiCondition.hunger; } }
    Condition stamina { get { return uiCondition.stamina; } }

    public float noHungerHealthDecay; // 배고픔이 0일때 사용할 값
    public event Action onTakeDamage; // Damage를 받을 때 호출할 Action

    private void Update()
    {
        hunger.Subtract(hunger.passiveValue * Time.deltaTime);
        stamina.Add(stamina.passiveValue * Time.deltaTime);

        if (hunger.curValue <= 0f)
        {
            health.Subtract(noHungerHealthDecay * Time.deltaTime);
        }

        if (health.curValue <= 0f)
        {
            Die();
        }
    }

    public void Heal(float amount) // 회복 메서드
    {
        health.Add(amount);
    }

    public void Eat(float amount) // 음식 섭취 메서드
    {
        hunger.Add(amount);
    }

    public void Die()
    {
        Debug.Log("플레이어가 죽었다.");
    }

    // 데미지 받을 때 액션 호출
    public void TakePhysicalDamage(int damageAmount)
    {
        health.Subtract(damageAmount);
        onTakeDamage?.Invoke();
    }

    public void TakeBuff(float amount, float buffTime)
    {
        StartCoroutine(BuffTimer(buffTime));
    }

    IEnumerator BuffTimer(float time)
    {
        CharacterManager.Instance.Player.controller.moveSpeed = 10;

        yield return new WaitForSeconds(time);

        CharacterManager.Instance.Player.controller.moveSpeed = 5;
    }
}
