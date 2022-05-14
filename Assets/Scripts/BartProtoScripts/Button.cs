using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Button : MonoBehaviour
{
    private bool playerOnButton;
    private bool buttonState;

    public float playerDistance;

    public GameObject finishLine;

    public LayerMask playerMask;

    public Vector3 offset;

    public UnityEvent ending;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        playerOnButton = Physics.CheckSphere(transform.position + offset, playerDistance, playerMask);
        if (playerOnButton && !buttonState)
        {
            finishLine.SetActive(true);
            buttonState = true;
            ending.Invoke();
        }
        else if (!playerOnButton && buttonState)
        {
            finishLine.SetActive(false);
            buttonState = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position + offset, playerDistance);
    }
}
