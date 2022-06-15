using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class GhostDialogue : MonoBehaviour, IInteractable
{
    public string nodeToTrigger;

    [SerializeField] private YarnProject yarnProject;
    [SerializeField] private Sprite customDialogueBG;

    private DialogueRunner dialogueRunner;
    private bool firstConversation = true;

    void Start()
    {
        dialogueRunner = FindObjectOfType<DialogueRunner>();
        dialogueRunner.dialogueViews = FindObjectsOfType<DialogueViewBase>();

        dialogueRunner.onDialogueComplete.AddListener(ToggleMouse);

        EventSystem.SubscribeEvent(EventSystem.EventType.ON_ZERO_SOLAR_HITS, () => nodeToTrigger = "PuzzleExplanation");
        EventSystem.SubscribeEvent(EventSystem.EventType.ON_SINGLE_SOLAR_HIT,() => nodeToTrigger = "UpgradeExplanation");
        EventSystem.SubscribeEvent(EventSystem.EventType.ON_DUAL_SOLAR_HIT, () => nodeToTrigger = "PuzzleFinished");
    }

    private void Update()
    {

    }

    public void Interact()
    {
        UIManager.instance.SetDialogueBackground(customDialogueBG);
        dialogueRunner.SetProject(yarnProject);
        dialogueRunner.startNode = nodeToTrigger;
        dialogueRunner.StartDialogue(dialogueRunner.startNode);
        Cursor.visible = true;
        if (firstConversation)
        {
            nodeToTrigger = "PuzzleExplanation";
            firstConversation = false;
        }
    }

    private void ToggleMouse()
    {
        Cursor.visible = false;

    }
}
