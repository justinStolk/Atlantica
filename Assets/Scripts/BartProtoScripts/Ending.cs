using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Ending : MonoBehaviour
{
    private bool playerAtEnd;

    public Vector3 offset;

    public float playerDistance;

    public LayerMask playerMask;

    public GameObject endCanvas;

    public UnityEvent endEvent;

    void Start()
    {

    }

    void Update()
    {
        playerAtEnd = Physics.CheckSphere(transform.position + offset, playerDistance, playerMask);
        if (playerAtEnd)
        {
            endCanvas.SetActive(true);
            endEvent.Invoke();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position + offset, playerDistance);
    }
}
