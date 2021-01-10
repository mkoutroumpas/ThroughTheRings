using UnityEngine;
public class RingObject : IRingObject
{
    public Vector3 Position { get; private set; }
    public Vector3 Rotation { get; private set; }
    public Vector3 RotationSpeed { get; private set; }
    public Color Color{ get; private set; }
    public GameObject Object { get; private set; }
    public PrimitiveType PrimitiveType { get; private set; }
    public Renderer Renderer { get; private set; }
    public Vector3 Scale { get; private set; }

    public RingObject(PrimitiveType primitiveType = PrimitiveType.Cube, float uniformScale = 1f)
    {
        this.PrimitiveType = primitiveType;
        this.Object = GameObject.CreatePrimitive(this.PrimitiveType);
    }

    public void SetPosition(float radialDistance, float angle)
    {
        // TODO: Trigonometrically set position coordinates.
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