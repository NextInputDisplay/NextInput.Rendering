using SFML.Window;

namespace InputSFML;

public static class Input
{
    public static float GetRightTrigger()
    {
        return (Joystick.GetAxisPosition(0, Joystick.Axis.R) + 100f) / 200f;
    }
    
}