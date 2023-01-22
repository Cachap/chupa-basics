using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float runSpeed = 7f;
    [SerializeField] private float walkSpeed = 3f;
    [SerializeField] private float rotateSpeed = 5f;

    [SerializeField] private float stamina = 100f;
    [SerializeField] private float staminaConsumption = 40f;
    [SerializeField] private float staminaRecovery = 20f;

    [SerializeField] private float pickupDistance = 2f;

    private float currentlyMovementSpeed = 3f;
    private Inventory inventory;

    public float Stamina { get { return stamina; } }  
    public bool IsShowInfoItem { get; private set; }

	private void Start()
	{
		inventory = GetComponent<Inventory>();
	}

	private void Update()
    {
        if (Input.GetKey(KeyCode.W))
            Move(Vector3.forward);

        if (Input.GetKey(KeyCode.S))
            Move(Vector3.back);

        if (Input.GetKey(KeyCode.D))
            Move(Vector3.right);

        if (Input.GetKey(KeyCode.A))
            Move(Vector3.left);

        Rotate(Input.GetAxis("Mouse X"));

        DetectionItem();
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
    }

	private void Move(Vector3 direction)
	{
		transform.position += transform.TransformDirection(currentlyMovementSpeed * Time.deltaTime * direction);
	}

    private void Rotate(float direction)
    {
        transform.Rotate(Vector3.up, rotateSpeed * direction);
    }

    private void DetectionItem()
	{
        Ray ray = new Ray(transform.position, transform.TransformDirection(Vector3.forward) * pickupDistance);

        if (Physics.Raycast(ray, out RaycastHit obj, pickupDistance))
		{
            var itemObj = obj.transform.gameObject;
            if (itemObj.CompareTag(Item.ItemTag))
			{
                IsShowInfoItem = true;
                PickupItem(itemObj.GetComponent<Item>());
            }
        }
        else
            IsShowInfoItem = false;
    }

    private void PickupItem(Item item)
    {
       if (Input.GetKeyDown(KeyCode.E))
	   {
           item.gameObject.SetActive(false);
           inventory.AddItem(item);
       }
    }

    private void UseItem()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            inventory.UseItem((int)KeyCode.Alpha1 - 49);

        if (Input.GetKeyDown(KeyCode.Alpha2))
            inventory.UseItem((int)KeyCode.Alpha2 - 49);

        if (Input.GetKeyDown(KeyCode.Alpha3))
            inventory.UseItem((int)KeyCode.Alpha3 - 49);
    }

    private bool StaminaIsOver()
    {
        if (stamina > 0)
            return false;

        return true;
    }

    private bool StaminaIsFull()
    {
        if (stamina < 100)
            return false;

        return true;
    }

    private void Run()
    {
        currentlyMovementSpeed = runSpeed;
    }

    private void Walk()
	{
        currentlyMovementSpeed = walkSpeed;
    }

    private void DecreaseStamina()
    {
        stamina -= staminaConsumption * Time.deltaTime;
    }

    private void IncreaseStamina()
	{
        stamina += staminaRecovery * Time.deltaTime;
	}

    public void UpStamina(float value)
	{
        stamina += value;
	}
}
