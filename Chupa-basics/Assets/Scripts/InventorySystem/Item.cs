using UnityEngine;

public class Item : MonoBehaviour
{
	public readonly static string ItemTag = "Item";

	[SerializeField] private bool isYourSelf;
	private IEffect ability;

	public Sprite sprite;

	private void Start()
	{
		if(isYourSelf)
			ability = gameObject.AddComponent<EffectOnYourself>();
		else
			ability = gameObject.AddComponent<EffectOnEnemy>();
	}

	public void Use()
	{
		ability.MakeEffect();
	}
}
