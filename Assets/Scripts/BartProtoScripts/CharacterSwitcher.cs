using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSwitcher : MonoBehaviour
{
    public FirstPersonMove activeScript;
    private FirstPersonMove playerScript;
    private FirstPersonMove rogZakScript;

    public GameObject activeObject;
    public GameObject player;
    public GameObject rogZak;

    private GameObject playerCam;
    private GameObject rogZakCam;

    public float interactLenght;

    public GameObject playerCanvas;
    public GameObject backpackCanvas;

    public GameObject backpackLocation;

    private bool isOnBack;

    void Start()
    {
        playerScript = player.GetComponent<FirstPersonMove>();
        rogZakScript = rogZak.GetComponent<FirstPersonMove>();

        playerCam = player.transform.GetChild(0).gameObject;
        rogZakCam = rogZak.transform.GetChild(0).gameObject;

        isOnBack = false;

        CheckActive();
    }

    void Update()
    {
        Backpack();
    }

    void CheckActive()
    {
        if (rogZakScript.playerInControl == true)
        {
            activeScript = rogZakScript;
            activeObject = rogZak;
        }
        else if (playerScript.playerInControl == true)
        {
            activeScript = playerScript;
            activeObject = player;
        }
        else
        {
            Debug.Log("Two active players");
        }
    }

    void switchActive()
    {
        if (activeScript == playerScript)
        {
            Debug.Log("ROGZAK GO");
            playerScript.playerInControl = false;
            rogZakScript.playerInControl = true;

            playerCam.SetActive(false);
            rogZakCam.SetActive(true);

            CheckActive();
        }
        else if (activeScript == rogZakScript)
        {
            Debug.Log("PLAYER GO");
            playerScript.playerInControl = true;
            rogZakScript.playerInControl = false;

            playerCam.SetActive(true);
            rogZakCam.SetActive(false);

            CheckActive();
        }
    }

    void Backpack()
    {
        RaycastHit hit;
        if (Physics.Raycast(activeObject.transform.position, activeObject.transform.TransformDirection(Vector3.forward), out hit, interactLenght))
        {
            if (hit.transform.gameObject.name == "Player")
            {
                playerCanvas.SetActive(true);
                backpackCanvas.SetActive(false);

                if (Input.GetKeyDown(KeyCode.E))
                {
                    playerCanvas.SetActive(false);
                    rogZak.transform.position = backpackLocation.transform.position;
                    rogZak.transform.SetParent(backpackLocation.transform.parent);
                    rogZak.GetComponent<Rigidbody>().gameObject.SetActive(false);
                    switchActive();
                    isOnBack = true;
                    playerScript.canDoubleJump = true;
                }
                return;
            }

            if (hit.transform.gameObject.name == "Rogzak")
            {
                playerCanvas.SetActive(false);
                backpackCanvas.SetActive(true);

                if (Input.GetKeyDown(KeyCode.E))
                {
                    backpackCanvas.SetActive(false);
                    rogZak.transform.position = backpackLocation.transform.position;
                    rogZak.transform.SetParent(backpackLocation.transform.parent);
                    rogZak.GetComponent<Rigidbody>().gameObject.SetActive(false);
                    isOnBack = true;
                    playerScript.canDoubleJump = true;
                }
                return;
            }
        }
        else
        {
            playerCanvas.SetActive(false);
            backpackCanvas.SetActive(false);

            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("E");
                if (isOnBack == false)
                {
                    switchActive();
                }
                else if (isOnBack == true)
                {
                    dropBackpack();
                }
            }
        }
    }

    void dropBackpack()
    {
        rogZak.transform.parent = null;
        isOnBack = false;
        rogZak.GetComponent<Rigidbody>().gameObject.SetActive(true);
        playerScript.canDoubleJump = false;
        switchActive();
    }
}
