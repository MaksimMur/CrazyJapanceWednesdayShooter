using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueSytem : MonoBehaviour
{
    public GameObject dialogUI;
    public Button bContinue;
    public Image icon;
    public Text textName;
    public Text textComment;

    public void OpenDialogUI()
    {
        dialogUI.SetActive(true);
    }

    public void CloseDialogUI()
    {
        dialogUI.SetActive(false);
    }
}
