using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image imgHP;
    [SerializeField] private float updateSpeedSeconds = 0.5f;

    private void Awake()
    {

        GetComponent<IHealthSystem>().onHpChanged += GetDamage;
    }
    private void GetDamage()
    {
        StartCoroutine("CangeHealthBar");
    }

    private IEnumerator CangeHealthBar()
    {
        float prechange = imgHP.fillAmount;
        float nextCond= GetComponent<IHealthSystem>().CurrentHealth / GetComponent<IHealthSystem>().MaxHeatlh;

        float elapsed = 0;
        while (elapsed < updateSpeedSeconds)
        {
            elapsed += Time.deltaTime;
            float v= Mathf.Lerp(prechange, nextCond, elapsed / updateSpeedSeconds);

            imgHP.fillAmount = v;
            yield return null;
        }

        imgHP.fillAmount = nextCond;

    }
}
