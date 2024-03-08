using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class CameraMoveRight: MonoBehaviour
{
    private const float moveSpeed = 7.5f;
    private const float cameraSpeed = 3.0f;
    bool m_RotationBound;

    public InputActionProperty AimRotation;

    public Quaternion TargetRotation { private set; get; }

    private Vector3 moveVector = Vector3.zero;
    private float moveY = 0.0f;

    private new Rigidbody rigidbody;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;

        TargetRotation = transform.rotation;
    }

    private void Update()
    {
        var rotation = AimRotation.action.ReadValue<Quaternion>();
        Debug.Log(rotation);
        var targetEuler = TargetRotation.eulerAngles + new Vector3(rotation.x, rotation.y, rotation.z);
        if (targetEuler.x > 180.0f)
        {
            targetEuler.x -= 360.0f;
        }
        targetEuler.x = Mathf.Clamp(targetEuler.x, -75.0f, 75.0f);
        TargetRotation = Quaternion.Euler(targetEuler);
    }

    public void ResetTargetRotation()
    {
        TargetRotation = Quaternion.LookRotation(transform.forward, Vector3.up);
    }
}