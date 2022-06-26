using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class ThirdPersonCamera : MonoBehaviour
{
    public Rigidbody Rb;
    public PlayerManager PlayerManager;



    private void Start()
    {

    }

    public void PlayerLookAt()
    {
        Vector3 direction = Rb.velocity;
        direction.y = 0f;

        if (PlayerManager.Move.ReadValue<Vector2>().sqrMagnitude > 0.1f && direction.sqrMagnitude > 0.1f)
        {
            this.Rb.rotation = Quaternion.LookRotation(direction, Vector3.up);
        }
        else
        {
            Rb.angularVelocity = Vector3.zero;
        }
    }

    

    public Vector3 GetCameraForward(CinemachineFreeLook camera)
    {
        Vector3 forward = camera.transform.forward;
        forward.y = 0;
        return forward.normalized;
    }

    public Vector3 GetCameraRight(CinemachineFreeLook camera)
    {
        Vector3 right = camera.transform.right;
        right.y = 0;
        return right.normalized;
    }
}
