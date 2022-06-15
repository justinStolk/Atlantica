using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleLightTrigger : MonoBehaviour
{
    public GameObject LightInside;
    public GameObject LightOutside;

    public bool isTriggered = false;
    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" && isTriggered == false)
        {
            if(LightInside.activeSelf == false)
            {
                LightInside.SetActive(true);
            }
            else
            {
                LightInside.SetActive(false);
            }
            if (LightOutside.activeSelf == false)
            {
                LightOutside.SetActive(true);
            }
            else
            {
                LightOutside.SetActive(false);
            }

            isTriggered = true;
            
            StartCoroutine(ResetTrigger());
        }
    }

    IEnumerator ResetTrigger()
    {
        yield return new WaitForSeconds(2f);
        isTriggered = false;
    }
}
