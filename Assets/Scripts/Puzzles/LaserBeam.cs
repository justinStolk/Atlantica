using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBeam
{
    private GameObject laserObject;
    private LineRenderer laser;
    private Vector3 position, direction;
    private List<Vector3> laserPoints = new();
    private List<Transform> reflectorPoints = new();
    private List<Vector3> reflectorRotations = new();

    public LaserBeam(Vector3 laserPosition, Vector3 laserDirection, Material laserMaterial)
    {
        laserObject = new GameObject("Laser", typeof(LineRenderer));
        laser = laserObject.GetComponent<LineRenderer>();
        position = laserPosition;
        direction = laserDirection;

        laser.startWidth = 0.1f;
        laser.endWidth = 0.1f;
        laser.startColor = Color.red;
        laser.endColor = Color.yellow;
        laser.material = laserMaterial;

        CastLaser(position, direction);
    }

    void CastLaser(Vector3 pos, Vector3 dir)
    {
        laserPoints.Add(pos);

        Ray ray = new Ray(pos, dir);
        if(Physics.Raycast(ray, out RaycastHit hit, 50))
        {
            if (hit.transform.CompareTag("Reflector"))
            {
                Vector3 point = hit.point;
                Vector3 reflectedDirection = Vector3.Reflect(dir, hit.normal);
                laserPoints.Add(point);
                reflectorPoints.Add(hit.transform);
                reflectorRotations.Add(hit.transform.eulerAngles);
                CastLaser(point, reflectedDirection);
            }else if(hit.transform.CompareTag("LaserTarget"))
            {
                laserPoints.Add(hit.point);
                laser.startColor = Color.green;
                laser.endColor = Color.green;
                laser.startWidth = 0.5f;
                laser.endWidth = 0.5f;
                UpdateLaser();
            }
            else
            {
                laserPoints.Add(hit.point);
                UpdateLaser();
            }
        }
        else
        {
            laserPoints.Add(ray.GetPoint(50));
            UpdateLaser();
        }



    }

    void UpdateLaser()
    {
        int count = 0;
        laser.positionCount = laserPoints.Count;


        foreach (Vector3 laserPoint in laserPoints)
        {
            laser.SetPosition(count, laserPoint);
            count++;
        }
    }


    void RecastLaser()
    {
        laser.startWidth = 0.1f;
        laser.endWidth = 0.1f;
        laser.startColor = Color.red;
        laser.endColor = Color.yellow;

        laserPoints.Clear();
        reflectorPoints.Clear();
        reflectorRotations.Clear();
        CastLaser(position, direction);
    }

    public void EvaluateLaser()
    {
        //Fix multireflection blocking issue with for-loop here
        Ray ray = new Ray(position, direction);
        if(Physics.Raycast(ray, out RaycastHit hit, 50))
        {
            if (!hit.transform.CompareTag("Reflector"))
            {
                RecastLaser();
                return;
            }
            else
            {
                if (!laserPoints.Contains(hit.point))
                {
                    RecastLaser();
                    return;
                }
            }
        }

        for(int i = 0; i < reflectorPoints.Count; i++)
        {
            Transform reflectorPoint = reflectorPoints[i];
            if(reflectorPoint.eulerAngles != reflectorRotations[i])
            {
                RecastLaser();
            }
        }
    }

}