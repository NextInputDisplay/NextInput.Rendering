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
    
    public Scene()
    {
        _renderList = new SortedList<int, Drawable>();
    }

    public void Add(Drawable drawable, int layer = Int32.MaxValue)
    {
        if (layer == Int32.MaxValue) layer = (_renderList.Keys.Count > 0)?_renderList.Keys.Max()+1:0;
        _renderList.Add(layer,drawable);
    }

    public IList<Drawable> ToRender => _renderList.Values;
}