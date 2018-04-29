namespace Core
{
    public interface ITimerListener
    {
        void HandleTick(double dt);
    }
}