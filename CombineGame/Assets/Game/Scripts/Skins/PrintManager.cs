using UnityEngine;

public class PrintManager : MonoBehaviour
{
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private int numberOfMaterial;
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        SetPrint(Globals.instance.skinData.skinPrints[PlayerPrefs.GetInt(Keys.currentPrint)].print);
        GameAction.sellectPrint += SetPrint;
    }

    private void OnDestroy()
    {
        GameAction.sellectPrint -= SetPrint;
    }

    void SetPrint(Material print)
    {
        Material[] materials = meshRenderer.materials;
        materials[numberOfMaterial] = print; 
        meshRenderer.materials = materials;
    }
}
