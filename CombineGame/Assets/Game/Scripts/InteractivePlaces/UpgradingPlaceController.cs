using UnityEngine;
public class UpgradingPlaceController : TriggerWithDelay
{
    [SerializeField] private UIPageController storePage;
    [SerializeField] private SkinManagerPanel skinManagerPanel;
    public override void SetTrigger()
    {
        skinManagerPanel.SetCurrentSkin();
        storePage.OpenPage();
    }
}
