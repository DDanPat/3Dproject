using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum JumpType
{
    Up,
    foward
}

public class JumpPlatform : MonoBehaviour
{
    public JumpType jumpType;
    private float jumpPadPower;

    private void Start()
    {
        switch (jumpType)
        {
            case JumpType.Up:
                jumpPadPower = 300f;
                break;
            case JumpType.foward:
                break;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        CharacterManager.Instance.Player.controller.JumpPad(jumpPadPower);
    }
}
