using UnityEngine;
using UnityEngine.UI;

public static class Inventory
{
	public static Item[] items;

	private static readonly int limitItems = 3;
	private static int currentItems = 0;
	private static readonly GameObject inventory;
	private static readonly Image[] uiItems;

	static Inventory()
	{
		items = new Item[3];
		inventory = GameObject.Find(nameof(Inventory));
		uiItems = inventory.transform.GetComponentsInChildren<Image>()[1..];
	}

	public static void AddItem(Item item)
	{
		if (InventoryIsFull())
			return;

		item.gameObject.SetActive(false);

		int i = 0;
		foreach (var ui in uiItems)
		{
			if (ui.name == "Inventory")
				continue;

			if (ui.sprite.name == "EmptyItem")
			{
				ui.sprite = item.gameObject.GetComponent<Image>().sprite;
				items[i] = item;
				break;
			}

			i++;
		}
		currentItems++;
	}

	private static bool InventoryIsFull()
	{
		if (currentItems >= limitItems)
			return true;

		return false;
	}

	public static void UseItem(int index)
	{
		if (items[index].gameObject.GetComponent<Image>().sprite.name == "EmptyItem")
			return;

		Debug.Log(items[index].Effect);

		currentItems--;
		inventory.transform.GetChild(index).GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/EmptyItem");
		items[index] = null;
	}
}
