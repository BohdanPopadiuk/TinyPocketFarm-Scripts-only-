using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/SkinData")]
public class SkinData : ScriptableObject
{
    public List<SkinPrint> skinPrints;
}

[Serializable]
public class SkinPrint
{
    public int price;
    public Material print;
    public Sprite printPreview;
    public bool skinBuyed => PlayerPrefs.GetInt("SkinPrint_" + print.name) == 1;

    public void BuySkin()
    {
        PlayerPrefs.SetInt("SkinPrint_" + print.name, 1);
        GAEvents.BuyPrint(print.name);
    }
}
