using System;
using Cinemachine;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerMovement : MonoBehaviour {
    private float maxMovementSpeed = 1400f;
    private float jumpForce = 6f;
    private float turnSensitivity = 3f;
    
    private CinemachineVirtualCamera _camera;
    private Transform horizontalTransform;
    private Transform verticalTransform;
    private Rigidbody rb;

    private bool onGround = true;
    private float _xRotation = 0.0f;
    void Start() {
        horizontalTransform = transform;
        _camera = transform.GetComponentInChildren<CinemachineVirtualCamera>();
        verticalTransform = _camera.gameObject.transform;
        rb = GetComponent<Rigidbody>();
    }

    void Update() {
        #region Movement
        var moveX = Input.GetAxis("Horizontal");
        var moveZ = Input.GetAxis("Vertical");

        var movement = transform.TransformDirection(new Vector3(moveX, 0f, moveZ) * (maxMovementSpeed * Time.deltaTime));

        rb.velocity = new Vector3(movement.x, rb.velocity.y, movement.z);
        
        if (Input.GetKeyDown(KeyCode.Space) && onGround)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
        
        //rb.velocity *= 0.95f; //Apply drag
        #endregion

        #region Rotation
        var mouseX = Input.GetAxis("Mouse X") * turnSensitivity;
        var mouseY = Input.GetAxis("Mouse Y") * turnSensitivity;
        
        horizontalTransform.Rotate(0.0f, mouseX, 0.0f, Space.World);
        
        _xRotation -= mouseY;
        _xRotation = Mathf.Clamp(_xRotation, -90.0f, 90.0f);
        verticalTransform.localRotation = Quaternion.Euler(_xRotation, 0.0f, 0.0f);
        #endregion
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Ground")) {
            onGround = true;
        }
    }

    private void OnCollisionExit(Collision other) {
        if (other.gameObject.CompareTag("Ground")) {
            onGround = false;
        }
    }
}
