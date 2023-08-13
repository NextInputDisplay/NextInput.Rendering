using System.Numerics;
using RenderSFML;
using AbstractRendering;
using InputSFML;

namespace Run;

public class Program
{
    public static void Main(string[] args)
    {
        Animator animator = new Animator(Input.GetRightTrigger);
        
        
        WindowProperties properties = new WindowProperties()
        {
            Size = new Vec2(800, 600),
            Title = "Refrag loves the balls"
        };

        Renderer.Init(properties);
        
        Renderer.LoadTextures("test.png");
        
        Scene scene = new Scene();
        
        Circle lunaCircle = new Circle((100,100),100);
        ShapeProperties lunaCircleProperties = new ShapeProperties { TextureId = 0, OutlineColor = Color.Red, OutlineWidth = 10 };
        scene.Add(lunaCircle,lunaCircleProperties);
        
        KeyFrameHandler lunaCircleOutlineColour = new KeyFrameHandler(ref lunaCircleProperties.OutlineColor.H);
        lunaCircleOutlineColour.Add(0f,0f);
        lunaCircleOutlineColour.Add(1f,360f);
        KeyFrameHandler lunaCircleOutlineWidth = new KeyFrameHandler(ref lunaCircleProperties.OutlineWidth);
        lunaCircleOutlineWidth.Add(0f,5f);
        lunaCircleOutlineWidth.Add(1f,100f);
        
        animator.Add(lunaCircleOutlineColour);
        animator.Add(lunaCircleOutlineWidth);
        
        scene.Add(new Rectangle((400,200),(450,300)),
            new ShapeProperties {Color = Color.Green}
        );
        scene.Add(new ConvexShape(new Vec2[]{(300,250),(250,300),(350,300)}),
            new ShapeProperties {Color = Color.Blue}
        );
        scene.Add(new Polygon((300,300),50,8),
            new ShapeProperties {Color = Color.Red}
        );

        while (Renderer.Window.IsOpen)
        {
            animator.Update();
            Renderer.Update(scene);
        }

    }
}
