namespace AbstractRendering;

public class Handler
{
    private Animator.ValueFunction _valueFunction;
    private List<KeyFrameHandler> _keyFrameHandlers;

    public Handler(Animator.ValueFunction valueFunction, params KeyFrameHandler[] keyFrameHandlers)
    {
        _valueFunction = valueFunction;
        _keyFrameHandlers = new List<KeyFrameHandler>(keyFrameHandlers);
    }

    public void Update()
    {
        float value = _valueFunction();

        foreach (var keyFrameHandler in _keyFrameHandlers)
        {
            keyFrameHandler.Update(value);
        }
    }

    public void Add(KeyFrameHandler handler) => _keyFrameHandlers.Add(handler);
}

public class Animator
{
    public delegate float ValueFunction();

    private List<Handler> _handlers;
    
    public Animator()
    {
        _handlers = new List<Handler>();
    }
    
    public void Update()
    {
        foreach (var handler in _handlers)
        {
            handler.Update();
        }
    }

    public void Add(params Handler[] handlers)
    {
        foreach (var handler in handlers)
        {
            _handlers.Add(handler);
        }
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

public class KeyFrameHandler
{
    private List<KeyFrame> _keyFrames;
    private int _propertyPointer;

    public KeyFrameHandler(int propertyPointer)
    {
        _keyFrames = new List<KeyFrame>();
        _propertyPointer = propertyPointer;
    }
    
    public void Add(float time, float value) => _keyFrames.Add(new KeyFrame(time,value));
    
    
    private (int,float) GetLerp(float time)
    {
        float lerp = 0f;
        int i = 0;
        
        for (; i < _keyFrames.Count-1; i++)
        {
            float delta = _keyFrames[i + 1].Time - _keyFrames[i].Time;
            if (delta > time)
            {
                lerp = time / delta;
                break;
            }

            time -= delta;
        }

        if (i == _keyFrames.Count-1)
        {
            lerp = 1f;
            i--;
        }

        return (i, lerp);
    }
    
    public void Update(float time)
    {
        var (index, lerp) = GetLerp(time);
        Current.Scene.Values[_propertyPointer] = _keyFrames[index].Value * (1f - lerp) + _keyFrames[index + 1].Value * lerp;
    }
}
