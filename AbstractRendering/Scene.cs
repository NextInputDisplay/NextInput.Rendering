namespace AbstractRendering;

public struct RenderObject
{
    public Drawable Data;
}

public struct WindowProperties
{
    public Vec2 Size;
    public string Title;
}


public class Scene
{
    private SortedList<int, Drawable> _renderList;
    
    public float[] Values;
    public float GetV(int id) => Values[id];
    public (float,float) Get2V(int id) => (Values[id],Values[id + 1]);
    public (float,float,float,float) Get4V(int id) => (Values[id],Values[id + 1],Values[id + 2],Values[id+3]);
    public void SetV(int id,float v) => Values[id] = v;
    public void Set2V(int id, (float x, float y) v)
    {
        Values[id] = v.x;
        Values[id + 1] = v.y;
    }
    public void Set4V(int id, (float x, float y, float z, float w) v)
    {
        Values[id] = v.x;
        Values[id + 1] = v.y;
        Values[id + 2] = v.z;
        Values[id + 3] = v.w;
    }
    
    
    public Scene()
    {
        _renderList = new SortedList<int, Drawable>();
        Init();
    }

    public void Init()
    {
        Current.Scene = this;
    }

    private int startPointer = 0;
    public void Add(Drawable drawable, int layer = Int32.MaxValue)
    {
        drawable.StartPointer = startPointer;
        startPointer += drawable.PointerSize;
        if (layer == Int32.MaxValue) layer = (_renderList.Keys.Count > 0)?_renderList.Keys.Max()+1:0;
        _renderList.Add(layer,drawable);
    }

    public IList<Drawable> ToRender => _renderList.Values;
}