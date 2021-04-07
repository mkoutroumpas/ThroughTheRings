using Unity.Entities;

[UpdateInGroup(typeof(SimulationSystemGroup))]
public class RingSystem_Player : SystemBase
{
    #region Private variables
    EntityQuery ringObjectQuery;
    #endregion

    #region System overrides
    protected override void OnCreate()
    {
        ringObjectQuery = GetEntityQuery(typeof(RingObject_RotationSpeed), typeof(RingObject_Appearance), typeof(RingObject_Position), typeof(RingObject_SystemData));
    }
    protected override void OnUpdate()
    {

    }
    #endregion
}