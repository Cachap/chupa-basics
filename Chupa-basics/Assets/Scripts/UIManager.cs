using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
	[SerializeField] Image fillStaminaBar;
	[SerializeField] Text infoItem;
	[SerializeField] Player player;

	private void Update()
	{
		ChangeStaminaBar();

		if(player.IsShowInfoItem)
			ShowInfoItem();
		else
			HideInfoItem();
	}

	private void ChangeStaminaBar()
	{
		fillStaminaBar.fillAmount = player.Stamina / 100;
	}

	private void ShowInfoItem()
	{
		infoItem.gameObject.SetActive(true);
	}

	private void HideInfoItem()
	{
		infoItem.gameObject.SetActive(false);
	}
}
