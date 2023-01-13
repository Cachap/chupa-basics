using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float movementSpeed = 3f;

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
    }

	private void Move(Vector3 direction)
	{
		transform.position += movementSpeed * Time.deltaTime * direction;
	}
}
