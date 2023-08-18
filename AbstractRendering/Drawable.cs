namespace AbstractRendering;

public static class Current
{
    public static Scene Scene;
}

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
    public int StartPointer;
    public int PointerSize;
    public abstract void Draw();
}


public class Line : Drawable
{
    public Line(Vec2 start, Vec2 end, float width = 1f)
    {
        PointerSize = 9;
        
        Current.Scene.Set2V(StartRef,start);
        Current.Scene.Set2V(EndRef,end);
        Current.Scene.SetV(WidthRef,width);
    }
    
    // Properties:
    // start,  end,  width,  color
    // (0,1), (2,3),  (4),  (5,6,7,8)
    
    public int StartRef => StartPointer + 0;
    public int EndRef => StartPointer + 2;
    public int WidthRef => StartPointer + 4;
    public int ColorRef => StartPointer + 5;
    

    public override string ToString() => ((Vec2)Current.Scene.Get2V(StartRef))+","+((Vec2)Current.Scene.Get2V(EndRef));

    public static RenderImplementation Implementation = new EmptyImplementation();
    public override void Draw() => Implementation.Draw(this);
}

public class Text : Drawable
{
    public string Message;
    public int FontId;
    public bool Centered;

    
    public Text(string message, int fontId, bool centered = false)
    {
        PointerSize = 12;
        
        Message = message;
        FontId = fontId;
        Centered = centered;
    }
    
    // Properties:
    //  pos,  size, width, color,  out-color
    // (0,1), (2),  (3), (4,5,6,7)  (8,9,10,11)
    
    public int PosRef => StartPointer + 0;
    public int CharacterSizeRef => StartPointer + 2;
    public int OutlineWidthRef => StartPointer + 3;
    public int ColorRef => StartPointer + 4;
    public int OutlineColorRef => StartPointer + 8;
    
    public override string ToString() => Message;
    
    public static RenderImplementation Implementation = new EmptyImplementation();
    public override void Draw() => Implementation.Draw(this);
}

public abstract class Shape : Drawable
{
    // Properties:
    //  rotation, width,  color,  out-color
    //    (0),     (1), (2,3,4,5), (6,7,8,9)

    public int RotationRef => StartPointer + 0;
    public int OutlineWidthRef => StartPointer + 1;
    public int ColorRef => StartPointer + 2;
    public int OutlineColorRef => StartPointer + 6;
    
    public int TextureId = -1;
}

public class Circle : Shape
{
    // Properties:
    //  shape,   pos,   radius
    //  (0..9), (10,11),  (12)

    public int PosRef => StartPointer + 10;
    public int RadiusRef => StartPointer + 12;

    public Circle()
    {
        PointerSize = 13;
    }
    
    public override string ToString() => ((Vec2)Current.Scene.Get2V(PosRef))+","+Current.Scene.GetV(RadiusRef);

    public static RenderImplementation Implementation = new EmptyImplementation();
    public override void Draw() => Implementation.Draw(this);
}

public class Rectangle : Shape
{
    // Properties:
    //  shape,    p1,     p2
    //  (0..9), (10,11), (12,13)
    
    public int P1Ref => StartPointer + 10;
    public int P2Ref => StartPointer + 12;

    public Rectangle()
    {
        PointerSize = 14;
    }

    public override string ToString() => ((Vec2)Current.Scene.Get2V(P1Ref))+","+((Vec2)Current.Scene.Get2V(P2Ref));
    
    public static RenderImplementation Implementation = new EmptyImplementation();
    public override void Draw() => Implementation.Draw(this);
}

/*
public class ConvexShape : Shape
{
    public Vec2[] Verts;

    public ConvexShape(Vec2[] verts)
    {
        PointerSize = 
        
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
}*/

public class Polygon : Shape
{
    // Properties:
    //  shape,   pos,   radius, sides
    //  (0..9), (10,11), (12),  (13)

    public int PosRef => StartPointer + 10;
    public int RadiusRef => StartPointer + 12;
    public int NumSidesRef => StartPointer + 13;

    public Polygon()
    {
        PointerSize = 14;
    }

    public override string ToString() => ((Vec2)Current.Scene.Get2V(PosRef))+","+Current.Scene.GetV(RadiusRef)+","+Current.Scene.GetV(NumSidesRef);

    public static RenderImplementation Implementation = new EmptyImplementation();
    public override void Draw() => Implementation.Draw(this);
}