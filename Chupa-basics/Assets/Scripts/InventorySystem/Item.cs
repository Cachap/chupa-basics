using UnityEngine;

public class Item : MonoBehaviour
{
	public readonly static string ItemTag = "Item";

	[SerializeField] private bool isYourSelf;
	private IEffect effect;

	public Sprite sprite;

	private void Start()
	{
		if(isYourSelf)
			effect = gameObject.AddComponent<EffectOnYourself>();
		else
			effect = gameObject.AddComponent<EffectOnEnemy>();
	}

	public void Use()
	{
		effect.MakeEffect();
	}
}
