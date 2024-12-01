using UnityEngine;

public class WheelSpinMiniGameService : MiniGame
{
    public Transform wheel;
    public float rotationSpeed = 100f;

    protected override void Initialize()
    {
        Debug.Log("Wheel Spin Mini-Game Started!");
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            wheel.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
        }

        if (Mathf.Abs(wheel.rotation.eulerAngles.z - 90) < 5f)
        {
            Debug.Log("Wheel Spin Mini-Game Completed!");
            EndMiniGame(true);
        }
    }

    protected override void Cleanup()
    {
        Debug.Log("Wheel Spin Mini-Game Ended!");
    }
}
