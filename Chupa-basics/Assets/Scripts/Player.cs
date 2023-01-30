using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float runSpeed = 7f;
    [SerializeField] private float walkSpeed = 3f;
    [SerializeField] private float rotateSpeed = 15f;

    [SerializeField] private float stamina = 100f;
    [SerializeField] private float staminaConsumption = 20f;
    [SerializeField] private float staminaRecovery = 10f;

    [SerializeField] private float interactionDistance = 3f;
    [SerializeField] private UIManager uiManager;
    
    private float currentlyMovementSpeed = 3f;
    private Inventory inventory;
    private Rigidbody selfRigidbody;

	private void Start()
	{
		inventory = GetComponent<Inventory>();
        selfRigidbody = GetComponent<Rigidbody>();
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

        Vector3 movementX = moveVertical * transform.forward;
        Vector3 movementY = moveHorizontal * transform.right;
        Vector3 movement = movementX + movementY;

        selfRigidbody.velocity = currentlyMovementSpeed * movement;
	}

    private void Rotation()
    {
        Vector3 direction = Vector3.up * Input.GetAxis("Mouse X");
        Quaternion deltaRotation = Quaternion.Euler(rotateSpeed * direction);

        selfRigidbody.MoveRotation(selfRigidbody.rotation * deltaRotation);
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
