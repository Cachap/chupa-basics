using UnityEngine;

public class Item : MonoBehaviour
{
	[SerializeField] string effect;
	public Sprite sprite;
	public readonly static string ItemTag = "Item";

	public string Effect { get { return effect; } }
}
