using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float runSpeed = 7f;
    [SerializeField] private float walkSpeed = 3f;
    [SerializeField] private float rotateSpeed = 5f;

    [SerializeField] private float stamina = 100f;
    [SerializeField] private float staminaConsumption = 40f;
    [SerializeField] private float staminaRecovery = 20f;

    [SerializeField] private float interactionDistance = 3f;
    [SerializeField] private UIManager uiManager;
    
    private float currentlyMovementSpeed = 3f;
    private Inventory inventory;

	private void Start()
	{
		inventory = GetComponent<Inventory>();
    }

	private void Update()
    {
        Move();
        Rotation();

        UseItem();

        if (Input.GetKey(KeyCode.LeftShift) 
            && !StaminaIsOver() 
            && !Input.GetKeyUp(KeyCode.LeftShift))
		{
            Run();
            DecreaseStamina();
		}

		else if (!StaminaIsFull())
		{
            Walk();
            IncreaseStamina();
		}

        UIManage();
    }

	private void Move()
	{
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical);
        transform.Translate(currentlyMovementSpeed * Time.deltaTime * movement);
	}

    private void Rotation()
    {
        float direction = Input.GetAxis("Mouse X");
        transform.Rotate(Vector3.up, direction * rotateSpeed);
    }

    private Tags DetectionInteractableObject()
	{
        Ray ray = new(transform.position, transform.TransformDirection(Vector3.forward) * interactionDistance);

        if (Physics.Raycast(ray, out RaycastHit obj, interactionDistance))
		{
            var someObj = obj.transform.gameObject;

            if (someObj.CompareTag(nameof(Tags.Item)))
			{
                if (Input.GetKeyDown(KeyCode.E))
                    PickupItem(someObj.GetComponent<Item>());

                return Tags.Item;
            }

            if (someObj.CompareTag(nameof(Tags.Door)))
            {
                if (Input.GetKeyDown(KeyCode.E))
                    DoorAction(someObj.GetComponent<Door>());

                return Tags.Door;
            }
        }
        return Tags.None;
    }

    private void PickupItem(Item item) => inventory.AddItem(item);

    private void DoorAction(Door door) => door.Action();

    private void UseItem()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
		{
            var indexItem = (int)KeyUseInInventory.Alpha1;
            inventory.UseItem(indexItem);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
		{
            var indexItem = (int)KeyUseInInventory.Alpha2;
            inventory.UseItem(indexItem);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
		{
            var indexItem = (int)KeyUseInInventory.Alpha3;
            inventory.UseItem(indexItem);
        }
    }

    private bool StaminaIsOver() => stamina < 0;

    private bool StaminaIsFull() => stamina > 100;

    private void Run() => currentlyMovementSpeed = runSpeed;

    private void Walk() => currentlyMovementSpeed = walkSpeed;

    private void DecreaseStamina() => stamina -= staminaConsumption * Time.deltaTime;

    private void IncreaseStamina() => stamina += staminaRecovery * Time.deltaTime;

    public void UpStamina(float value) => stamina += value;

    private void UIManage()
	{
        uiManager.ShowInfoObject(DetectionInteractableObject());
        uiManager.ChangeStaminaBar(stamina);
    }
}
