using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum JumpType
{
    Up,
    forward
}

public class JumpPlatform : MonoBehaviour, IInteractable
{
    public JumpType jumpType;
    public float jumpPadPower;

    public string jumpPadName;
    public string info;

    bool OnJumpPad = false;


    private void Update()
    {
        if (OnJumpPad &&Input.GetKeyDown(KeyCode.E))
        {
            CharacterManager.Instance.Player.controller.JumpPad(jumpPadPower, jumpType);
        }
    }

    private void OnCollisionEnter(Collision _collision)
    {
        OnJumpPad = true;
        // 상호작용 키를 눌러 작동 이라는 안내말 출력
    }

    private void OnCollisionExit(Collision collision)
    {
        OnJumpPad = false ;
    }

    public string GetInteractPrompt()
    {
        string str = $"[E]눌러 사용하기\n{jumpPadName}\n{info}";
        return str;
    }

    public void OnInteract()
    {
        return;
    }
}
