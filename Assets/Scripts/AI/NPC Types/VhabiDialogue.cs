using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class VhabiDialogue : MonoBehaviour, IInteractable
{
    public string nodeToTrigger;

    [SerializeField] private YarnProject yarnProject;
    [SerializeField] private Animator anim;

    private DialogueRunner dialogueRunner;

    void Start()
    {
        dialogueRunner = FindObjectOfType<DialogueRunner>();
        dialogueRunner.dialogueViews = FindObjectsOfType<DialogueViewBase>();
        dialogueRunner.onDialogueComplete.AddListener(() => anim.SetBool("isTalking", false));
        dialogueRunner.onDialogueComplete.AddListener(ToggleMouse);

        EventSystem.SubscribeEvent(EventSystem.EventType.ON_DUAL_SOLAR_HIT, ShiftToEndDialogue);
    }

    public void Interact()
    {
        anim.SetBool("isTalking", true);
        Cursor.visible = true;
        dialogueRunner.SetProject(yarnProject);
        dialogueRunner.startNode = nodeToTrigger;
        dialogueRunner.StartDialogue(dialogueRunner.startNode);
    }

    private void ToggleMouse()
    {
        Cursor.visible = false;

    }

    private void ShiftToEndDialogue()
    {
        nodeToTrigger = "Ending";
        EventSystem.UnsubscribeEvent(EventSystem.EventType.ON_DUAL_SOLAR_HIT, ShiftToEndDialogue);
        Cursor.visible = false;

    }
}
