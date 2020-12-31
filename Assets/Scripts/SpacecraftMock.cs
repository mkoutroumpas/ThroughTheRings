using UnityEngine;

public class SpacecraftMock : MonoBehaviour
{
    const int UnitLengthInMeters = 1000;
    const float TranslationRate = 0.005f;
    const float ZeroSpeedTolerance = 0.00001f;
    const float SlowDownRate = 0.001f;

    float _velocityMetersPerSecondX = 0;
    float _velocityMetersPerSecondY = 0;
    float _velocityMetersPerSecondZ = 0;

    bool _fwdInput;
    bool _revInput;
    bool _rightInput;
    bool _leftInput;
    bool _upInput;
    bool _downInput;
    bool _allStopInput;
    bool _stopping;

    int _mainThrusterInput;
    int _horizontalInput;
    int _verticalInput;

    void Start()
    {

    }

    void Update()
    {
        _fwdInput = Input.GetKey(KeyCode.Keypad9);
        _revInput = Input.GetKey(KeyCode.Keypad7);

        _rightInput = Input.GetKey(KeyCode.Keypad6);
        _leftInput = Input.GetKey(KeyCode.Keypad4);

        _upInput = Input.GetKey(KeyCode.Keypad8);
        _downInput = Input.GetKey(KeyCode.Keypad2);

        _allStopInput = Input.GetKey(KeyCode.Keypad0);

        if (_allStopInput && !_stopping)
        {
            _stopping = true;
        }

        if (_stopping) 
        {
            SlowDown();

            if (!IsStopped())
            {
                return;
            }
            else
            {
                SlowDown(true);

                _stopping = false;
            }
        }

        if (_fwdInput && _revInput) _mainThrusterInput = 0;
        else if (_fwdInput)
        {
            _mainThrusterInput = 1;

            UpdateVelocity();
        }
        else if (_revInput)
        {
            _mainThrusterInput = -1;

            UpdateVelocity();
        }

        if (_rightInput && _leftInput) _horizontalInput = 0;
        else if (_rightInput)
        {
            _horizontalInput = 1;

            UpdateVelocity();
        }
        else if (_leftInput)
        {
            _horizontalInput = -1;

            UpdateVelocity();
        }

        if (_upInput && _downInput) _verticalInput = 0;
        else if (_upInput)
        {
            _verticalInput = 1;

            UpdateVelocity();
        }
        else if (_downInput)
        {
            _verticalInput = -1;

            UpdateVelocity();
        }
    }

    void FixedUpdate()
    {
        transform.position += new Vector3(_velocityMetersPerSecondX, _velocityMetersPerSecondY, _velocityMetersPerSecondZ) / UnitLengthInMeters;

        _horizontalInput = 0;
        _verticalInput = 0;
        _mainThrusterInput = 0;
    }

    void UpdateVelocity()
    {
        _velocityMetersPerSecondX += _horizontalInput * TranslationRate;
        _velocityMetersPerSecondY += _verticalInput * TranslationRate;
        _velocityMetersPerSecondZ += _mainThrusterInput * TranslationRate;
    }

    bool IsStopped() 
    {
        return _velocityMetersPerSecondX <= ZeroSpeedTolerance
            && _velocityMetersPerSecondY <= ZeroSpeedTolerance
            && _velocityMetersPerSecondZ <= ZeroSpeedTolerance;
    }

    void SlowDown(bool allStop = false) 
    {
        if (allStop) 
        {
            _velocityMetersPerSecondX = 0f;
            _velocityMetersPerSecondY = 0f;
            _velocityMetersPerSecondZ = 0f;

            return;
        }

        _velocityMetersPerSecondX -= SlowDownRate;
        _velocityMetersPerSecondY -= SlowDownRate;
        _velocityMetersPerSecondZ -= SlowDownRate;
    }
}
