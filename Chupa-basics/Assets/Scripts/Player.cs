using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float defaultMovementSpeed = 3f;
    [SerializeField] float rotateSpeed = 5f;
    float currentlyMovementSpeed = 3f;

    [SerializeField] float stamina = 100f;
    [SerializeField] float boostMovementSpeed = 7f;
    [SerializeField] float staminaConsumption = 40f;
    [SerializeField] float staminaRecovery = 20f;

    private bool isRun;

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

        if (Input.GetKey(KeyCode.LeftShift))
            Run();

        if (Input.GetKeyUp(KeyCode.LeftShift))
            Walk();

        Rotate(Input.GetAxis("Mouse X"));
        RecoveryStamina();
    }

	private void Move(Vector3 direction)
	{
		transform.position += transform.TransformDirection(currentlyMovementSpeed * Time.deltaTime * direction);
	}

    private void Rotate(float direction)
    {
        transform.Rotate(Vector3.up, rotateSpeed * direction);
    }

    private void Run()
    {
        isRun = true;
        currentlyMovementSpeed = boostMovementSpeed;

        if (stamina > 0)
            stamina -= staminaConsumption * Time.deltaTime;
        else
            currentlyMovementSpeed = defaultMovementSpeed;
    }

    private void Walk()
	{
        isRun = false;
        currentlyMovementSpeed = defaultMovementSpeed;
    }

    private void RecoveryStamina()
	{
        if(stamina < 100 && !isRun)
            stamina += staminaRecovery * Time.deltaTime;
	}
}
