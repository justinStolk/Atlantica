using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BackPackInteract : MonoBehaviour
{
    public PlayerManager PlayerManager;
    public TMP_Text InteractText;
    public bool BackpackActive;

    [SerializeField] private float sphereRadius;
    [SerializeField] private float maxDistance;

    private bool interactionHit;
    private RaycastHit hit;
    private Vector3 origin;
    private Vector3 direction;
    private float currentHitDistance;

    // Start is called before the first frame update
    void Start()
    {
        PlayerManager.PlayerActionsAsset.Backpack.Interact.started += InteractWith;
        InteractText.gameObject.SetActive(false);
        interactionHit = false;
        BackpackActive = false;

    }

    // Update is called once per frame
    void Update()
    {
        origin = transform.position;
        direction = transform.up;

        BackpackActive = !PlayerManager.PlayerInteract.PlayerActive;

        if (BackpackActive)
        {

            if (Physics.SphereCast(origin, sphereRadius, direction, out hit, maxDistance))
            {
                currentHitDistance = hit.distance;
                interactionHit = true;

                if (hit.transform.tag == "Player")
                {
                    InteractText.gameObject.SetActive(true);
                }

                if (hit.collider.gameObject.GetComponentInParent<IInteractable>() != null)
                {
                    InteractText.gameObject.SetActive(true);
                }
            }
            else
            {
                interactionHit = false;
                InteractText.gameObject.SetActive(false);
            }
        }
    }



    private void InteractWith(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {

        if (interactionHit == true)
        {

            if (hit.transform.tag == "Player")
            {
                Debug.Log("BACKPACK SPOTTED");
                EventSystem.CallEvent(EventSystem.EventType.ON_BACKPACK_TAKE);
                PlayerManager.SwitchToPlayer(obj);
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
