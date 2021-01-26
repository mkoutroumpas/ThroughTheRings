using UnityEngine;
public class RingObject : IRingSystemObject
{
    public Vector3 Position { get; private set; }
    public Vector3 Rotation { get; private set; }
    public Vector3 SelfRotationSpeed { get; private set; }
    public Vector3 SystemRotationSpeed { get; private set; }
    public Color Color{ get; private set; }
    public GameObject Object { get; private set; }
    public PrimitiveType PrimitiveType { get; private set; }
    public Vector3 Scale { get; private set; }
    
    private Vector3 _coordSystemZero;
    
    public RingObject(Vector3 initialPosition, Vector3 coordSystemZero, PrimitiveType primitiveType = PrimitiveType.Cube, float uniformScale = 1f)
    {
        this._coordSystemZero = coordSystemZero == default ? new Vector3(0f, 0f, 0f) : coordSystemZero;

        this.PrimitiveType = primitiveType;
        this.Object = GameObject.CreatePrimitive(this.PrimitiveType);

        this.Position = initialPosition == default ? new Vector3(0f, 0f, 0f) : initialPosition;
        this.Object.transform.position = this.Position;

        this.Scale = new Vector3(uniformScale, uniformScale, uniformScale);
        this.Object.transform.localScale = this.Scale;
    }

    public void SetPosition(
        float radialDistance, float angle, System.Random random = default, Distributions distribution = default, 
        Vector3 overheads = default, float minDeviation = -1000f, float maxDeviation = 1000f)
    {
        float xPos = radialDistance * Mathf.Sin(angle * Mathf.PI / 180) + overheads.x;
        float yPos = this._coordSystemZero.y + overheads.y;
        float zPos = this._coordSystemZero.z - radialDistance * Mathf.Cos(angle * Mathf.PI / 180) + overheads.z;

        float devDiff = maxDeviation - minDeviation;

        if (distribution == Distributions.White)
        {
            xPos = (float)(random.NextDouble() * devDiff + minDeviation) + xPos;
            yPos = (float)(random.NextDouble() * devDiff + minDeviation) + yPos;
            zPos = (float)(random.NextDouble() * devDiff + minDeviation) + zPos;
        }

        this.Position = new Vector3(xPos, yPos, zPos);
        this.Object.transform.position = this.Position;
    }

    public void SetUniformScale(float uniformScale)
    {
        if (this.Object == null) return;
        this.Object.transform.localScale = new Vector3(uniformScale, uniformScale, uniformScale); 
    }

    public void SetInitialRotation(Vector3 rotation)
    {
        if (rotation == null) return;
        if (this.Object == null) return;

        this.Rotation = rotation;
        this.Object.transform.Rotate(this.Rotation, Space.Self);
    }

    public void SetSelfRotationSpeed(Vector3 rotationSpeed)
    {
        if (rotationSpeed == null) return;
        this.SelfRotationSpeed = rotationSpeed;
    }

    public void SetSystemRotationSpeed(Vector3 rotationSpeed)
    {
        if (rotationSpeed == null) return;
        this.SystemRotationSpeed = rotationSpeed;
    }

    public void SetColor(Color color)
    {
        if (color == null) return;
        this.Color = color;

        Renderer objectRenderer = this.Object.GetComponent<Renderer>();
        if (objectRenderer == null) return;

        objectRenderer?.material.SetColor("_Color", this.Color);
    }

    public void SetObject(GameObject gameObject)
    {
        if (gameObject == null) return;
        this.Object = gameObject;
    }
}