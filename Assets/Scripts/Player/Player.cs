using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    private CharacterController _controller;
    [SerializeField]
    private float _speed = 5.0f;
    [SerializeField]
    private float _gravity = 1.0f;
    [SerializeField]
    private float _jumpHeight = 15.0f;
    private Vector3 _direction;
    private Vector3 _velocity;
    [SerializeField]
    private float mouseX, mouseY;
    private Camera _camera;
    [SerializeField]
    private float _cameraSensitivity = 2f;

    [SerializeField]
    private bool _jumping;

    void Start()
    {
        _controller = GetComponent<CharacterController>();
        _camera = Camera.main;
        if(_camera == null)
        {
            Debug.LogError("Main Camera is null");
        }
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        CalculateMovement();
        MouseLook();
        CursorView();
    }

    void CalculateMovement()
    {
        if (_controller.isGrounded == true)
        {
            float _horizontalInput = Input.GetAxis("Horizontal");
            float _verticalInput = Input.GetAxis("Vertical");
            _direction = new Vector3(_horizontalInput, 0, _verticalInput);
            _velocity = _direction * _speed;
            _velocity = transform.TransformDirection(_velocity);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                _velocity.y = _jumpHeight;
            }
        }
        
        _velocity.y -= _gravity * Time.deltaTime;

        

        _controller.Move(_velocity * Time.deltaTime);
    }

    void MouseLook()
    {
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");

        //look left and right
        Vector3 currentRotation = transform.rotation.eulerAngles;
        currentRotation.y += mouseX * _cameraSensitivity;
        transform.rotation = Quaternion.Euler(0,currentRotation.y, 0);

        //look up and down
        Vector3 currentCameraRotation = _camera.transform.rotation.eulerAngles;
        currentCameraRotation.x -= mouseY * _cameraSensitivity;
        if (currentCameraRotation.x < 180 && currentCameraRotation.x > 0)
        {
            currentCameraRotation.x = Mathf.Clamp(currentCameraRotation.x, 0, 28);
        }
        else if (currentCameraRotation.x < 360 && currentCameraRotation.x > 180)
        {
            currentCameraRotation.x = Mathf.Clamp(currentCameraRotation.x, 270, 360);
        }
        _camera.transform.rotation = Quaternion.Euler(currentCameraRotation.x, currentRotation.y, 0);
    }

    void CursorView()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }


   


}
