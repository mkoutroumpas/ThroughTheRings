using UnityEngine;
public class RingObject : IRingObject
{
    public Vector3 Position { get { return this.Object.transform.position; } private set {} }
    public Vector3 Rotation { get; private set; }
    public Vector3 RotationSpeed { get; private set; }
    public Color Color{ get; private set; }
    public GameObject Object { get; private set; }
    public PrimitiveType PrimitiveType { get; private set; }
    public Renderer Renderer { get; private set; }
    public Vector3 Scale { get; private set; }

    private Vector3 CoordSystemZero;

    public RingObject(Vector3 initialPosition, Vector3 coordSystemZero, PrimitiveType primitiveType = PrimitiveType.Cube, float uniformScale = 1f)
    {
        this.PrimitiveType = primitiveType;
        this.Object = GameObject.CreatePrimitive(this.PrimitiveType);
        this.Position = initialPosition == null ? new Vector3(0f, 0f, 0f) : initialPosition;
        this.Scale = new Vector3(uniformScale, uniformScale, uniformScale);
        this.CoordSystemZero = coordSystemZero;
    }

    public void SetPosition(
        float radialDistance, float angle, System.Random random = default, Distributions distribution = default, 
        Vector3 overheads = default, float minDeviation = -1000f, float maxDeviation = 1000f)
    {
        float xPos = radialDistance * Mathf.Sin(angle * Mathf.PI / 180) + overheads.x;
        float yPos = this.CoordSystemZero.y + overheads.y;
        float zPos = this.CoordSystemZero.z - radialDistance * Mathf.Cos(angle * Mathf.PI / 180) + overheads.z;

        if (distribution == Distributions.White)
        {
            xPos = (float)(random.NextDouble() * (maxDeviation - minDeviation) + minDeviation) + xPos;
            yPos = (float)(random.NextDouble() * (maxDeviation - minDeviation) + minDeviation) + yPos;
            zPos = (float)(random.NextDouble() * (maxDeviation - minDeviation) + minDeviation) + zPos;
        }

        this.Object.transform.position = new Vector3(xPos, yPos, zPos);
    }

    public void SetUniformScale(float uniformScale)
    {
        if (this.Object == null) return;
        this.Object.transform.localScale = new Vector3(uniformScale, uniformScale, uniformScale); 
    }

    public void SetInitialRotation(Vector3 rotation)
    {
        if (rotation == null) return;
        this.Rotation = rotation;
    }

    public void SetRotationSpeed(Vector3 rotationSpeed)
    {
        if (rotationSpeed == null) return;
        this.RotationSpeed = rotationSpeed;

        // TODO: Also implement ability to change rotation speed 'on-the-fly' (guess will be handled by Unity's thread ...)
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