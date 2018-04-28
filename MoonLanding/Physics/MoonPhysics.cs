using MoonLanding.Tools;

namespace MoonLanding.Physics
{
    public class MoonPhysics : Physics
    {
        private readonly Vector Gravity = Vector.Create(0, 1.62);
        
        public override void Update(double dt)
        {
            foreach (var obj in Objects)
                UpdateObject(obj, dt);
        }

        private void UpdateObject(IPhysObject obj, double dt)
        {
            var actualAcceleration = obj.Acceleration + Gravity;
            obj.Velocity += actualAcceleration * dt;
            obj.Cords += obj.Velocity * dt;
            
            obj.Update(dt);
        }
    }
}