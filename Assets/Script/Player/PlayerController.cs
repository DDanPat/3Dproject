using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public float walkSpeed;
    public float runSpeed;
    private bool isRuning = false; // 달리기 사용 여부
    private Vector2 curMovementInput;
    public float jumpPower;
    public float jumpCount; // 점프 횟수
    public float curJumpCount; // 점프 사용 남은 횟수
    public LayerMask groundLayerMask;
    public float jumpStamina;
    public float runStamina;

    [Header("Look")]
    public Transform cameraContainer;
    public float minXLook;
    public float maxXLook;
    private float camCurXRot;
    public float lookSensitivity;

    public Action inventory;
    private Vector2 mouseDelta;

    [HideInInspector]
    public bool canLook = true;

    public bool isUseObject = false;
    private Rigidbody _rigidbody;
    private Animator _animator;


    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponentInChildren<Animator>();
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        curJumpCount = jumpCount;
    }

    private void FixedUpdate()
    {
        Move();
        if (isRuning && curMovementInput != Vector2.zero)
        {
            CharacterManager.Instance.Player.condition.UseStamina(runStamina);
            moveSpeed = runSpeed;
        }    
        else moveSpeed = walkSpeed;

        _animator.SetBool("isRunning", isRuning && curMovementInput != Vector2.zero);
        Debug.Log(isUseObject);
    }

    private void LateUpdate()
    {
        if (canLook)
        {
            CameraLook();
        }
    }

    public void OnLookInput(InputAction.CallbackContext context)
    {
        mouseDelta = context.ReadValue<Vector2>();
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        IsGrounded();
        if (context.phase == InputActionPhase.Performed)
        {
            curMovementInput = context.ReadValue<Vector2>();
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            curMovementInput = Vector2.zero;
        }
        _animator.SetBool("isMoving", curMovementInput != Vector2.zero);
    }

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && IsGrounded())
        {
            if (CharacterManager.Instance.Player.condition.UseStamina(jumpStamina))
            {
                _rigidbody.AddForce(Vector2.up * jumpPower, ForceMode.Impulse);
                curJumpCount--;
            } 
        }
    }

    public void OnRunInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            if (CharacterManager.Instance.Player.condition.UseStamina(runStamina))
            {
                isRuning = true;
            }
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            isRuning = false;
        }
    }

    private void Move()
    {
        if (isUseObject) return;
        Vector3 dir = transform.forward * curMovementInput.y + transform.right * curMovementInput.x;
        dir *= moveSpeed;
        dir.y = _rigidbody.velocity.y;

        _rigidbody.velocity = dir;
    }

    void CameraLook()
    {
        camCurXRot += mouseDelta.y * lookSensitivity;
        camCurXRot = Mathf.Clamp(camCurXRot, minXLook, maxXLook);
        cameraContainer.localEulerAngles = new Vector3(-camCurXRot, 0, 0);

        transform.eulerAngles += new Vector3(0, mouseDelta.x * lookSensitivity, 0);
    }

    bool IsGrounded() // 레이케스트를 책상 다리처럼 만들어 사용
    {
        Ray[] rays = new Ray[4]
        {
            new Ray(transform.position + (transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (transform.right * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.right * 0.2f) +(transform.up * 0.01f), Vector3.down)
        };

        for (int i = 0; i < rays.Length; i++)
        {
            if (Physics.Raycast(rays[i], 0.1f, groundLayerMask))
            {
                isUseObject = false;
                curJumpCount = jumpCount;
                return true;
            }
        }

        if (curJumpCount > 0)
        {
            return true;
        }

        return false;
    }

    public void ToggleCursor(bool toggle) // 마우스 숨기기
    {
        Cursor.lockState = toggle ? CursorLockMode.None : CursorLockMode.Locked;
        canLook = !toggle;
    }

    public void OnInventory(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Started)
        {
            inventory?.Invoke();
            ToggleCursur();
        }
    }

    void ToggleCursur()
    {
        bool toggle = Cursor.lockState == CursorLockMode.Locked;
        Cursor.lockState = toggle? CursorLockMode.None : CursorLockMode.Locked;
        canLook = !toggle;
    }

    //점프대 type 별로 위로 점프하는 힘을 부여하거나 플레이어가 바라보는 방향으로 힘을 부여
    public void JumpPad(float jumpValue, JumpType type)
    {
        Vector3 jumpDirection = Vector3.zero; // 기본값 (0,0,0)

        switch (type)
        {
            case JumpType.Up:
                jumpDirection = Vector3.up;
                break;

            case JumpType.forward:
                isUseObject = true;
                jumpDirection = (transform.forward + Vector3.up).normalized;
                break;
        }

        _rigidbody.AddForce(jumpDirection * jumpValue, ForceMode.Impulse);
    }

    

    //사다리 사용
    public void UseLandder()
    {
        _rigidbody.useGravity = false;

        if (curMovementInput.y > 0) // 올라가기
        {
            _rigidbody.velocity = new Vector3(0, moveSpeed * 0.5f, 0);
        }
        else if (curMovementInput.y < 0) // 내려가기
        {
            _rigidbody.velocity = new Vector3(0, -moveSpeed * 0.5f, 0);
        }
        else // 중간에 매달리기
        {
            _rigidbody.velocity = Vector3.zero;
        }

    }

    //중력 부여
    public void UseGrabity()
    {
        _rigidbody.useGravity = true ;
    }
}
