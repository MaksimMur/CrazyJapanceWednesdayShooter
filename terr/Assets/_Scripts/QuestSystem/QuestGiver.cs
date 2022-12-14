using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGiver : MonoBehaviour
{
    public Quest quest;
    public PlayerQuestBehaviour player;
    public QuestWindow questWindow;
    public bool IsGiveQuest{get;set;}
    public void GiveQuestForPlayer()
    {
        if (player.QuestPlayer == null)
        {
            IsGiveQuest = true;
            player.QuestPlayer = quest;
            questWindow.SetProgress($"{quest.goal.currentAmount}/{quest.goal.reqiredAmount}");
            player.SetGoal(quest.goal.goalType);
            questWindow.SetQuestDescritption(quest.title, quest.description);
            questWindow.OpenQuestWindow();
        }
    }
    public void AcceptQuest()
    {
        if(player.QuestPlayer == quest && quest.goal.IsReached()) questWindow.CloseQuesWindow();
    }
}
