using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    private bool isCursorLocked = false;

    public Camera playerCamera;
    
    public Transform itemsParent;
    public GameObject inventoryUI;

    Inventory inventory;

    InventorySlot[] slots;
    
    void Start()
    {
        inventory = Inventory.instance;
        inventory.onItemChangedCallback += UpdateUi;

        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
    }
    
    void Update()
    {
        if (Input.GetButtonDown("Inventory"))
        {
            ToggleInventory();
        }
    }

    void ToggleInventory()
    {
        // Toggle the inventory UI
        inventoryUI.SetActive(!inventoryUI.activeSelf);

        // Toggle cursor lock and visibility
        if (inventoryUI.activeSelf)
        {
            DisableCameraControl();
            UnlockCursor();
        }
        else
        {
            EnableCameraControl();
            LockCursor();
        }
    }

    void LockCursor()
    {
        // Lock the cursor to the center of the screen and hide it
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        isCursorLocked = true;
    }

    void UnlockCursor()
    {
        // Unlock the cursor and make it visible
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        isCursorLocked = false;
    }

    void DisableCameraControl()
    {
        // Disable camera control here (e.g., by disabling the script)
        playerCamera.GetComponent<PlayerLook>().enabled = false;
    }

    void EnableCameraControl()
    {
        // Enable camera control here (e.g., by enabling the script)
        playerCamera.GetComponent<PlayerLook>().enabled = true;
    }

    void UpdateUi()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < inventory.items.Count)
            {
                slots[i].AddItem(inventory.items[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }
}
