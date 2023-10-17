using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UIPageController : MonoBehaviour
{
    [SerializeField] private Button openButton;
    [SerializeField] private Button closeButton;
    
    private Vector3 closePanelPos;
    void Start()
    {
        closePanelPos = transform.localPosition;
        
        openButton.onClick.AddListener(()=> OpenPage());
        closeButton.onClick.AddListener(()=> ClosePage());
    }
    
    public void OpenPage()
    {
        transform.SetAsLastSibling();
        transform.DOLocalMove(Vector3.zero, Globals.instance.gameConfig.panelOpenSpeed);
    }
    
    public void ClosePage()
    {
        transform.DOLocalMove(closePanelPos, Globals.instance.gameConfig.panelOpenSpeed);
    }
}
