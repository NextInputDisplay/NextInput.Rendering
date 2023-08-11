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
        
        Renderer.LoadTextures("test.png");
        
        Scene scene = new Scene();
        
        scene.Add(new Circle((100,100),100),
            new ShapeProperties {TextureId = 0, OutlineColor = Color.White, OutlineWidth = 2}
        );
        scene.Add(new Rectangle((400,200),(450,300)),
            new ShapeProperties {Color = Color.Green}
        );
        scene.Add(new ConvexShape(new Vec2[]{(300,250),(250,300),(350,300)}),
            new ShapeProperties {Color = Color.Blue}
        );
        scene.Add(new Polygon((300,300),50,8),
            new ShapeProperties {Color = Color.DarkGrey, OutlineColor = Color.White, OutlineWidth = 2f}
        );

        while (Renderer.Window.IsOpen)
        {
            Renderer.Update(scene);
        }

    }
}
