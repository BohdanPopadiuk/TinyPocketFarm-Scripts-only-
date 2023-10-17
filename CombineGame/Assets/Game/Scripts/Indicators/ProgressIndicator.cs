using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ProgressIndicator : MonoBehaviour
{
    [SerializeField] private Transform field;
    [SerializeField] private Transform stackedGrass;
    [SerializeField] private int maxGrassCount;
    [SerializeField] private float fullPosition;
    [SerializeField] private ParticleSystem grassFallParticles;
    [SerializeField] private TextMeshProUGUI progressText;
    private int stackedGrassCount;
    private float progressStep;
    private bool levelComplete;
    private bool coinBoosterShowed;
    void Start()
    {
        maxGrassCount = field.GetChild(0).childCount * 100;
        progressText.text = "0%";
        progressStep = (fullPosition - stackedGrass.transform.localPosition.y) / 
            maxGrassCount * Globals.instance.gameConfig.stackGrassCount;//TODO maxGrassCount
        GameAction.updateProgressIndicator += UpdateProgressIndicator;
        GameAction.grassFall += GrassFall;
    }

    private void OnDestroy()
    {
        GameAction.updateProgressIndicator -= UpdateProgressIndicator;
        GameAction.grassFall -= GrassFall;
    }

    void UpdateProgressIndicator()
    {
        if(stackedGrass.transform.localPosition.y >= fullPosition) return;
        float calculatedProgress = stackedGrass.transform.localPosition.y + progressStep;
        stackedGrass.transform.localPosition = new Vector3(stackedGrass.transform.localPosition.x, 
            calculatedProgress, stackedGrass.transform.localPosition.z);

        stackedGrassCount += Globals.instance.gameConfig.stackGrassCount;
        float progressPercent = (float)stackedGrassCount / maxGrassCount * 100;
        progressText.text = progressPercent.ToString("n1") + "%";

        //if (progressPercent >= 80) GameAction.showNextLevelButton?.Invoke();
        
        if (progressPercent > 50f && !coinBoosterShowed)//в самому бустері налаштована частота показу (залишу 1 раз на 3 рівні)
        {
            GameAction.showCoinBooster?.Invoke();
            coinBoosterShowed = true;
        }
        
        if (progressPercent >= 99.99f && !levelComplete)
        {
            GameAction.levelComplete?.Invoke();
            levelComplete = true;
        }
    }

    void GrassFall(bool play)
    {
        if(play) grassFallParticles.Play();
        else grassFallParticles.Stop();
    }
}
