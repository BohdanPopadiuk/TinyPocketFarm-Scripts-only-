using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrintButton : MonoBehaviour
{
    [SerializeField] private Image printImage;
    [HideInInspector] public SkinPrint skinPrint;
    public GameObject lockImage;
    private void Start()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(() => OnClickSkin());
    }

    public void SetPrintButton(SkinPrint skinPrint)
    {
        this.skinPrint = skinPrint;
        printImage.sprite = skinPrint.printPreview;
        printImage.color = skinPrint.print.color;
        SetLockState();
    }

    void SetLockState()
    {
        if (transform.GetSiblingIndex() == 0) lockImage.SetActive(false);
        else lockImage.SetActive(!skinPrint.skinBuyed);
    }

    void OnClickSkin()
    {
        if(skinPrint.skinBuyed || transform.GetSiblingIndex() == 0) SelectSkin();
        GameAction.setPrintPrice?.Invoke(this);
    }

    public void SelectSkin()
    {
        Debug.Log("select skin nr: " + transform.GetSiblingIndex());
        SetLockState();
        PlayerPrefs.SetInt(Keys.currentPrint, transform.GetSiblingIndex());
        GameAction.sellectPrint?.Invoke(skinPrint.print);
    }
}
