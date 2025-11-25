using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float _speed = 25.0f;
    [SerializeField] float _jumpSpeed = 8.0f;
    [SerializeField] float _gravity = 20.0f;
    [SerializeField] float _sensitivity = 5f;
    CharacterController _controller;
    float _horizontal, _vertical;
    float _mouseX, _mouseY;
    bool _jump;
    void Awake()
    {
        _controller = GetComponent<CharacterController>();
    }
    void Update()
    {
        _horizontal = Input.GetAxis("Horizontal");
        _vertical = Input.GetAxis("Vertical");
        _mouseX = Input.GetAxis("Mouse X");
        _mouseY = Input.GetAxis("Mouse Y");
        _jump = Input.GetButton("Jump");
    }
    void FixedUpdate()
    {
        Vector3 moveDirection = Vector3.zero;
        if (_controller.isGrounded)
        {
            moveDirection = new Vector3(_horizontal, 0, _vertical);
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= _speed;

            if (_jump)
                moveDirection.y = _jumpSpeed;
        }

        float turner = _mouseX * _sensitivity;
        if (turner != 0)
        {
            transform.eulerAngles += new Vector3(0, turner, 0);
        }

        float looker = -_mouseY * _sensitivity;
        if (looker != 0)
        {
            transform.eulerAngles += new Vector3(looker, 0, 0);
        }

        moveDirection.y -= _gravity * Time.deltaTime;
        _controller.Move(moveDirection * Time.deltaTime);
    }
}