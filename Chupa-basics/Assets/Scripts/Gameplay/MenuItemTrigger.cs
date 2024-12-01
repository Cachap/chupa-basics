using UnityEngine;

public class MenuItemTrigger : MonoBehaviour
{
    private MiniGameUIController miniGameUIController;

    private void Start()
    {
        miniGameUIController = FindObjectOfType<MiniGameUIController>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            miniGameUIController.ShowUI(true);
        }
    }
}
