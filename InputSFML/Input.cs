using AbstractRendering;
using SFML.Window;

namespace InputSFML;

public static class Input
{
    public static Dictionary<string, Animator.ValueFunction> InputFunctions = new()
    {
        {"RightTrigger", GetRightTrigger},
        {"LeftStickX", GetLeftStickX},
        {"LeftStickY", GetLeftStickY},
        {"AdjLeftStickX", GetAdjustedLeftStickX},
        {"AdjLeftStickY", GetAdjustedLeftStickY},
        {"ButtonA", GetButtonA},
        {"ButtonB", GetButtonB},
    };
    
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


    public static float GetButtonA()
    {
        return Joystick.IsButtonPressed(0, 0)?1f:0f;
    }
    
    public static float GetButtonB()
    {
        return Joystick.IsButtonPressed(0, 1)?1f:0f;
    }
    
}