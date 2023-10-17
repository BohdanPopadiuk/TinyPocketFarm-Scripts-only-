using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/GameConfig")]
public class GameConfig : ScriptableObject
{
    [Header("Camera")]
    public float cameraSmooth = 5;
    public Vector3 cameraOffset;
    public Vector3 cameraAngle;

    [Header("Player")]
    public int playerCapacity = 200;
    public float playerSpeed = 5f;
    public float playerTurnSpeed = 700f;
    public int stackGrassCount = 2;

    [Header("Animations")]
    public float bladeSpeedRotation = 50f;

    [Header("UI")]
    public float panelOpenSpeed = .5f;
    public float scrollSkinSpeed = 1f;
    public float winPanelShowSpeed = 2f;
    public float winButtonShowDuration = 3.5f;

    [Header("Upgrades")]
    public int maxSpeedUpgrade;
    public int maxCapacityUpgrade;

    
    [Header("Money")]
    
    public int rewardForWheat = 10;
    public int rewardForLevelComplete = 200;
    public int rewardForAD = 200;
    public int rewardFor_1stIAP = 1000;
    public int rewardFor_2ndIAP = 10000;
    public int rewardFor_3rdIAP = 20000;

    public List<int> speedUpgradePrice;
    public List<int> capacityUpgradePrice;
    
    
    /*[Header("Parabolic")] public float parabolicHeight = 4f;
    public float parabolicSpeed = 1f;

    [Header("Level")] public float levelCompletePercent = .8f;

    [Header("Upgrade")] public float speedLevelMultiplier = 0.1f;
    public List<int> playerSpeedUpgradePrice;
    public List<int> stackSizeUpgradePrice;*/
}
