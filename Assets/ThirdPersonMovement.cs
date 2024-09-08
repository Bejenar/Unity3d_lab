using System;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    [SerializeField]
    private float speed = 5;

    [SerializeField]
    private float jumpPower = 5;

    [SerializeField]
    private float sensX = 10;

    [SerializeField]
    private float sensY = 10;


    [SerializeField]
    private Transform lookAt;

    private Rigidbody _rigidbody;
    private Vector3 movement = Vector3.zero;

    private float rotationX;
    private float rotationY;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        var inputX = Input.GetAxis("Horizontal");
        var inputZ = Input.GetAxis("Vertical");

        var mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        var mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        rotationY += mouseX;
        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);
        
        movement = new Vector3(inputX, 0, inputZ).normalized;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Jump");
            Jump();
        }
    }

    private void LateUpdate()
    {
        lookAt.rotation = Quaternion.Euler(rotationX, rotationY, 0);
    }

    private void Jump()
    {
        _rigidbody.AddForce(transform.up * jumpPower, ForceMode.Impulse);
    }

    private void FixedUpdate()
    {
        movement = lookAt.right * movement.x + lookAt.forward * movement.z;
        _rigidbody.AddForce(movement * speed);
    }
}