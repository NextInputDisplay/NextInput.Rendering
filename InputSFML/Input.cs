using SFML.Window;

namespace InputSFML;

public static class Input
{
    public static float GetRightTrigger()
    {
        return (Joystick.GetAxisPosition(0, Joystick.Axis.R) + 100f) / 200f;
    }

    public static uint CurrentButton = 1;
    public static float GetButton()
    {
        return Joystick.IsButtonPressed(0, CurrentButton)?1f:0f;
    }
    
}