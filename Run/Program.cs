using System.Numerics;
using RenderSFML;
using AbstractRendering;
using InputSFML;

namespace Run;

public class Program
{
    public static void Main(string[] args)
    {
        Animator rightTrigger = new Animator(Input.GetRightTrigger);
        Animator leftStickX = new Animator(Input.GetAdjustedLeftStickX);
        Animator leftStickY = new Animator(Input.GetAdjustedLeftStickY);
        Animator buttonAnimatorB = new Animator(Input.GetButtonB);
        Animator buttonAnimatorA = new Animator(Input.GetButtonA);
        
        Scene scene = new Scene();
        scene.Values = new float[40];
        
        WindowProperties properties = new WindowProperties()
        {
            Size = new Vec2(800, 600),
            Title = "Refrag loves the balls"
        };

        Renderer.Init(scene,properties);
        
        Renderer.LoadTextures("test.png");
        Renderer.LoadFonts("AgaveNerdFont-Regular.ttf");
        
        

        
        /*
        #region Circle
        Circle lunaCircle = new Circle((100,100),100);
        ShapeProperties lunaCircleProperties = new ShapeProperties { TextureId = 0, OutlineColor = Color.Red, OutlineWidth = 10 };
        scene.Add(lunaCircle,lunaCircleProperties);
        
        KeyFrameHandler lunaCircleOutlineColour = new KeyFrameHandler(ref lunaCircleProperties.OutlineColor.H);
        lunaCircleOutlineColour.Add(0f,0f);
        lunaCircleOutlineColour.Add(1f,360f);
        KeyFrameHandler lunaCircleOutlineWidth = new KeyFrameHandler(ref lunaCircleProperties.OutlineWidth);
        lunaCircleOutlineWidth.Add(0f,5f);
        lunaCircleOutlineWidth.Add(1f,100f);
        KeyFrameHandler lunaCircleMovementX= new KeyFrameHandler(ref lunaCircle.Pos.X);
        lunaCircleMovementX.Add(0f,100f);
        lunaCircleMovementX.Add(1f,500f);
        KeyFrameHandler lunaCircleRotation = new KeyFrameHandler(ref lunaCircleProperties.Rotation);
        lunaCircleRotation.Add(0f,0f);
        lunaCircleRotation.Add(1f,360f);
        
        rightTrigger.Add(lunaCircleOutlineColour);
        rightTrigger.Add(lunaCircleOutlineWidth);
        rightTrigger.Add(lunaCircleMovementX);
        rightTrigger.Add(lunaCircleRotation);
        #endregion
        
        scene.Add(new Rectangle((400,200),(450,300)),
            new ShapeProperties {Color = Color.Green}
        );
        scene.Add(new ConvexShape(new Vec2[]{(300,250),(250,300),(350,300)}),
            new ShapeProperties {Color = Color.Blue}
        );
        Line line = new Line((700, 500), (700, 500), 5);
        line.Color = Color.White;

        var lineEndX = new KeyFrameHandler(ref line.End.X);
        lineEndX.Add(0f,600f);
        lineEndX.Add(1f,800f);
        leftStickX.Add(lineEndX);
        
        var lineEndY = new KeyFrameHandler(ref line.End.Y);
        lineEndY.Add(0f,400f);
        lineEndY.Add(1f,600f);
        leftStickY.Add(lineEndY);
        
        scene.Add(line);
        */
        
        var polygon = new Polygon(); scene.Add(polygon);
        
        scene.Set2V(polygon.PosRef,(350,350));
        scene.SetV(polygon.RadiusRef,50f);
        scene.Set4V(polygon.ColorRef,Color.Red);
        
        KeyFrameHandler polygonNumSides = new KeyFrameHandler(polygon.NumSidesRef);
        polygonNumSides.Add(0f,6);
        polygonNumSides.Add(1f,8);
        
        
        
        var pooText = new Text("poo",0, true); scene.Add(pooText);
        
        scene.Set2V(pooText.PosRef,(350,350));
        scene.Set4V(pooText.ColorRef,Color.White);
        scene.SetV(pooText.CharacterSizeRef,40f);

        KeyFrameHandler pooTextAlpha= new KeyFrameHandler(pooText.ColorRef+3);
        pooTextAlpha.Add(0f, 0f);
        pooTextAlpha.Add(1f, 1f);
        
        
        
        buttonAnimatorB.Add(polygonNumSides);
        buttonAnimatorA.Add(pooTextAlpha);
   

        while (Renderer.Window.IsOpen)
        {
            buttonAnimatorA.Update();
            buttonAnimatorB.Update();

            Renderer.Update();
        }

    }
}
