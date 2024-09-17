using UnityEngine;

public class Door : MonoBehaviour
{
    private HingeJoint joint;
    private bool isOpened = true;

    private void Start()
    {
        joint = GetComponent<HingeJoint>();
    }

	public void Action()
	{
        isOpened = !isOpened;

        if (isOpened)
		{
            Close();
            return;
		}

        Open();
	}

    private void Close()
    {
        JointSpring jointSpring = joint.spring;
        jointSpring.targetPosition = 0;
        joint.spring = jointSpring;
    }

    private void Open()
	{
        JointSpring jointSpring = joint.spring;
        jointSpring.targetPosition = 90;
        joint.spring = jointSpring;
    }
}
