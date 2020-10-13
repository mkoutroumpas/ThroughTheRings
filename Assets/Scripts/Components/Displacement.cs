using Unity.Entities;

namespace Assets.Scripts.Components
{
    //  Represent displacement on any axis.
    public struct Displacement : IComponentData
    {
        public float Value;
    }
}
