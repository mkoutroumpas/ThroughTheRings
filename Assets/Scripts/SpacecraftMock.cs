using UnityEngine;

public class SpacecraftMock : MonoBehaviour
{
    private float VelocityMetersPerSecondX = 0;
    private float VelocityMetersPerSecondY = 0;
    private float VelocityMetersPerSecondZ = 0;
    private const bool Forward = true;
    private const int UnitLengthInMeters = 1000;
    private const float TranslationRate = 1f;
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
        {
            forwardInput = 1;

            UpdateVelocity();
        }
        else if (revInput)
        {
            forwardInput = -1;

            UpdateVelocity();
        }

        if (rightInput && leftInput)
            horizontalInput = 0;
        else if (rightInput)
        {
            horizontalInput = 1;

            UpdateVelocity();
        }
        else if (leftInput)
        {
            horizontalInput = -1;

            UpdateVelocity();
        }

        if (upInput && downInput)
            verticalInput = 0;
        else if (upInput)
        {
            verticalInput = 1;

            UpdateVelocity();
        }
        else if (downInput)
        {
            verticalInput = -1;

            UpdateVelocity();
        }
    }

    private void FixedUpdate()
    {
        transform.position += new Vector3(VelocityMetersPerSecondX, VelocityMetersPerSecondY, VelocityMetersPerSecondZ) / UnitLengthInMeters;

        horizontalInput = 0;
        verticalInput = 0;
        forwardInput = 0;
    }

    private void UpdateVelocity()
    {
        VelocityMetersPerSecondX += horizontalInput * TranslationRate;
        VelocityMetersPerSecondY += verticalInput * TranslationRate;
        VelocityMetersPerSecondZ += forwardInput * TranslationRate;
    }
}
