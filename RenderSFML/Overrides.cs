using AbstractRendering;
using SFML.Graphics;
using SFML.System;
using ConvexShape = AbstractRendering.ConvexShape;
using Drawable = AbstractRendering.Drawable;
using Shape = SFML.Graphics.Shape;

namespace RenderSFML;


public static class Implementation
{
    public static void Init()
    {
        Circle.Implementation = new RenderCircle();
        Rectangle.Implementation = new RenderRect();
        ConvexShape.Implementation = new RenderConvexShape();
        Polygon.Implementation = new RenderPolygon();
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
        
        Implementation.ApplyProperties(_shape, polygon.Properties);
        
        Renderer.Window.Draw(_shape);
    }
}