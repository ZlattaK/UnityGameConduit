using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [Header("Àגעמסבמנ סכמעמג")]
    public InventorySlot[] slots;

    private void Awake()
    {
        slots = GetComponentsInChildren<InventorySlot>();
    }

    public int GetOccupiedCount()
    {
        int count = 0;

        foreach (var slot in slots)
        {
            if (slot != null && slot.isOccupied)
                count++;
        }

        return count;
    }

    public bool HasAtLeastItems(int needed)
    {
        return GetOccupiedCount() >= needed;
    }
}