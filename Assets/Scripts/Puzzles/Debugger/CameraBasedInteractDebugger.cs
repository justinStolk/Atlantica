using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBasedInteractDebugger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray screenRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(screenRay, out RaycastHit hit, 100))
            {
                if (hit.transform.TryGetComponent<IInteractable>(out IInteractable interactable))
                {
                    Debug.Log(hit.transform.name);
                    interactable.Interact();
                }
            }
        }
    }
}
