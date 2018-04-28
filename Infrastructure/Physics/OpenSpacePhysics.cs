namespace Infrastructure.Physics
{
    public class OpenSpacePhysics : Physics
    {
        public override void Update(double dt)
        {
            foreach (var obj in Objects)
                obj.Update(dt);
        }
    }
}