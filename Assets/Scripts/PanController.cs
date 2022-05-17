using UnityEngine;

public class PanController : MonoBehaviour
{
	void Update () 
    {
		Rigidbody rigidbody = GetComponent<Rigidbody>();

		if (Input.GetKey(KeyCode.W))
		{
			rigidbody.AddForce(Vector3.forward);
			return;
		}

		if (Input.GetKey(KeyCode.S))
		{
			rigidbody.AddForce(Vector3.back);
			return;
		}

		if (Input.GetKey(KeyCode.A))
		{
			rigidbody.AddForce(Vector3.left);
			return;
		}

		if (Input.GetKey(KeyCode.D))
		{
			rigidbody.AddForce(Vector3.right);
			return;
		}	
	}
}

