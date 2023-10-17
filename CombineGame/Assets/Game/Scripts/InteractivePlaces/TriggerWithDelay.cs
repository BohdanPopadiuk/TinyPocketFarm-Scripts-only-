using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class TriggerWithDelay : MonoBehaviour
{
    [SerializeField] private Collider triggerCollider;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private float showTime;
    [SerializeField] private Image triggerDelayIndicator;
    [SerializeField] private float delay = 1f;
    private bool playerStayInTrigger;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            playerStayInTrigger = true;
            StartCoroutine(DelayTrigger());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            playerStayInTrigger = false;
            StartCoroutine(DelayTrigger());
        }
    }

    IEnumerator DelayTrigger()
    {
        float timer = triggerDelayIndicator.fillAmount;
        while (timer < delay && playerStayInTrigger || timer > 0 && !playerStayInTrigger)
        {
            timer += Time.deltaTime * (playerStayInTrigger ? 1 : -1);
            triggerDelayIndicator.fillAmount = timer / delay;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        if (playerStayInTrigger) SetTrigger();
    }

    public virtual void TriggerActivator(bool triggerEnabled)
    {
        triggerCollider.enabled = triggerEnabled;
        canvasGroup.DOFade(triggerEnabled ? 1 : 0, showTime);
        canvasGroup.transform.DOScale(triggerEnabled ? 1 : 0, showTime);
    }

    public virtual void SetTrigger() { }
}