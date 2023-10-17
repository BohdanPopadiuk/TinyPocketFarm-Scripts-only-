using System;
using DG.Tweening;
using UnityEngine;

public class TutorialController : MonoBehaviour
{
    private Vector3 playerStartPos;
    private float firstTutorialDistance = .5f;
    [SerializeField] private Transform player;
    [SerializeField] private CanvasGroup firstTutorial;
    [SerializeField] private TriggerWithDelay storeTrigger;

    private bool firstTutorialShowed;
    void Start()
    {
        playerStartPos = player.position;
        GameAction.showCoinBooster += ShowStoreTrigger;//насправді треба було створити нову подію, але й ця виконувала потрібний функціонал
    }

    private void OnDestroy()
    {
        GameAction.showCoinBooster -= ShowStoreTrigger;
    }

    private void ShowStoreTrigger()
    {
        storeTrigger.TriggerActivator(true);
    }

    void Update()
    {
        if (!firstTutorialShowed)
        {
            if (Vector3.Distance(playerStartPos, player.position) > firstTutorialDistance)
            {
                firstTutorial.DOFade(0, .5f);
                firstTutorial.transform.DOScale(0, .5f).SetEase(Ease.InOutBack);
                firstTutorialShowed = true;
            }
        }
    }
}
