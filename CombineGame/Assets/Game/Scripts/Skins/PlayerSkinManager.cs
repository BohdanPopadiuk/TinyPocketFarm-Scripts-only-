using UnityEngine;

public class PlayerSkinManager : MonoBehaviour
{
    [SerializeField] private PlayerSkin[] playerSkins;

    void Start()
    {
        SelectSkin();
        GameAction.selectSkin += SelectSkin;
    }

    private void OnDestroy()
    {
        GameAction.selectSkin -= SelectSkin;
    }
    
    void SelectSkin()
    {
        foreach (var playerSkin in playerSkins)
            if (!playerSkin.SkinSelected()) playerSkin.gameObject.SetActive(false);
            else playerSkin.gameObject.SetActive(true);
    }
}
