using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

[RequireComponent(typeof(DialogueRunner))]
public class TalkingState : BaseState
{
    [HideInInspector] public Transform Speaker;
    [HideInInspector] public bool IsTalking;

    private DialogueRunner dialogueRunner;
    private BaseState previousState;

    private void Start()
    {
        dialogueRunner = GetComponent<DialogueRunner>();
    }
    public override void OnStateEnter()
    {
        dialogueRunner.onDialogueComplete.AddListener(OnDialogueConcluded);
        IsTalking = true;
        previousState = owner.PreviousState;
        Vector3 speakerPos = new Vector3(Speaker.position.x, 0, Speaker.position.z);
        Vector3 myPos = new Vector3(transform.position.x, 0, transform.position.z);
        Vector3.RotateTowards(transform.position, Speaker.position, Mathf.Deg2Rad * 360, 90);
        dialogueRunner.StartDialogue(dialogueRunner.startNode);
    }

    public override void OnStateExit()
    {

    }

    public override void OnStateFixedUpdate()
    {

    }

    public override void OnStateUpdate()
    {
        if (!IsTalking)
        {
            dialogueRunner.onDialogueComplete.RemoveListener(OnDialogueConcluded);
            owner.SwitchState(previousState.GetType());
        }
    }
    private void OnDialogueConcluded()
    {
        IsTalking = false;
    }
}
