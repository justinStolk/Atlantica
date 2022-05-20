using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class TalkingState : BaseState
{
    [HideInInspector] public Transform Speaker;
    [HideInInspector] public bool IsTalking;

    [SerializeField] private YarnProject yarnProject;
    [SerializeField] private string nodeToTrigger;

    private DialogueRunner dialogueRunner;
    private BaseState previousState;

    private void Start()
    {
        dialogueRunner = FindObjectOfType<DialogueRunner>();
        Speaker = FindObjectOfType<PlayerManager>().transform;
        dialogueRunner.dialogueViews = FindObjectsOfType<DialogueViewBase>();
    }
    public override void OnStateEnter()
    {
        if(owner.PreviousState.GetType() != typeof(TalkingState))
        {
            dialogueRunner.onDialogueComplete.AddListener(OnDialogueConcluded);
            IsTalking = true;
            previousState = owner.PreviousState;
            Vector3 speakerPos = new Vector3(Speaker.position.x, 0, Speaker.position.z);
            Vector3 myPos = new Vector3(transform.position.x, 0, transform.position.z);
            Vector3.RotateTowards(myPos, speakerPos, Mathf.Deg2Rad * 360, 90);
            dialogueRunner.SetProject(yarnProject);
            dialogueRunner.startNode = nodeToTrigger;
            dialogueRunner.StartDialogue(dialogueRunner.startNode);
        }
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
            Debug.Log(previousState.GetType());
            owner.SwitchState(previousState.GetType());
        }
    }
    private void OnDialogueConcluded()
    {
        IsTalking = false;
    }
}
