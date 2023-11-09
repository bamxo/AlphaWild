using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    new public string name = "New Item";    // name
    public Sprite icon = null;              // icon
    public bool isDefaultItem = false;      // default?

    public virtual void Use()
    {
        // use item
        // something might happen

        Debug.Log("Using" + name);
    }

    public void RemoveFromInventory()
    {
        Inventory.instance.Remove(this);
    }
}
