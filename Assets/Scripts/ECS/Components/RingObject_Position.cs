using Unity.Entities;

[GenerateAuthoringComponent]
public struct RingObject_Position : IComponentData
{
    public float Angle;
    public float Radius;
}
