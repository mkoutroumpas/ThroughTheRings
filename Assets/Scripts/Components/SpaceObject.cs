using Unity.Entities;

namespace Assets.Scripts.Components
{
    //  Descriptor of an object in space in general.
    [GenerateAuthoringComponent]
    public struct SpaceObject : IComponentData
    {
        public int VelocityMetersPerSecond;
        public float TranslationRate;
        public bool Backward;
    }
}
