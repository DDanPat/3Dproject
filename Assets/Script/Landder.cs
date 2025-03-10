using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Landder : MonoBehaviour
{
    bool useLandder = false;
    private void Update()
    {
        Debug.Log("useLandder" + useLandder);
        if (useLandder)
        {
            CharacterManager.Instance.Player.controller.UseLandder();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        useLandder = true;
        CharacterManager.Instance.Player.controller.isUseObject = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        useLandder = false;
        CharacterManager.Instance.Player.controller.isUseObject = false;
        CharacterManager.Instance.Player.controller.UseGrabity();
    }

}
