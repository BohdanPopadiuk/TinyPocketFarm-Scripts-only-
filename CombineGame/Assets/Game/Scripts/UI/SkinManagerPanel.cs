using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;

public class SkinManagerPanel : MonoBehaviour
{
    [SerializeField] private Button buySkinButton;
    [SerializeField] private Button nextSkinButton;
    [SerializeField] private Button previousSkinButton;
    [SerializeField] private TextMeshProUGUI skinPriceText;
    [SerializeField] private TextMeshProUGUI skinNameText;
    [SerializeField] private GameObject coinIcon;

    [SerializeField] private Transform skinRenderCamera;
    [SerializeField] private PlayerSkin[] playerSkins;

    private int currentSkin;

    private void Start()
    {
        nextSkinButton.onClick.AddListener(() => ScrollSkin(true));
        previousSkinButton.onClick.AddListener(() => ScrollSkin(false));
        buySkinButton.onClick.AddListener(() => BuySkin());
    }

    void BuySkin()
    {
        if (playerSkins[currentSkin].BuySkin()) UpdateSkinInfo();
    }

    void ScrollSkin(bool scrollRight)
    {
        currentSkin += scrollRight ? 1 : -1;
        currentSkin = Mathf.Clamp(currentSkin, 0, playerSkins.Length - 1);
        ShowCurrentSkin();
    }

    void ShowCurrentSkin(bool smooth = true)
    {
        previousSkinButton.interactable = currentSkin != 0;
        nextSkinButton.interactable = currentSkin != playerSkins.Length - 1;
        
        Vector3 newCameraPos = new Vector3(playerSkins[currentSkin].transform.position.x, skinRenderCamera.position.y,
            skinRenderCamera.position.z);
        
        skinRenderCamera.DOMove(newCameraPos, smooth ? Globals.instance.gameConfig.scrollSkinSpeed : 0);
        
        playerSkins[currentSkin].SelectSkin();
        UpdateSkinInfo();
    }
    
    void UpdateSkinInfo()
    {
        skinNameText.text = playerSkins[currentSkin].combineSkin.name;
        bool skinBuyed = playerSkins[currentSkin].skinBuyed;
        coinIcon.SetActive(!skinBuyed);
        buySkinButton.interactable = !skinBuyed;
        skinPriceText.text = skinBuyed ? "SELECTED" : playerSkins[currentSkin].GetPrice.ToString();
    }

    public void SetCurrentSkin()//при старті а також для ресетування після переходу в інше меню або повернені до гри
    {
        foreach (var playerSkin in playerSkins)
        {
            if (playerSkin.SkinSelected())
            {
                currentSkin = playerSkin.transform.GetSiblingIndex();
                ShowCurrentSkin(false);
                return;
            }
        }
    }
}
