using AbstractRendering;
using NextInput.Input;
using NextInput.Input.SDL;
using SFML.Window;

namespace InputSFML;

public static class Input
{
    private static IInputBackend _inputBackend;

    private static IGameController _gameController;
    
    public static void Init()
    {
        InputBackendManager.RegisterInputBackend<SDLInputBackend>();
        _inputBackend = InputBackendManager.GetInputBackend();

        _gameController = _inputBackend.GetGameController(_inputBackend.GameControllers.First());
    }

    public static void Update()
    {
        _inputBackend.UpdateGameControllers();
    }
    
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
    
    public static float GetRightTrigger() => _gameController.GetAxis(GameControllerAxes.TriggerRight);
    

    //public static float GetLeftStickX() => (Joystick.GetAxisPosition(0,Joystick.Axis.X) + 100f) / 200f;
    
    //public static float GetLeftStickY() => (Joystick.GetAxisPosition(0,Joystick.Axis.Y) + 100f) / 200f;
    
    public static float GetLeftStickX() => _gameController.GetAxis(GameControllerAxes.LeftX);
    public static float GetLeftStickY() => _gameController.GetAxis(GameControllerAxes.LeftY);
    
    
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


    public static float GetButtonA() => _gameController.GetButton(GameControllerButtons.A) ? 1f : 0f;
    public static float GetButtonB() => _gameController.GetButton(GameControllerButtons.B) ? 1f : 0f;
    public static float GetButtonX() => _gameController.GetButton(GameControllerButtons.X) ? 1f : 0f;
    public static float GetButtonY() => _gameController.GetButton(GameControllerButtons.Y) ? 1f : 0f;
    
    public static float GetButtonStart() => _gameController.GetButton(GameControllerButtons.Start) ? 1f : 0f;
    public static float GetButtonSelect() => _gameController.GetButton(GameControllerButtons.Back) ? 1f : 0f;
    
    public static float GetButtonL3() => _gameController.GetButton(GameControllerButtons.LeftStick) ? 1f : 0f;
    public static float GetButtonR3() => _gameController.GetButton(GameControllerButtons.RightStick) ? 1f : 0f;

    public static float GetButtonL() => _gameController.GetButton(GameControllerButtons.LeftShoulder) ? 1f : 0f;
    public static float GetButtonR() => _gameController.GetButton(GameControllerButtons.RightShoulder) ? 1f : 0f;
    public static float GetButtonL2() => _gameController.GetAxis(GameControllerAxes.TriggerLeft);
    public static float GetButtonR2() => _gameController.GetAxis(GameControllerAxes.TriggerRight);
    

    public static void PrintButtons()
    {
        for (uint i = 0; i < Joystick.GetButtonCount(0); i++)
        {
            Console.Write((Joystick.IsButtonPressed(0,i)?1:0) + " ");
        }
        Console.WriteLine();
    }

    public static float GetDpadRight() => _gameController.GetButton(GameControllerButtons.DPadRight) ? 1f : 0f;
    public static float GetDpadLeft() => _gameController.GetButton(GameControllerButtons.DPadLeft) ? 1f : 0f;
    public static float GetDpadUp() => _gameController.GetButton(GameControllerButtons.DPadUp) ? 1f : 0f;
    public static float GetDpadDown() => _gameController.GetButton(GameControllerButtons.DPadDown) ? 1f : 0f;
    
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