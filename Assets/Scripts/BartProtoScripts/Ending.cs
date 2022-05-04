using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ending : MonoBehaviour
{
    private bool playerAtEnd;

    public Vector3 offset;

    public float playerDistance;

    public LayerMask playerMask;

    public GameObject endCanvas;

    void Start()
    {

    }

    void Update()
    {
        playerAtEnd = Physics.CheckSphere(transform.position + offset, playerDistance, playerMask);
        if (playerAtEnd)
        {
            endCanvas.SetActive(true);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position + offset, playerDistance);
    }
}
