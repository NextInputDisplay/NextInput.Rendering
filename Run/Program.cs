using RenderSFML;
using AbstractRendering;

namespace Run;

public class Program
{
    public static void Main(string[] args)
    {
        
        WindowProperties properties = new WindowProperties()
        {
            Size = new Vec2(800, 600),
            Title = "Refrag loves the balls"
        };

        Renderer.Init(properties);
        
        
        Scene scene = new Scene();
        scene.Add(new Circle(200,200,50, Color.Red));
        scene.Add(new Rectangle(400,200,450,300, Color.Blue));

        while (Renderer.Window.IsOpen)
        {
            Renderer.Update(scene);
        }

    }
}
