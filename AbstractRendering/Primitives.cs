namespace AbstractRendering;

public class Color
{
    public float R, G, B;
    public float A = 1f;

    public Color (float r, float g, float b, float a = 1f)
    {
        R = r;
        G = g;
        B = b;
        A = a;
    }

    public static Color Black = new (0f, 0f, 0f);
    public static Color Red = new (1f, 0f, 0f);
    public static Color Green = new (0f, 1f, 0f);
    public static Color Blue = new (0f, 0f, 1f);
    public static Color White = new (1f,1f,1f);
}

public class Vec2
{
    public float X, Y;
    public Vec2(float x, float y) { X = x; Y = y; }

    public override string ToString() => "{" + X + "," + Y + "}";

    public static implicit operator Vec2((float,float) vec) => new (vec.Item1, vec.Item2);

    public static Vec2 operator -(Vec2 a, Vec2 b) => new Vec2(a.X - b.X, a.Y - b.Y);

}