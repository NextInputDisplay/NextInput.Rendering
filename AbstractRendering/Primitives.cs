namespace AbstractRendering;

public class Color
{
    public float H,S,V;
    public float A = 1f;

    public Color(float v, float a = 1f)
    {
        H = 0f;
        S = 0f;
        V = v;
        A = a;
    }

    public Color (float h, float s, float v, float a = 1f)
    {
        H = h;
        S = s;
        V = v;
        A = a;
    }

    // (https://stackoverflow.com/a/6930407)
    public void SetRgb(float r, float g, float b)
    {
        float min, max, delta;
        min = r < g ? r : g;
        min = min < b ? min : b;

        max = r > g ? r : g;
        max = max > b ? max : b;

        V = max;
        delta = max - min;
        if (delta < 0.00001f)
        {
            S = 0f;
            H = 0f;
            return;
        }

        if (max > 0f)
        {
            S = delta / max;
        }
        else
        {
            S = 0f;
            H = 0f;
            return;
        }

        if (r >= max) H = (g - b) / delta;
        else
        {
            if (g >= max) H = 2f + (b - r) / delta;
            else H = 4f + (r - g) / delta;
        }

        H *= 60f;

        if (H < 0f) H += 360f;
    }
    public (float,float,float) GetRgb()
    {
        float hh, p, q, t, ff;
        int i;

        if (S <= 0f)
        {
            return (V,V,V);
        }

        hh = H%360f;
        hh /= 60f;
        i = (int)hh;
        ff = hh - i;
        p = V * (1f - S);
        q = V * (1f - (S * ff));
        t = V * (1f - (S * (1f - ff)));

        return i switch
        {
            0 => (V, t, p),
            1 => (q, V, p),
            2 => (p, V, t),
            3 => (p, q, V),
            4 => (t, p, V),
            _ => (V, p, q)
        };
    }

    public static implicit operator Color((float h, float s, float v, float a) c) => new(c.h, c.s, c.v, c.a);
    public static implicit operator (float,float,float,float)(Color c) => (c.H, c.S, c.V, c.A);

    public static Color Black  => new(0f);
    public static Color DarkGrey => new (0.2f);
    public static Color Red => new (0f, 1f, 1f);
    public static Color Green => new (120f, 1f, 1f);
    public static Color Blue => new (240f, 1f, 1f);
    public static Color White => new (1f);
}

public class Vec2
{
    public float X, Y;
    public Vec2(float x, float y) { X = x; Y = y; }

    public override string ToString() => "{" + X + "," + Y + "}";

    public static implicit operator Vec2((float,float) vec) => new (vec.Item1, vec.Item2);
    public static implicit operator (float,float)(Vec2 vec) => (vec.X, vec.Y);

    public static Vec2 operator -(Vec2 a, Vec2 b) => new (a.X - b.X, a.Y - b.Y);
    public static Vec2 operator +(Vec2 a, Vec2 b) => new (a.X + b.X, a.Y + b.Y);
    public static Vec2 operator /(Vec2 a, float b) => new (a.X / b, a.Y / b);
    public static Vec2 operator *(Vec2 a, float b) => new (a.X * b, a.Y * b);

    public float Length => MathF.Sqrt(X * X + Y * Y);

    public Vec2 Normalized => this / Length;


}