using AbstractRendering;
using SFML.System;

namespace RenderSFML;

public static class Convert
{
    public static SFML.Graphics.Color Color(AbstractRendering.Color color)
    {
        if (color == null) color = AbstractRendering.Color.Black;
        byte r = (byte)(color.R * 255f);
        byte g = (byte)(color.G * 255f);
        byte b = (byte)(color.B * 255f);
        byte a = (byte)(color.A * 255f);
        
        return new SFML.Graphics.Color(r,g,b,a);
    }

    public static Vector2f Vec2(Vec2 vec2) => new (vec2.X, vec2.Y);
}