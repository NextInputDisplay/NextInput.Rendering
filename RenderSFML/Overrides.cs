using AbstractRendering;
using SFML.Graphics;
using SFML.System;
using ConvexShape = AbstractRendering.ConvexShape;
using Drawable = AbstractRendering.Drawable;
using Shape = SFML.Graphics.Shape;
using Text = SFML.Graphics.Text;

namespace RenderSFML;


public static class Implementation
{
    public static void Init()
    {
        Line.Implementation = new RenderLine();
        Circle.Implementation = new RenderCircle();
        Rectangle.Implementation = new RenderRect();
        ConvexShape.Implementation = new RenderConvexShape();
        Polygon.Implementation = new RenderPolygon();
        AbstractRendering.Text.Implementation = new RenderText();
    }
    
    public static void ApplyProperties(Shape sfmlShape, AbstractRendering.Shape shape)
    {
        if (shape.TextureId == -1)
        {
            sfmlShape.FillColor = Convert.Color(Renderer.Scene.Get4V(shape.ColorRef));
        }
        else
        {
            sfmlShape.Texture = Renderer.Textures[shape.TextureId];
            
            // FIXME: This is a giant hack so that we can make rectangle with textures disappear as we want
            if (Renderer.Scene.GetV(shape.ColorRef + 4) == 0.0)
                sfmlShape.TextureRect = new IntRect(0, 0, (int)sfmlShape.Texture.Size.X, (int)sfmlShape.Texture.Size.Y);
            else
                sfmlShape.TextureRect = new IntRect(0, 0, 0, 0);
        }
        
        sfmlShape.OutlineColor = Convert.Color(Renderer.Scene.Get4V(shape.OutlineColorRef));
        sfmlShape.OutlineThickness = Renderer.Scene.GetV(shape.OutlineWidthRef);
        sfmlShape.Rotation = Renderer.Scene.GetV(shape.RotationRef);
    }

}

public class RenderLine : RenderImplementation
{
    public override void Draw(Drawable drawable)
    {
        Line line = (Line)drawable;

        float radius = Renderer.Scene.GetV(line.WidthRef) / 2f;
        CircleShape circle = new CircleShape(radius);
        circle.FillColor = Convert.Color(Renderer.Scene.Get4V(line.ColorRef));

        Vec2 radiusOffset = new Vec2(radius, radius);

        Vec2 start = Renderer.Scene.Get2V(line.StartRef);
        Vec2 end = Renderer.Scene.Get2V(line.EndRef);
        
        circle.Position = Convert.Vec2(start - radiusOffset);
        Renderer.Window.Draw(circle);
        circle.Position = Convert.Vec2(end - radiusOffset);
        Renderer.Window.Draw(circle);

        Vec2 difference = end - start;
        Vec2 offset = new Vec2(difference.Y, -difference.X).Normalized * radius;
        
        var convexShape = new SFML.Graphics.ConvexShape();
        convexShape.SetPointCount(4);
        convexShape.SetPoint(0,Convert.Vec2(start+offset));
        convexShape.SetPoint(1,Convert.Vec2(start-offset));
        convexShape.SetPoint(2,Convert.Vec2(end-offset));
        convexShape.SetPoint(3,Convert.Vec2(end+offset));
        convexShape.FillColor = circle.FillColor;
        
        Renderer.Window.Draw(convexShape);
    }
}

public class RenderText : RenderImplementation
{
    public override void Draw(Drawable drawable)
    {
        var text = (AbstractRendering.Text)drawable;

        Text sfmlText = new Text(text.Message, Renderer.Fonts[text.FontId]);
        sfmlText.OutlineThickness = Renderer.Scene.GetV(text.OutlineWidthRef);
        sfmlText.OutlineColor = Convert.Color(Renderer.Scene.Get4V(text.OutlineColorRef));
        sfmlText.FillColor = Convert.Color(Renderer.Scene.Get4V(text.ColorRef));
        sfmlText.CharacterSize = (uint)Renderer.Scene.GetV(text.CharacterSizeRef);
        sfmlText.Position = Convert.Vec2(Renderer.Scene.Get2V(text.PosRef));

        if (text.Centered)
        {
            var g = sfmlText.GetGlobalBounds();
            var l = sfmlText.GetLocalBounds();
            sfmlText.Origin = new Vector2f(MathF.Round(g.Width / 2f + l.Left), MathF.Round(g.Height / 2f + l.Top));
        }

        Renderer.Window.Draw(sfmlText);
    }
}

public class RenderCircle : RenderImplementation
{
    private static CircleShape _shape;
    public override void Draw(Drawable drawable)
    {
        Circle circle = (Circle)drawable;

        _shape = new CircleShape(Renderer.Scene.GetV(circle.RadiusRef), (uint)Renderer.Scene.GetV(circle.RadiusRef) / 2);
        
        _shape.Position = Convert.Vec2(Renderer.Scene.Get2V(circle.PosRef));
        
        _shape.Origin = new Vector2f(_shape.Radius, _shape.Radius);

        Implementation.ApplyProperties(_shape, circle);
        
        Renderer.Window.Draw(_shape);
    }
}

public class RenderRect : RenderImplementation
{
    private static RectangleShape _shape = new();
    public override void Draw(Drawable drawable)
    {
        Rectangle rect = (Rectangle)drawable;
        Vec2 p1 = Renderer.Scene.Get2V(rect.P1Ref);
        Vec2 p2 = Renderer.Scene.Get2V(rect.P2Ref);

        _shape.Position = Convert.Vec2(p1);
        _shape.Size = Convert.Vec2(p2-p1);
        
        Implementation.ApplyProperties(_shape, rect);
        
        Renderer.Window.Draw(_shape);
    }
}


public class RenderConvexShape : RenderImplementation
{
    private static SFML.Graphics.ConvexShape _sfmlShape = new();
    public override void Draw(Drawable drawable)
    {
        ConvexShape shape = (ConvexShape)drawable;
        
        _sfmlShape.SetPointCount((uint)shape.NumVerts);

        for (int i = 0; i < shape.NumVerts; i++)
        {
            _sfmlShape.SetPoint((uint)i,Convert.Vec2( Renderer.Scene.Get2V(shape.VertsRef + i * 2) ));
        }

        _sfmlShape.Position = Convert.Vec2(Renderer.Scene.Get2V(shape.PosRef));
        
        Implementation.ApplyProperties(_sfmlShape, shape);
        
        Renderer.Window.Draw(_sfmlShape);
    }
}

public class RenderPolygon : RenderImplementation
{
    private static CircleShape _shape = new();
    public override void Draw(Drawable drawable)
    {
        Polygon polygon = (Polygon)drawable;

        _shape = new CircleShape(Renderer.Scene.GetV(polygon.RadiusRef), (uint)Renderer.Scene.GetV(polygon.NumSidesRef));
        _shape.Position = Convert.Vec2(Renderer.Scene.Get2V(polygon.PosRef));
        
        _shape.Origin = new Vector2f(_shape.Radius, _shape.Radius);
        
        Implementation.ApplyProperties(_shape, polygon);
        
        Renderer.Window.Draw(_shape);
    }
}