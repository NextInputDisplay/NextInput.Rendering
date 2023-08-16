using SFML.Window;

namespace InputSFML;

public static class Input
{
    public static float GetRightTrigger()
    {
        return (Joystick.GetAxisPosition(0, Joystick.Axis.R) + 100f) / 200f;
    }

    public static float GetLeftStickX()
    {
        return (Joystick.GetAxisPosition(0,Joystick.Axis.X) + 100f) / 200f;
    }
    public static float GetLeftStickY()
    {
        return (Joystick.GetAxisPosition(0,Joystick.Axis.Y) + 100f) / 200f;
    }
    
    public static float GetAdjustedLeftStickX()
    {
        float x = Joystick.GetAxisPosition(0, Joystick.Axis.X) / 100f;
        float y = Joystick.GetAxisPosition(0, Joystick.Axis.Y) / 100f;

        float l = x * x + y * y;
        if (l > 1f)
        {
            x /= MathF.Sqrt(l);
        }

        return (x + 1f) / 2f;
    }
    public static float GetAdjustedLeftStickY()
    {
        float x = Joystick.GetAxisPosition(0, Joystick.Axis.X) / 100f;
        float y = Joystick.GetAxisPosition(0, Joystick.Axis.Y) / 100f;

        float l = x * x + y * y;
        if (l > 1f)
        {
            y /= MathF.Sqrt(l);
        }

        return (y + 1f) / 2f;
    }

    public static uint CurrentButton = 1;
    public static float GetButton()
    {
        return Joystick.IsButtonPressed(0, CurrentButton)?1f:0f;
    }
    
}