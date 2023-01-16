using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float runSpeed = 7f;
    [SerializeField] float walkSpeed = 3f;
    [SerializeField] float rotateSpeed = 5f;

    [SerializeField] float stamina = 100f;
    [SerializeField] float staminaConsumption = 40f;
    [SerializeField] float staminaRecovery = 20f;

    float currentlyMovementSpeed = 3f;

    public float Stamina { get { return stamina; } }  

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
}
