using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerInteract : MonoBehaviour
{
    public LayerMask layermask;
    public TMP_Text InteractText;
    public bool PlayerActive;

    [SerializeField] private float sphereRadius;
    [SerializeField] private float maxDistance;


    private PlayerManager playerManager;
    private bool interactionHit;
    private RaycastHit hit;
    private Vector3 origin;
    private Vector3 direction;
    private float currentHitDistance;

    // Start is called before the first frame update
    void Start()
    {
        playerManager = GetComponent<PlayerManager>();
        playerManager.playerActionsAsset.Player.Interact.started += InteractWith;
        InteractText.gameObject.SetActive(false);
        interactionHit = false;
        PlayerActive = true;
    }

    public void Update()
    {
        origin = transform.position;
        direction = transform.forward;

         
        if(PlayerActive == true)
        {

            if (Physics.SphereCast(origin, sphereRadius, direction, out hit, maxDistance))
            {
                currentHitDistance = hit.distance;
                Debug.Log(currentHitDistance);
                interactionHit = true;
                InteractText.gameObject.SetActive(true);
            }
            else
            {
                Debug.Log("NOTHING");
                interactionHit = false;
                InteractText.gameObject.SetActive(false);

            }
        }
    }

    private void InteractWith(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {

        if (interactionHit == true)
        {

            if (hit.transform.tag == "Backpack")
            {
                Debug.Log("BACKPACK SPOTTED");
                EventSystem.CallEvent(EventSystem.EventType.ON_BACKPACK_TAKE);
            }



            if (hit.collider.gameObject.GetComponentInParent<IInteractable>() != null)
            {
                Debug.Log("IINTERACTABLE SPOTTED");
                hit.collider.gameObject.GetComponentInParent<IInteractable>().Interact();
            }
        }


    }
    
    

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Debug.DrawLine(origin, origin + direction * currentHitDistance);
        Gizmos.DrawWireSphere(origin + direction * currentHitDistance, sphereRadius);
    }

}
