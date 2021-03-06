using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserShooter : MonoBehaviour
{
    [SerializeField] private Material laserMaterial;
    [SerializeField] private float maxLightRayDistance;
    [SerializeField] private LayerMask beamLayerMask;

    private LaserBeam laserBeam;

    // Start is called before the first frame update
    void Start()
    {
        laserBeam = new LaserBeam(transform.position, transform.forward, maxLightRayDistance, laserMaterial, beamLayerMask);
        
    }

    // Update is called once per frame
    void Update()
    {
        laserBeam.EvaluateLaser();
    }

}
