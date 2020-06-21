using System.Collections;
using UnityEngine;

public class SpacecraftMock : MonoBehaviour
{
    private const int VelocityMetersPerSecond = 10;
    private const bool Forward = true;
    private const int UnitLengthInMeters = 1000;
    private const float TranslationRate = 0.1f;
    private float horizontalInput;
    private float verticalInput;

    private void Start()
    {
        //StartCoroutine(MoveForward());
    }

    private IEnumerator MoveForward()
    {
        var transform = gameObject.transform;

        while (true)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y,
                transform.position.z + (Forward ? 1 : -1) * TranslationRate * VelocityMetersPerSecond / UnitLengthInMeters);

            yield return new WaitForSeconds(TranslationRate);
        }
    }

    private void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        if (horizontalInput != 0 || verticalInput != 0)
        {
            transform.position += new Vector3(horizontalInput, 0, verticalInput) * TranslationRate * VelocityMetersPerSecond / UnitLengthInMeters;
        }
    }
}
