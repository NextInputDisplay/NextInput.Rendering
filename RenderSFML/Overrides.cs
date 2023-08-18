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
    
    public static void ApplyProperties(Shape shape, ShapeProperties properties)
    {
        if (properties.TextureId == -1)
        {
            shape.FillColor = Convert.Color(properties.Color);
        }
        else
        {
            shape.Texture = Renderer.Textures[properties.TextureId];
            //shape.TextureRect = new IntRect(0, 0, 303, 188);
        }
        
        shape.OutlineColor = Convert.Color(properties.OutlineColor);
        shape.OutlineThickness = properties.OutlineWidth;
        shape.Rotation = properties.Rotation;
    }

}

public class RenderLine : RenderImplementation
{
    public override void Draw(Drawable drawable)
    {
        Line line = (Line)drawable;

        float radius = line.Width / 2f;
        CircleShape circle = new CircleShape(radius);
        circle.FillColor = Convert.Color(line.Color);

        Vec2 radiusOffset = new Vec2(radius, radius);
        
        circle.Position = Convert.Vec2(line.Start - radiusOffset);
        Renderer.Window.Draw(circle);
        circle.Position = Convert.Vec2(line.End - radiusOffset);
        Renderer.Window.Draw(circle);

        Vec2 difference = line.End - line.Start;
        Vec2 offset = new Vec2(difference.Y, -difference.X).Normalized * radius;
        
        var convexShape = new SFML.Graphics.ConvexShape();
        convexShape.SetPointCount(4);
        convexShape.SetPoint(0,Convert.Vec2(line.Start+offset));
        convexShape.SetPoint(1,Convert.Vec2(line.Start-offset));
        convexShape.SetPoint(2,Convert.Vec2(line.End-offset));
        convexShape.SetPoint(3,Convert.Vec2(line.End+offset));
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
        sfmlText.OutlineThickness = text.OutlineWidth;
        sfmlText.OutlineColor = Convert.Color(text.OutlineColor);
        sfmlText.FillColor = Convert.Color(text.Color);
        sfmlText.CharacterSize = (uint)text.CharacterSize;
        sfmlText.Position = Convert.Vec2(text.Position);

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
    private static CircleShape _shape = new();
    public override void Draw(Drawable drawable)
    {
        Circle circle = (Circle)drawable;

        _shape.Position = Convert.Vec2(circle.Pos);
        _shape.Radius = circle.Radius;

        _shape.Origin = new Vector2f(circle.Radius, circle.Radius);

        Implementation.ApplyProperties(_shape, circle.Properties);
        
        Renderer.Window.Draw(_shape);
    }
}

public class RenderRect : RenderImplementation
{
    private static RectangleShape _shape = new();
    public override void Draw(Drawable drawable)
    {
        Rectangle rect = (Rectangle)drawable;

        _shape.Position = Convert.Vec2(rect.P1);
        _shape.Size = Convert.Vec2(rect.P2-rect.P1);
        
        Implementation.ApplyProperties(_shape, rect.Properties);
        
        Renderer.Window.Draw(_shape);
    }
}

public class RenderConvexShape : RenderImplementation
{
    private static SFML.Graphics.ConvexShape _sfmlShape = new();
    public override void Draw(Drawable drawable)
    {
        ConvexShape shape = (ConvexShape)drawable;
        
        _sfmlShape.SetPointCount((uint)shape.Verts.Length);

        for (uint i = 0; i < shape.Verts.Length; i++)
        {
            _sfmlShape.SetPoint(i,Convert.Vec2(shape.Verts[i]));
        }
        
        Implementation.ApplyProperties(_sfmlShape, shape.Properties);
        
        Renderer.Window.Draw(_sfmlShape);
    }
}

public class RenderPolygon : RenderImplementation
{
    private static CircleShape _shape = new();
    public override void Draw(Drawable drawable)
    {
        Polygon polygon = (Polygon)drawable;

        _shape = new CircleShape(polygon.Radius, (uint)polygon.NumSides);
        _shape.Position = Convert.Vec2(polygon.Pos);
        
        _shape.Origin = new Vector2f(polygon.Radius, polygon.Radius);
        
        Implementation.ApplyProperties(_shape, polygon.Properties);
        
        Renderer.Window.Draw(_shape);
    }
}