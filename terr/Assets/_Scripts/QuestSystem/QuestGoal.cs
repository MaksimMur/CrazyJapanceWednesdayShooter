using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestGoal 
{
    public GoalType goalType;

    public int reqiredAmount;
    public int currentAmount;

    public bool IsReached()
    {
        return currentAmount >= reqiredAmount;
    }
    public void EnemyKilled()
    {
        if (goalType == GoalType.Kill)
            currentAmount++;
    }
}
public enum GoalType
{ 
    Kill
}
