using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDialogBehaviour : MonoBehaviour
{
    
    private DialogueTrigger trigger;
    private MovementStateManager movement;
    private AimStateManager aim;
    private PlayerCharacteristics characteristics;
    private WeaponManager weaponManager;
    private WeaponAmmo weaponAmmo;
    private ActionStateManager actionStateManager;
    private void Awake()
    {
        movement = GetComponent<MovementStateManager>();
        aim = GetComponent<AimStateManager>();
        characteristics = GetComponent<PlayerCharacteristics>();
        weaponManager = GetComponentInChildren<WeaponManager>();
        weaponAmmo = GetComponentInChildren<WeaponAmmo>();
        actionStateManager = GetComponent<ActionStateManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<DialogueTrigger>())
        {
            trigger = other.GetComponent<DialogueTrigger>();
            if(trigger.HasAnyDialog) trigger.Hint.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
       if(trigger!=null) trigger.Hint.SetActive(false);
       trigger = null;
    }
    public void OffFuctionalityOfhero()
    {
        Debug.Log("Off");
        movement.enabled = false;
        aim.enabled = false;
        aim.VCam.gameObject.SetActive(false);
        characteristics.enabled = false;
        weaponManager.enabled = false;
        weaponAmmo.enabled = false;
        actionStateManager.enabled = false;
    }
    public void OnFuctionalityOfhero()
    {
        Debug.Log("On");
        movement.enabled = true;
        aim.VCam.gameObject.SetActive(true);
        aim.enabled = true;
        characteristics.enabled = true;
        weaponManager.enabled = true;
        weaponAmmo.enabled = true;
        actionStateManager.enabled = true;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && trigger!=null && !trigger.DialogeIsOver)
        {
            trigger.StartDialogue(this);
            trigger.Hint.SetActive(false);
        }
    }
}
