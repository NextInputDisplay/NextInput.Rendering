namespace AbstractRendering;

public abstract class RenderImplementation
{
    public abstract void Draw(Drawable drawable);
}

public class EmptyImplementation : RenderImplementation
{
    public override void Draw(Drawable drawable) { throw new NotImplementedException(); }
}

public abstract class Drawable
{
    public abstract void Draw();
}


public class Line : Drawable
{
    public Vec2 Start, End;
    public float Width;
    public Color Color = Color.Black;

    public Line(Vec2 start, Vec2 end, float width = 1f)
    {
        Start = start;
        End = end;
        Width = width;
    }
    
    public override string ToString() => Start+","+End;

    public static RenderImplementation Implementation = new EmptyImplementation();
    public override void Draw() => Implementation.Draw(this);
}


public class ShapeProperties
{
    public Color Color = Color.Black;
    public Color OutlineColor = Color.Black;
    public Property OutlineWidth = 0f;
    public Property Rotation = 0f;
    public int TextureId = -1;
}

public abstract class Shape : Drawable
{
    public ShapeProperties Properties;
}

public class Circle : Shape
{
    public Vec2 Pos;
    public Property Radius;

    public Circle(Vec2 pos, float radius)
    {
        Pos = pos; Radius = radius;
    }
    
    public override string ToString() => Pos+","+Radius;

    public static RenderImplementation Implementation = new EmptyImplementation();
    public override void Draw() => Implementation.Draw(this);
}

public class Rectangle : Shape
{
    public Vec2 P1,P2;

    public Rectangle(Vec2 p1, Vec2 p2)
    {
        P1 = p1; P2 = p2;
    }

    public override string ToString() => P1+","+P2;
    
    public static RenderImplementation Implementation = new EmptyImplementation();
    public override void Draw() => Implementation.Draw(this);
}

public class ConvexShape : Shape
{
    public Vec2[] Verts;

    public ConvexShape(Vec2[] verts)
    {
        Verts = verts;
    }

    public override string ToString()
    {
        string str = "{";
        for (int i = 0; i < Verts.Length-1; i++)
        {
            str += Verts[i] + ",";
        }

        return str + Verts[^1] + "}";
    }
    
    public static RenderImplementation Implementation = new EmptyImplementation();
    public override void Draw() => Implementation.Draw(this);
}

public class Polygon : Shape
{
    public Vec2 Pos;
    public Property Radius;
    public Property NumSides;

    public Polygon(Vec2 pos, float radius, int numSides)
    {
        Pos = pos; Radius = radius; NumSides = numSides;
    }

    public override string ToString() => Pos+","+Radius+","+NumSides;

    public static RenderImplementation Implementation = new EmptyImplementation();
    public override void Draw() => Implementation.Draw(this);
}