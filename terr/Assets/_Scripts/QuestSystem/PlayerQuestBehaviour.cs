using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerQuestBehaviour : MonoBehaviour
{
    [SerializeField] private QuestWindow questWindow;
    public Quest QuestPlayer { get; set; }

    public void SetGoal(GoalType type)
    {
        switch (type)
        {
            case GoalType.Kill:
                Enemy.onEnemyDied += Battle;
                break;
        }
    }
    public void Battle()
    {
        QuestPlayer.goal.EnemyKilled();
        questWindow.SetProgress($"{QuestPlayer.goal.currentAmount}/{QuestPlayer.goal.reqiredAmount}");
        if (QuestPlayer.goal.IsReached())
        {
            questWindow.QuestComplete();
        }
    }
}
