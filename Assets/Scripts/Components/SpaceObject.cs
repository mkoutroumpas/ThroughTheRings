using Unity.Entities;

namespace Assets.Scripts.Components
{
    [GenerateAuthoringComponent]
    public struct SpaceObject : IComponentData
    {
        public int VelocityMetersPerSecond;
        public float TranslationRate;
        public bool Backward;
    }
}
