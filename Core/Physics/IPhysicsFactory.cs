namespace Core.Physics
{
    public interface IPhysicsFactory
    {
        IPhysics FromName(string name);
    }
}