using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public LayerMask layermask;

    [SerializeField] private float sphereRadius;
    [SerializeField] private float maxDistance;


    private PlayerManager playerManager;

    private Vector3 origin;
    private Vector3 direction;
    private float currentHitDistance;

    // Start is called before the first frame update
    void Start()
    {
        playerManager = GetComponent<PlayerManager>();
        playerManager.playerActionsAsset.Player.Interact.started += RetrieveBackpack;
    }

    private void RetrieveBackpack(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        origin = transform.position;
        direction = transform.forward;

        RaycastHit hit;

        if (Physics.SphereCast(origin, sphereRadius, direction, out hit, maxDistance))
        {
            Debug.Log(hit);
            currentHitDistance = hit.distance;
            Debug.Log(currentHitDistance);
            if(hit.transform.tag == "Backpack")
            {
                EventSystem.CallEvent(EventSystem.EventType.ON_BACKPACK_TAKE);
            }
        }
        else
        {
            Debug.Log("NOTHING");
        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Debug.DrawLine(origin, origin + direction * currentHitDistance);
        Gizmos.DrawWireSphere(origin + direction * currentHitDistance, sphereRadius);
    }

}
