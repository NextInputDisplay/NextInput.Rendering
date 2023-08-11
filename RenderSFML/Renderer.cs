using AbstractRendering;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using Drawable = AbstractRendering.Drawable;

namespace RenderSFML;

public static class Convert
{
    public static SFML.Graphics.Color Color(AbstractRendering.Color color)
    {
        byte r = (byte)(color.R * 255f);
        byte g = (byte)(color.G * 255f);
        byte b = (byte)(color.B * 255f);
        byte a = (byte)(color.A * 255f);
        
        return new SFML.Graphics.Color(r,g,b,a);
    }
}

public class RenderCircle : RenderImplementation
{
    private static CircleShape _shape = new();
    public override void Draw(Drawable drawable)
    {
        Circle circle = (Circle)drawable;

        _shape.Position = new Vector2f(circle.X, circle.Y);
        _shape.Radius = circle.Radius;
        _shape.FillColor = Convert.Color(circle.Color);
        
        Renderer.Window.Draw(_shape);
    }
}

public class RenderRect : RenderImplementation
{
    private static RectangleShape _shape = new();
    public override void Draw(Drawable drawable)
    {
        Rectangle rect = (Rectangle)drawable;

        _shape.Position = new Vector2f(rect.X1, rect.Y1);
        _shape.Size = new Vector2f(rect.X2-rect.X1, rect.Y2-rect.Y1);
        _shape.FillColor = Convert.Color(rect.Color);
        
        Renderer.Window.Draw(_shape);
    }
}



public static class Implementation
{
    public static void Init()
    {
        Circle.Implementation = new RenderCircle();
        Rectangle.Implementation = new RenderRect();
    }
}


public static class Renderer
{
    public static RenderWindow Window;
    
    public static void Init(WindowProperties options)
    {
        var mode = new VideoMode((uint)options.Size.X, (uint)options.Size.Y);
        var settings = new ContextSettings{ AntialiasingLevel = 8 };
        Window = new RenderWindow(mode, options.Title, Styles.Default, settings);
        
        Window.KeyPressed += (_, e) =>
        {
            if (e.Code == Keyboard.Key.Escape) Window.Close();
        };

        Window.Closed += (_,_) => { Window.Close(); };

        Implementation.Init();
    }

    public static void Update(Scene scene)
    {
        // Process events
        Window.DispatchEvents();
        
        Window.Clear(SFML.Graphics.Color.Black);

        foreach (var drawable in scene.ToRender)
        {
            drawable.Draw();
        }


        // Finally, display the rendered frame on screen
        Window.Display();
    }
}