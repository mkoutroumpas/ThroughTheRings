using UnityEngine;

public class MouseLookAround : MonoBehaviour
{
    private float rotationX = 0f;
    private float rotationY = 0f;

    public float Sensitivity { get; set; } = 2f;

    void Update()
    {
        rotationY += Input.GetAxis("Mouse X") * Sensitivity;
        rotationX += Input.GetAxis("Mouse Y") * Sensitivity;

        transform.localEulerAngles = new Vector3(rotationX, rotationY, 0);
    }
}
