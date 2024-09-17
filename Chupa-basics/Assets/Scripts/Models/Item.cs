using UnityEngine;

public class Item : MonoBehaviour
{
	[SerializeField] private bool isYourself;
	private IEffect effect;

	public Sprite sprite;

	private void Start()
	{
		if(isYourself)
			effect = gameObject.AddComponent<EffectOnYourself>();
		else
			effect = gameObject.AddComponent<EffectOnEnemy>();
	}

	public void Use()
	{
		effect.MakeEffect();
	}
}
