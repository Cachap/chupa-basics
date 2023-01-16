using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
	[SerializeField] Image fillStaminaBar;
	[SerializeField] Player player;

	private void Update()
	{
		ChangeStaminaBar();
	}

	public void ChangeStaminaBar()
	{
		fillStaminaBar.fillAmount = player.Stamina / 100;
	}
}
