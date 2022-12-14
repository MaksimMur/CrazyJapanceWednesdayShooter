using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WomanDialogue : DialogueTrigger
{
    [SerializeField] private EnemySpawner enemySpawner;
    private QuestGiver giver;
   

    protected override void Awake()
    {
        base.Awake();
        giver = GetComponent<QuestGiver>();
    }
    public override void StartDialogue(PlayerDialogBehaviour behaviour)
    {
        if (allDialogs == 0 || currentDialog + 1 > allDialogs)
        {
            EndDialog();
            return;
        }
        if (!giver.IsGiveQuest)
        {
            currentDialog = 0;
        }
        else
        {
            if (giver.quest.goal.IsReached())
            {
                giver.AcceptQuest();
                currentDialog = 2;
                DialogeIsOver = true;
            }
            else
            {
                currentDialog = 1;
            }
        }
        playerDialogBehaviour = behaviour;
        Cursor.visible = true;
        behaviour.OffFuctionalityOfhero();
        dialogcamera.SetActive(true);
        system.OpenDialogUI();
        system.bContinue.onClick.RemoveAllListeners();
        system.bContinue.onClick.AddListener(NextNode);
        NextDialgoue(dialogue[currentDialog]);
        if (!giver.IsGiveQuest)
        {
            giver.GiveQuestForPlayer();
            enemySpawner.SpawnEnemyStart();
            
        }
    }

    public override void NextNode()
    {
        if (allNodes == 0 || currentNode + 1 > allNodes)
        {
            if (currentDialog == 2)
            {
                anim.SetBool("SuccesRequest", true);
            }
            EndDialog();
        }
        else
        {
       
            DialogueNode node = dialogue[currentDialog].nodes[currentNode];
            system.icon.sprite = node.icon;
            system.textName.text = node.name;
            currentNode++;
            StopAllCoroutines();
            StartCoroutine(ReadingSentences(node.sentences));
        }
    }

    public override void NextDialgoue(Dialogue dialogue)
    {
        if (allDialogs == 0 || currentDialog + 1 > allDialogs)
        {
            EndDialog();
        }
        else
        {
            if (currentDialog != 2)
            {
                anim.SetBool("StartDefaultDialog", true);
            }
            else anim.SetBool("SuccesRequest", true);

            currentNode = 0;
            allNodes = dialogue.nodes.Count;
            NextNode();
        }
    }

    public void AnimationIsOver()
    {
        anim.SetBool("StartDefaultDialog", false);
    }
}
