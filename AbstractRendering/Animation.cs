namespace AbstractRendering;

public class Animator
{
    List<KeyFrameHandler> handlers;

    public delegate float ValueFunction();

    public ValueFunction GetValue;
    
    public Animator(ValueFunction valueFunction)
    {
        handlers = new List<KeyFrameHandler>();
        GetValue = valueFunction;
    }
    
    public void Update()
    {
        Set(GetValue());
    }

    public void Set(float value)
    {
        foreach (var handler in handlers)
        {
            handler.Update(value);
        }

    }

    public void Add(KeyFrameHandler handler) => handlers.Add(handler);
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
    private Property _property;

    public KeyFrameHandler(ref Property property)
    {
        _keyFrames = new List<KeyFrame>();
        _property = property;
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
        _property.Value = _keyFrames[index].Value * (1f - lerp) + _keyFrames[index + 1].Value * lerp;
    }
}
