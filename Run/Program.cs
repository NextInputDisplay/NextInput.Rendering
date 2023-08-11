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
        
        scene.Add(new Circle((200,200),50),
            new ShapeProperties {Color = Color.Red}
        );
        scene.Add(new Rectangle((400,200),(450,300)),
            new ShapeProperties {Color = Color.Green}
        );
        scene.Add(new ConvexShape(new Vec2[]{(300,250),(250,300),(350,300)}),
            new ShapeProperties {Color = Color.Blue}
        );
        scene.Add(new Polygon((300,300),50,8),
            new ShapeProperties {Color = Color.Black, OutlineColor = Color.White, OutlineWidth = 2f}
        );

        while (Renderer.Window.IsOpen)
        {
            Renderer.Update(scene);
        }

    }
}
