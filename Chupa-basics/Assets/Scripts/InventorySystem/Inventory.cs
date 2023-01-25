using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
	private Item[] items;

	private readonly int limitItems = 3;
	private int countItems = 0;

	[SerializeField] private Sprite emptyCell;
	[SerializeField] private Image[] cells;

	Inventory()
	{
		items = new Item[limitItems];
	}

	public void AddItem(Item item)
	{
		if (InventoryIsFull())
			return;

		int i = 0;
		foreach (var cell in cells)
		{
			if (cell.sprite == emptyCell)
			{
				cell.sprite = item.sprite;
				items[i] = item;
				break;
			}
			i++;
		}
		countItems++;
	}

	private bool InventoryIsFull() => countItems >= limitItems;

	public void UseItem(int index)
	{
		if (items[index] == null)
			return;

		items[index].Use();
		ClearCell(index);
	}

	private void ClearCell(int index)
	{
		countItems--;
		cells[index].sprite = emptyCell;
		items[index] = null;
	}
}
