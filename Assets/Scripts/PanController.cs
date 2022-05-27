﻿using UnityEngine;

public class PanController : MonoBehaviour
{
	private float speed = 2.0f;
	void Start()
	{
		
	}
	void Update () 
    {
		if (Input.GetKey(KeyCode.W)) transform.position += Vector3.forward * speed * Time.deltaTime;
		if (Input.GetKey(KeyCode.S)) transform.position += Vector3.back* speed * Time.deltaTime;
		if (Input.GetKey(KeyCode.A)) transform.position += Vector3.left* speed * Time.deltaTime;
		if (Input.GetKey(KeyCode.D)) transform.position += Vector3.right * speed * Time.deltaTime;
	}
}

