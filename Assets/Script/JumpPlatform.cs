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

    bool OnJumpPad = false; // 점프대 사용여부


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
