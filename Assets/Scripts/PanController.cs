using UnityEngine;

public class PanController : MonoBehaviour
{
	private Rigidbody rb;
	void Start()
	{
		rb = GetComponent<Rigidbody>();
	}
	void Update () 
    {
		if (Input.GetKey(KeyCode.W))
		{
			rb.AddForce(Vector3.forward);
			return;
		}

		if (Input.GetKey(KeyCode.S))
		{
			rb.AddForce(Vector3.back);
			return;
		}

		if (Input.GetKey(KeyCode.A))
		{
			rb.AddForce(Vector3.left);
			return;
		}

		if (Input.GetKey(KeyCode.D))
		{
			rb.AddForce(Vector3.right);
			return;
		}	
	}
}

