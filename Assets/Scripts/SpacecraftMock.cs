using System.Collections;
using UnityEngine;

public class SpacecraftMock : MonoBehaviour
{
    private const int VelocityMetersPerSecond = 10;
    private const bool Forward = true;
    private const int UnitLengthInMeters = 1000;
    private const float TranslationRate = 0.01f;
    private int forwardInput;
    private bool fwdInput;
    private bool revInput;
    private bool rightInput;
    private bool leftInput;
    private bool upInput;
    private bool downInput;
    private int horizontalInput;
    private int verticalInput;

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
        fwdInput = Input.GetKey("e");
        revInput = Input.GetKey("q");

        rightInput = Input.GetKey("d");
        leftInput = Input.GetKey("a");

        upInput = Input.GetKey("w");
        downInput = Input.GetKey("s");

        if (fwdInput && revInput)
            forwardInput = 0;
        else if (fwdInput)
            forwardInput = 1;
        else if (revInput)
            forwardInput = -1;

        if (rightInput && leftInput)
            horizontalInput = 0;
        else if (rightInput)
            horizontalInput = 1;
        else if (leftInput)
            horizontalInput = -1;

        if (upInput && downInput)
            verticalInput = 0;
        else if (upInput)
            verticalInput = 1;
        else if (downInput)
            verticalInput = -1;
    }

    private void FixedUpdate()
    {
        if (horizontalInput != 0 || verticalInput != 0 || forwardInput != 0)
        {
            transform.position += 
                new Vector3(horizontalInput, verticalInput, forwardInput) * TranslationRate * VelocityMetersPerSecond / UnitLengthInMeters;

            ////forwardInput = 0;
            ////horizontalInput = 0;
            ////verticalInput = 0;
        }
    }
}
