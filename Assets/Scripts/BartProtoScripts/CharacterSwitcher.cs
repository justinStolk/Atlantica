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

    void Start()
    {
        playerScript = player.GetComponent<FirstPersonMove>();
        rogZakScript = rogZak.GetComponent<FirstPersonMove>();

        playerCam = player.transform.GetChild(0).gameObject;
        rogZakCam = rogZak.transform.GetChild(0).gameObject;

        CheckActive();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("E");
            switchActive();
        }
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
}
