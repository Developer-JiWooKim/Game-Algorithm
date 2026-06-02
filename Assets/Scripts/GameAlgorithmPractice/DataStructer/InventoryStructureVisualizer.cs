using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryStructureVisualizer : MonoBehaviour
{
    private class InventoryItem
    {
        public int ItemId { get; }
        public string ItemName { get; }
        public Color SlotColor { get; }

        public InventoryItem(int itemId, string itemName, Color slotColor)
        {
            ItemId = itemId;
            ItemName = itemName;
            SlotColor = slotColor;
        }
    }

    [Header("Inventory")]
    [SerializeField] private int    maxSlotCount = 8;
    [SerializeField] private float  slotSize = 0.8f;
    [SerializeField] private float  slotGap = 0.15f;

    private readonly List<InventoryItem> inventory = new List<InventoryItem>();
    private readonly Dictionary<int, int> slotIndexByItemId = new Dictionary<int, int>();

    private int nextItemId = 1000;
    private int selectedSlotIndex;
    private int highlightedItemId = -1;

    private void Update()
    {
        // Keyboard.current는 현재 연결된 키보드 장치를 가져오는 Input System 프로퍼티입니다.
        if (Keyboard.current == null)
        {
            return;
        }

        // wasPressedThisFrame은 이번 프레임에 막 눌린 순간에만 true가 됩니다.
        if (Keyboard.current.aKey.wasPressedThisFrame)
        {
            AddItem();
        }

        if (Keyboard.current.backspaceKey.wasPressedThisFrame)
        {
            RemoveSelectedItem();
        }

        if (Keyboard.current.leftArrowKey.wasPressedThisFrame)
        {
            selectedSlotIndex--;
        }

        if (Keyboard.current.rightArrowKey.wasPressedThisFrame)
        {
            selectedSlotIndex++;
        }

        if (Keyboard.current.fKey.wasPressedThisFrame)
        {
            HighlightNewestItemByDictionary();
        }

        selectedSlotIndex = Mathf.Clamp(selectedSlotIndex, 0, Mathf.Max(0, inventory.Count - 1));
    }

    private void HighlightNewestItemByDictionary()
    {
        int newItemId = nextItemId - 1;

        if (slotIndexByItemId.TryGetValue(newItemId, out int slotIndex))
        {
            selectedSlotIndex = slotIndex;
            highlightedItemId = newItemId;
        }
    }

    private void RemoveSelectedItem()
    {
        if (inventory.Count == 0) return;

        InventoryItem removedItem = inventory[selectedSlotIndex];
        inventory.RemoveAt(selectedSlotIndex);
        slotIndexByItemId.Remove(removedItem.ItemId);

        RebuildDictionary();
        selectedSlotIndex = Mathf.Clamp(selectedSlotIndex, 0, Mathf.Max(0, inventory.Count - 1));
        highlightedItemId = -1;
    }

    private void RebuildDictionary()
    {
        slotIndexByItemId.Clear();

        for (int i = 0; i < inventory.Count; i++)
        {
            slotIndexByItemId[inventory[i].ItemId] = i;
        }
    }

    private void AddItem()
    {
        if (inventory.Count >= maxSlotCount) return;

        int itemId = nextItemId;
        nextItemId++;

        string itemName = "Item" + itemId;
        Color slotColor = GetColorBySlot(inventory.Count);
        InventoryItem item = new InventoryItem(itemId, itemName, slotColor);

        inventory.Add(item);
        slotIndexByItemId[item.ItemId] = inventory.Count - 1;

        selectedSlotIndex = inventory.Count - 1;
        highlightedItemId = item.ItemId;
    }

    private Color GetColorBySlot(int index)
    {
        // 같은 색만 반복되면 슬롯 구분이 어려우므로 몇 가지 색을 번갈아 사용합니다.
        Color[] colors =
        {
            new Color(0.2f, 0.7f, 1f),
            new Color(0.3f, 0.9f, 0.45f),
            new Color(1f, 0.75f, 0.25f),
            new Color(0.9f, 0.45f, 1f)
        };

        return colors[index % colors.Length];
    }

    private void OnDrawGizmos()
    {
        // OnDrawGizmos는 Scene 뷰에 개발용 시각 표시를 그릴 때 사용하는 Unity 메시지 메서드입니다.
        for (int i = 0; i < maxSlotCount; i++)
        {
            Vector3 slotPosition = transform.position + Vector3.right * i * (slotSize + slotGap);

            // Application.isPlaying은 현재 Play 모드인지 확인하는 프로퍼티입니다.
            bool hasItem = Application.isPlaying && i < inventory.Count;

            // Gizmos.DrawCube는 Scene 뷰에 색이 채워진 정육면체를 그립니다.
            Gizmos.color = hasItem ? inventory[i].SlotColor : Color.gray;
            Gizmos.DrawCube(slotPosition, Vector3.one * slotSize);

            // DrawWireCube는 채워지지 않은 테두리 상자를 그립니다.
            Gizmos.color = Color.black;
            Gizmos.DrawWireCube(slotPosition, Vector3.one * slotSize);

            if (!Application.isPlaying || !hasItem)
            {
                continue;
            }

            if (i == selectedSlotIndex)
            {
                Gizmos.color = Color.white;
                Gizmos.DrawWireCube(slotPosition, Vector3.one * (slotSize + 0.18f));
            }

            if (inventory[i].ItemId == highlightedItemId)
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawWireSphere(slotPosition + Vector3.up * 0.65f, 0.18f);
            }
        }
    }
}
