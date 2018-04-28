namespace Infrastructure.Physics
{
    public interface IPhysicsFactory
    {
        IPhysics FromName(string name);
    }
}