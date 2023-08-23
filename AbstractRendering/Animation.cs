namespace AbstractRendering;

public class Handler
{
    public string FunctionName;
    
    private Animator.ValueFunction _valueFunction;
    public List<KeyFrameHandler> KeyFrameHandlers;

    public Handler(string functionName, Animator.ValueFunction valueFunction, params KeyFrameHandler[] keyFrameHandlers)
    {
        FunctionName = functionName;
        
        _valueFunction = valueFunction;
        KeyFrameHandlers = new List<KeyFrameHandler>(keyFrameHandlers);
    }

    public void Update()
    {
        float value = _valueFunction();

        foreach (var keyFrameHandler in KeyFrameHandlers)
        {
            keyFrameHandler.Update(value);
        }
    }

    public void Add(KeyFrameHandler handler) => KeyFrameHandlers.Add(handler);
}

public class Animator
{
    public delegate float ValueFunction();

    public List<Handler> Handlers;

    private Dictionary<string, ValueFunction> _dictionary;
    
    public Animator(Dictionary<string, ValueFunction> dict)
    {
        _dictionary = dict;
        Handlers = new List<Handler>();
    }
    
    public void Update()
    {
        foreach (var handler in Handlers)
        {
            handler.Update();
        }
    }

    public void Add(string name, params KeyFrameHandler[] keyFrameHandlers)
    {
        Handlers.Add(new Handler(name, _dictionary[name], keyFrameHandlers));
    }
}

public class KeyFrame
{
    public float Time;
    public float Value;

    public KeyFrame(float time, float value)
    {
        Time = time;
        Value = value;
    }
}

public class SimpleButtonKf : KeyFrameHandler
{
    public SimpleButtonKf(int propertyPointer, float min, float max) : base(propertyPointer)
    {
        Add(0f,min);
        Add(1f,max);
    }
}

public class KeyFrameHandler
{
    public List<KeyFrame> KeyFrames;
    public int PropertyPointer;

    public KeyFrameHandler(int propertyPointer)
    {
        KeyFrames = new List<KeyFrame>();
        PropertyPointer = propertyPointer;
    }
    
    public void Add(float time, float value) => KeyFrames.Add(new KeyFrame(time,value));
    
    
    private (int,float) GetLerp(float time)
    {
        float lerp = 0f;
        int i = 0;
        
        for (; i < KeyFrames.Count-1; i++)
        {
            float delta = KeyFrames[i + 1].Time - KeyFrames[i].Time;
            if (delta > time)
            {
                lerp = time / delta;
                break;
            }

            time -= delta;
        }

        if (i == KeyFrames.Count-1)
        {
            lerp = 1f;
            i--;
        }

        return (i, lerp);
    }
    
    public void Update(float time)
    {
        var (index, lerp) = GetLerp(time);
        Current.Scene.Values[PropertyPointer] = KeyFrames[index].Value * (1f - lerp) + KeyFrames[index + 1].Value * lerp;
    }
}
