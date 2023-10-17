using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PrintManagerPanel : MonoBehaviour
{
    private SkinData skinData;
    private PrintButton selectedPrintButton;

    [SerializeField] private GameObject coinIcon;
    [SerializeField] private Transform usedIcon;
    [SerializeField] private Transform selectedIcon;
    [SerializeField] private PrintButton printPrefabButton;
    [SerializeField] private Transform contentGroup;
    [SerializeField] private Button skinBuyButton;
    [SerializeField] private TextMeshProUGUI skinPriceText;
    void Start()
    {
        skinData = Globals.instance.skinData;
        CreatePrintButtons();
        skinBuyButton.onClick.AddListener(() => BuySkin());
        GameAction.setPrintPrice += SetBuyButton;
    }

    private void OnDestroy()
    {
        GameAction.setPrintPrice -= SetBuyButton;
    }

    void CreatePrintButtons()
    {
        for (int i = 0; i < skinData.skinPrints.Count; i++)
        {
            PrintButton printButton = Instantiate(printPrefabButton, contentGroup);
            printButton.SetPrintButton(skinData.skinPrints[i]);
        }
        
        SetBuyButton(contentGroup.GetChild(PlayerPrefs.GetInt(Keys.currentPrint)).GetComponent<PrintButton>());
    }

    void SetBuyButton(PrintButton printButton)
    {
        bool skinBuyed = printButton.skinPrint.skinBuyed || printButton.transform.GetSiblingIndex() == 0;
        selectedPrintButton = printButton;
        skinBuyButton.interactable = !skinBuyed;
        skinPriceText.text = skinBuyed ? "SELECTED" : printButton.skinPrint.price.ToString();
        coinIcon.SetActive(!skinBuyed);
        
        selectedIcon.parent = printButton.transform;
        selectedIcon.localPosition = Vector3.zero;
        if (skinBuyed)
        {
            usedIcon.parent = printButton.transform;
            usedIcon.localPosition = Vector3.zero;
        }
    }

    private void BuySkin()
    {
        if (Globals.instance.UseMoney(selectedPrintButton.skinPrint.price) && !selectedPrintButton.skinPrint.skinBuyed)
        {
            selectedPrintButton.skinPrint.BuySkin();
            selectedPrintButton.SelectSkin(); //todo add same VFX and SFX

            usedIcon.parent = selectedPrintButton.transform;
            usedIcon.localPosition = Vector3.zero;
            skinBuyButton.interactable = false;
            skinPriceText.text = "SELECTED";
            coinIcon.SetActive(false);
        }
    }
}
