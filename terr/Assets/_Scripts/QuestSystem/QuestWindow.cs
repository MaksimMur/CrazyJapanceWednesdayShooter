using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class QuestWindow : MonoBehaviour
{
    [SerializeField] private GameObject questContainer;
    [SerializeField] private Text tTitle;
    [SerializeField] private Text tProgress;
    [SerializeField] private Text tDescription;
    [SerializeField] private Image icon;
    [SerializeField] private Sprite[] QuestCompleteCondition;

    public void SetQuestDescritption(string title, string desription)
    {
        tTitle.text = title;
        tDescription.text = desription;
    }
    public void OpenQuestWindow()
    {
        questContainer.SetActive(true);
    }

    public void CloseQuesWindow()
    {
        questContainer.SetActive(false);
    }

    public void QuestComplete()
    {
        icon.sprite = QuestCompleteCondition[1];
    }

    public void GetCompleteQuest()
    {
        icon.sprite = QuestCompleteCondition[0];
    }

    public void SetProgress(string progress)
    {
        tProgress.text = progress;
    }
}
