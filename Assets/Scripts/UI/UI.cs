using UnityEngine;

public class UI : MonoBehaviour
{
    public UI_ItemToolTip itemToolTip { get; private set; }
    public UI_StatToolTip statToolTip { get; private set; }

    public UI_Inventory inventoryUI { get; private set; }

    private bool skillTreeEnabled;
    private bool inventoryEnabled;

    private void Awake()
    {
        itemToolTip = GetComponentInChildren<UI_ItemToolTip>();
        statToolTip = GetComponentInChildren<UI_StatToolTip>();

        inventoryUI = GetComponentInChildren<UI_Inventory>(true);

        inventoryEnabled = inventoryUI.gameObject.activeSelf;
    }

    public void ToggleInventoryUI()
    {
        inventoryEnabled = !inventoryEnabled;
        inventoryUI.gameObject.SetActive(inventoryEnabled);
        statToolTip.ShowToolTip(false, null);
        itemToolTip.ShowToolTip(false, null);
    }
}
