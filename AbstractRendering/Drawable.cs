namespace AbstractRendering;

public abstract class RenderImplementation
{
    public virtual void Draw(Drawable drawable) { throw new NotImplementedException(); }
}

public abstract class Drawable
{
    public abstract void Draw();
}

public abstract class Shape : Drawable
{
    public Color Color = Color.Black;
}

public class Circle : Shape
{
    public float X, Y, Radius;

    public Circle(float x, float y, float radius, Color color)
    {
        X = x; Y = y; Radius = radius;
        Color = color;
    }
    
    public override string ToString() => "{"+X+","+Y+","+Radius+"}";

    public static RenderImplementation? Implementation = null;
    public override void Draw() => Implementation?.Draw(this);
}

public class Rectangle : Shape
{
    public float X1, Y1, X2, Y2;

    public Rectangle(float x1, float y1, float x2, float y2, Color color)
    {
        X1 = x1; Y1 = y1; X2 = x2; Y2 = y2;
        Color = color;
    }

    public override string ToString() => "{"+X1+","+Y1+","+X2+","+Y2+"}";
    
    public static RenderImplementation? Implementation = null;
    public override void Draw() => Implementation?.Draw(this);
}