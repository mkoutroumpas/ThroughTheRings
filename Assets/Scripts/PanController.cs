using UnityEngine;

public class PanController : MonoBehaviour
{
	private float speed = 2.0f;
	void Start()
	{
		
	}
	void Update () 
    {
		if (Input.GetKeyDown(KeyCode.W)) transform.position += Vector3.forward * speed * Time.deltaTime;
		if (Input.GetKeyDown(KeyCode.S)) transform.position += Vector3.back* speed * Time.deltaTime;
		if (Input.GetKeyDown(KeyCode.A)) transform.position += Vector3.left* speed * Time.deltaTime;
		if (Input.GetKeyDown(KeyCode.D)) transform.position += Vector3.right * speed * Time.deltaTime;
	}
}

