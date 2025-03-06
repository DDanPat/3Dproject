using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    public Rigidbody _rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        Vector3 jumpForce = Vector3.up * 10f; // 위로 점프하는 힘
        Vector3 forwardForce = transform.forward * 100f; // 앞으로 가는 힘

        _rigidbody.AddForce(jumpForce, ForceMode.Impulse);
        _rigidbody.AddForce(forwardForce, ForceMode.Force); // 지속적인 힘 적용
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
