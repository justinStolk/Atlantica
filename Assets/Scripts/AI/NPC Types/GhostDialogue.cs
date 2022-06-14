using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class GhostDialogue : MonoBehaviour, IInteractable
{
    public string nodeToTrigger;

    [SerializeField] private YarnProject yarnProject;
    
    private DialogueRunner dialogueRunner;

    void Start()
    {
        dialogueRunner = FindObjectOfType<DialogueRunner>();
        dialogueRunner.dialogueViews = FindObjectsOfType<DialogueViewBase>();

        dialogueRunner.onDialogueComplete.AddListener(SetExplanationDialogue);

        EventSystem.SubscribeEvent(EventSystem.EventType.ON_ZERO_SOLAR_HITS, () => nodeToTrigger = "PuzzleExplanation");
        EventSystem.SubscribeEvent(EventSystem.EventType.ON_SINGLE_SOLAR_HIT,() => nodeToTrigger = "UpgradeExplanation");
        EventSystem.SubscribeEvent(EventSystem.EventType.ON_DUAL_SOLAR_HIT, () => nodeToTrigger = "PuzzleFinished");

    }

    public void Interact()
    {
        dialogueRunner.SetProject(yarnProject);
        dialogueRunner.startNode = nodeToTrigger;
        dialogueRunner.StartDialogue(dialogueRunner.startNode);
    }

    private void SetExplanationDialogue()
    {
        nodeToTrigger = "PuzzleExplanation";
        dialogueRunner.onDialogueComplete.RemoveListener(SetExplanationDialogue);
    }
}
