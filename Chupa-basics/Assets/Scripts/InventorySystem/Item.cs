using UnityEngine;

public class Item : MonoBehaviour
{
	[SerializeField] string effect;

	public string Effect { get { return effect; } }

	public readonly static string ItemTag = "Item";
}
