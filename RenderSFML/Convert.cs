using AbstractRendering;
using SFML.System;

namespace RenderSFML;

public static class Convert
{
    public static SFML.Graphics.Color Color(Color color)
    {
        var (cr,cg,cb) = color.GetRgb();        
        
        byte r = (byte)(cr * 255f);
        byte g = (byte)(cg * 255f);
        byte b = (byte)(cb * 255f);
        byte a = (byte)(color.A * 255f);
        
        return new SFML.Graphics.Color(r,g,b,a);
    }

    public static Vector2f Vec2(Vec2 vec2) => new (vec2.X, vec2.Y);
}