using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    private Transform floorLevel;
    private BoxCollider col;

    private void Start()
    {
        floorLevel = transform.GetChild(0);
        col = GetComponent<BoxCollider>();

        AdjustCollider();
    }

    public void AdjustCollider()
    {
        float waterSize = Vector3.Distance(transform.position, floorLevel.position);
        float waterCentre = waterSize / 2;

        col.size = new Vector3(col.size.x, waterSize + 0.1f , col.size.z);
        col.center = new Vector3(0, -waterCentre, 0);
    }
    
}
