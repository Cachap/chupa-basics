using UnityEngine;
using UnityEngine.Events;

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

    private UnityEvent<float> OnStaminaChange = new UnityEvent<float>();
    private UnityEvent<Tags> OnInfoActionChange = new UnityEvent<Tags>();

	private void Start()
	{
		inventory = GetComponent<Inventory>();
        selfRigidbody = GetComponent<Rigidbody>();
        OnStaminaChange.AddListener(ChangeStaminaUI);
        OnInfoActionChange.AddListener(ChangInfoAction);
    }

	private void Update()
    {
        Move();
        Rotate();
        DetectInteractableObject();
        UseItem();

        if (Input.GetKey(KeyCode.LeftShift) 
            && !StaminaIsOver() 
            && !Input.GetKeyUp(KeyCode.LeftShift))
                Run();
		else if (!StaminaIsFull())
            Walk();
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

    private void Rotate()
    {
        Vector3 direction = Vector3.up * Input.GetAxis("Mouse X");
        Quaternion deltaRotation = Quaternion.Euler(rotateSpeed * direction);

        selfRigidbody.MoveRotation(selfRigidbody.rotation * deltaRotation);
    }

    private void DetectInteractableObject()
	{
        Ray ray = new(transform.position, transform.TransformDirection(Vector3.forward) * interactionDistance);

        if (Physics.Raycast(ray, out RaycastHit obj, interactionDistance))
		{
            var someObj = obj.transform.gameObject;

            if (someObj.CompareTag(nameof(Tags.Item)))
			{
                if (Input.GetKeyDown(KeyCode.E))
                    PickupItem(someObj.GetComponent<Item>());

                OnInfoActionChange.Invoke(Tags.Item);
                return;
            }

            if (someObj.CompareTag(nameof(Tags.Door)))
            {
                if (Input.GetKeyDown(KeyCode.E))
                    DoorAction(someObj.GetComponent<Door>());

                OnInfoActionChange.Invoke(Tags.Door);
                return;
            }
        }

        OnInfoActionChange.Invoke(Tags.None);
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

    private void Run()
	{
        currentlyMovementSpeed = runSpeed;
        DecreaseStamina();
    }

	private void DecreaseStamina()
	{
        stamina -= staminaConsumption * Time.deltaTime;
        OnStaminaChange.Invoke(stamina);
    }

    private void Walk()
    {
        currentlyMovementSpeed = walkSpeed;
        IncreaseStamina();
    }

    private void IncreaseStamina()
	{
        stamina += staminaRecovery * Time.deltaTime;
        OnStaminaChange.Invoke(stamina);
    }

    public void UpStamina(float value) => stamina += value;

    private void ChangInfoAction(Tags tag)
	{
        uiManager.ShowInfoObject(tag);
    }

    private void ChangeStaminaUI(float value)
	{
        uiManager.ChangeStaminaBar(stamina);
    }
}
