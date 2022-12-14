using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using Cinemachine;
public class DialogueTrigger : MonoBehaviour
{

    [SerializeField] protected GameObject dialogcamera;
    [SerializeField] protected float speedOfText=0.1f;
    [SerializeField] protected GameObject hint;
    [SerializeField] protected List<Dialogue> dialogue;
    [SerializeField] protected DialogueSytem system;
    protected PlayerDialogBehaviour playerDialogBehaviour;
    protected Animator anim;

    public GameObject Hint { get => hint; protected set => hint = value; }
    protected virtual void Awake()
    {
        anim=GetComponent<Animator>();
    }
    private void Start()
    {
        allDialogs = dialogue.Count;
    }

    public virtual void StartDialogue(PlayerDialogBehaviour behaviour)
    {
        if (allDialogs == 0 || currentDialog + 1  > allDialogs)
        {
            return;
        }
        playerDialogBehaviour = behaviour;
        Cursor.visible = true;
        behaviour.OffFuctionalityOfhero();
        dialogcamera.SetActive(true);
        system.OpenDialogUI();
        system.bContinue.onClick.RemoveAllListeners();
        system.bContinue.onClick.AddListener(NextNode);
        NextDialgoue(dialogue[currentDialog]);
    }
    protected int currentDialog = 0, allDialogs = 0, currentNode = 0, allNodes = 0;

    public bool HasAnyDialog => currentDialog+1 < allDialogs;

    public virtual void NextDialgoue(Dialogue dialogue)
    {
        if (allDialogs == 0 || currentDialog + 1 > allDialogs)
        {
            EndDialog();
        }
        else
        {

            currentNode = 0;
            allNodes = dialogue.nodes.Count;
            NextNode();
        }
    }
    public virtual void NextNode()
    {      
        if (allNodes == 0 || currentNode + 1 > allNodes)
        {
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
    public void EndDialog()
    {
        dialogcamera.SetActive(false);
        Cursor.visible = false;
        playerDialogBehaviour.OnFuctionalityOfhero();
        system.bContinue.onClick.RemoveAllListeners();
        system.CloseDialogUI();
    }
    public IEnumerator ReadingSentences(string sentences)
    {
        system.textComment.text = string.Empty;
        foreach (char c in sentences.ToCharArray())
        {
            system.textComment.text = new StringBuilder(system.textComment.text+c.ToString()).ToString();
            yield return new WaitForSeconds(speedOfText);
        }
    }
    public bool DialogeIsOver { get; set; }
}
