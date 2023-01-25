using UnityEngine;

public class EffectOnYourself : MonoBehaviour, IEffect
{
	private Player player;

	private void Start()
	{
		player = GameObject.Find(nameof(Player)).GetComponent<Player>();
	}

	public void MakeEffect()
	{
		player.UpStamina(100f);
	}
}
