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
        {"AdjRightStickX", GetAdjustedRightStickX},
        {"AdjLeftStickY", GetAdjustedLeftStickY},
        {"AdjRightStickY", GetAdjustedRightStickY},
        {"ButtonA", GetButtonA},
        {"ButtonB", GetButtonB},
        {"ButtonX", GetButtonX},
        {"ButtonY", GetButtonY},
        {"ButtonL", GetButtonL},
        {"ButtonR", GetButtonR},
        {"ButtonL2", GetButtonL2},
        {"ButtonR2", GetButtonR2},
        {"ButtonL3", GetButtonL3},
        {"ButtonR3", GetButtonR3},
        {"ButtonStart", GetButtonStart},
        {"ButtonSelect", GetButtonSelect},
        {"DpadRight", GetDpadRight},
        {"DpadLeft", GetDpadLeft},
        {"DpadUp", GetDpadUp},
        {"DpadDown", GetDpadDown},
    };
    
    public static float GetRightTrigger() => (Joystick.GetAxisPosition(0, Joystick.Axis.R) + 100f) / 200f;
    

    public static float GetLeftStickX() => (Joystick.GetAxisPosition(0,Joystick.Axis.X) + 100f) / 200f;
    
    public static float GetLeftStickY() => (Joystick.GetAxisPosition(0,Joystick.Axis.Y) + 100f) / 200f;
    
    
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
    
    
    public static float GetAdjustedRightStickX()
    {
        float x = Joystick.GetAxisPosition(0, Joystick.Axis.U) / 100f;
        float y = Joystick.GetAxisPosition(0, Joystick.Axis.V) / 100f;

        float l = x * x + y * y;
        if (l > 1f)
        {
            x /= MathF.Sqrt(l);
        }

        return (x + 1f) / 2f;
    }
    public static float GetAdjustedRightStickY()
    {
        float x = Joystick.GetAxisPosition(0, Joystick.Axis.U) / 100f;
        float y = Joystick.GetAxisPosition(0, Joystick.Axis.V) / 100f;

        float l = x * x + y * y;
        if (l > 1f)
        {
            y /= MathF.Sqrt(l);
        }

        return (y + 1f) / 2f;
    }


    public static float GetButtonA() => Joystick.IsButtonPressed(0, 0)? 1f:0f;
    public static float GetButtonB() => Joystick.IsButtonPressed(0, 1)?1f:0f;
    public static float GetButtonX() => Joystick.IsButtonPressed(0, 2)?1f:0f;
    public static float GetButtonY() => Joystick.IsButtonPressed(0, 3)?1f:0f;
    
    public static float GetButtonStart() => Joystick.IsButtonPressed(0, 7)?1f:0f;
    public static float GetButtonSelect() => Joystick.IsButtonPressed(0, 6)?1f:0f;
    
    public static float GetButtonL3() => Joystick.IsButtonPressed(0, 9)?1f:0f;
    public static float GetButtonR3() => Joystick.IsButtonPressed(0, 10)?1f:0f;

    public static float GetButtonL() => Joystick.IsButtonPressed(0, 4) ? 1f : 0f;
    public static float GetButtonR() => Joystick.IsButtonPressed(0, 5) ? 1f : 0f;
    public static float GetButtonL2() => (Joystick.GetAxisPosition(0,Joystick.Axis.Z) + 100f) / 200f;
    public static float GetButtonR2() => (Joystick.GetAxisPosition(0,Joystick.Axis.R) + 100f) / 200f;
    

    public static void PrintButtons()
    {
        for (uint i = 0; i < Joystick.GetButtonCount(0); i++)
        {
            Console.Write((Joystick.IsButtonPressed(0,i)?1:0) + " ");
        }
        Console.WriteLine();
    }

    public static float GetDpadRight() => Joystick.GetAxisPosition(0, Joystick.Axis.PovX) > 0 ? 1f : 0f;
    public static float GetDpadLeft() => Joystick.GetAxisPosition(0, Joystick.Axis.PovX) < 0 ? 1f : 0f;
    public static float GetDpadUp() => Joystick.GetAxisPosition(0, Joystick.Axis.PovY) < 0 ? 1f : 0f;
    public static float GetDpadDown() => Joystick.GetAxisPosition(0, Joystick.Axis.PovY) > 0 ? 1f : 0f;
    
    public static void PrintAxes()
    {
        Console.Write(Joystick.GetAxisPosition(0,Joystick.Axis.X)+"|");
        Console.Write(Joystick.GetAxisPosition(0,Joystick.Axis.Y)+"|");
        Console.Write(Joystick.GetAxisPosition(0,Joystick.Axis.R)+"|");
        Console.Write(Joystick.GetAxisPosition(0,Joystick.Axis.V)+"|");
        Console.Write(Joystick.GetAxisPosition(0,Joystick.Axis.Z)+"|");
        Console.Write(Joystick.GetAxisPosition(0,Joystick.Axis.U)+"|");
        Console.Write(Joystick.GetAxisPosition(0,Joystick.Axis.PovX)+"|");
        Console.Write(Joystick.GetAxisPosition(0,Joystick.Axis.PovY)+"|");
        Console.WriteLine();
    }
    
}