public class Timer
{
    float WaitTime;
    float LastCalled = 0;

    public Timer(float time)
    {
        WaitTime = time;
    }

    public bool CallAgain(float deltaTime)
    {
        LastCalled += deltaTime;
        if (LastCalled >= WaitTime)
        {
            LastCalled = 0;
            return true;
        }

        return false;
    }
}