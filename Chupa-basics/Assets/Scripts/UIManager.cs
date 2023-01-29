using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
	[SerializeField] Image fillStaminaBar;
	[SerializeField] Text infoObject;

	public void ShowInfoObject(Tags tagObject)
	{
		if (tagObject == Tags.Item)
			infoObject.text = "����� (�)";
		if (tagObject == Tags.Door)
			infoObject.text = "����������������� (�)";
		if (tagObject == Tags.None)
			infoObject.text = "";
	}

	public void ChangeStaminaBar(float stamina) => fillStaminaBar.fillAmount = stamina / 100;
}
