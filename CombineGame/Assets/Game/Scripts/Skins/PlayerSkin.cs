using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkin : MonoBehaviour
{
    public CombineSkin combineSkin;
    private string skinKey => "PlayerSkin_" + combineSkin.name;
    private string selectedStringKey = "SelectedSkin";

    public string GetName => combineSkin.name;
    public int GetPrice => combineSkin.price;
    public bool skinBuyed => PlayerPrefs.GetInt(skinKey) == 1 || combineSkin.isBasicSkin;
    
    public bool SkinSelected()
    {
        if (PlayerPrefs.GetString(selectedStringKey) == skinKey) return true;
        if (PlayerPrefs.GetString(selectedStringKey) == "" && combineSkin.isBasicSkin) return true;
        return false;
    }
    public bool BuySkin()
    {
        if(!Globals.instance.UseMoney(combineSkin.price)) return false;
        PlayerPrefs.SetInt(skinKey, 1);
        GameAction.newSkinBuyed?.Invoke();
        GAEvents.BuySkin(combineSkin.name);
        SelectSkin();
        return true;
    }

    public void SelectSkin()
    {
        if(!skinBuyed) return;
        PlayerPrefs.SetString(selectedStringKey, skinKey);
        GameAction.selectSkin?.Invoke();
    }
}
