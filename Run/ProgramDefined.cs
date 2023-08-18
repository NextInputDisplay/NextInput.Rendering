using System.Text.Json;
using RenderSFML;
using AbstractRendering;
using InputSFML;

namespace Run;

public class ProgramDefined
{
    public static void Run()
    {
        Scene scene = new Scene(Input.InputFunctions);
        scene.Values = new float[100];

        WindowProperties properties = new WindowProperties()
        {
            Size = new Vec2(800, 600),
            Title = "Refrag loves the balls"
        };

        Renderer.Init(scene,properties);
        
        Renderer.LoadTextures("test.png");
        Renderer.LoadFonts("AgaveNerdFont-Regular.ttf");
        

        #region Circle
        Circle lunaCircle = new Circle();scene.Add(lunaCircle);
        lunaCircle.TextureId = 0;
        scene.Set2V(lunaCircle.PosRef,(100,100));
        scene.SetV(lunaCircle.RadiusRef,100);
        scene.Set4V(lunaCircle.OutlineColorRef,Color.Red);
        
        KeyFrameHandler lunaCircleOutlineColour = new KeyFrameHandler(lunaCircle.OutlineColorRef);
        lunaCircleOutlineColour.Add(0f,0f);
        lunaCircleOutlineColour.Add(1f,360f);
        KeyFrameHandler lunaCircleOutlineWidth = new KeyFrameHandler(lunaCircle.OutlineWidthRef);
        lunaCircleOutlineWidth.Add(0f,5f);
        lunaCircleOutlineWidth.Add(1f,100f);
        KeyFrameHandler lunaCircleMovementX= new KeyFrameHandler(lunaCircle.PosRef);
        lunaCircleMovementX.Add(0f,100f);
        lunaCircleMovementX.Add(1f,500f);
        KeyFrameHandler lunaCircleRotation = new KeyFrameHandler(lunaCircle.RotationRef);
        lunaCircleRotation.Add(0f,0f);
        lunaCircleRotation.Add(1f,360f);
        
        scene.Animator.Add("RightTrigger", 
            lunaCircleOutlineColour,
            lunaCircleOutlineWidth,
            lunaCircleMovementX,
            lunaCircleRotation
        );

        #endregion
        
        #region Rect
        var rect = new Rectangle(); scene.Add(rect);
        scene.Set2V(rect.P1Ref, (400,200));
        scene.Set2V(rect.P2Ref, (450,300));
        scene.Set4V(rect.ColorRef, Color.Green);
        #endregion

        #region ConvexShape
        var convexShape = new ConvexShape(3); scene.Add(convexShape);
        scene.Set2V(convexShape.VertsRef+0,(300,250));
        scene.Set2V(convexShape.VertsRef+2,(250,300));
        scene.Set2V(convexShape.VertsRef+4,(350,300));
        scene.Set4V(convexShape.ColorRef,Color.Blue);
        #endregion

        #region Line
        Line line = new Line(); scene.Add(line);
        scene.Set2V(line.StartRef,(700,500));
        scene.SetV(line.WidthRef,5f);
        scene.Set4V(line.ColorRef,Color.White);

        var lineEndX = new KeyFrameHandler(line.EndRef);
        lineEndX.Add(0f,600f);
        lineEndX.Add(1f,800f);
        
        var lineEndY = new KeyFrameHandler(line.EndRef+1);
        lineEndY.Add(0f,400f);
        lineEndY.Add(1f,600f);
        
        scene.Animator.Add("AdjLeftStickX",lineEndX);
        scene.Animator.Add("AdjLeftStickY",lineEndY);
        #endregion
        
        #region Polygon
        var polygon = new Polygon(); scene.Add(polygon);
        
        scene.Set2V(polygon.PosRef,(350,350));
        scene.SetV(polygon.RadiusRef,50f);
        scene.Set4V(polygon.ColorRef,Color.Red);
        
        KeyFrameHandler polygonNumSides = new KeyFrameHandler(polygon.NumSidesRef);
        polygonNumSides.Add(0f,6);
        polygonNumSides.Add(1f,8);
        
        scene.Animator.Add("ButtonB", polygonNumSides);
        #endregion
        
        #region Text
        var pooText = new Text("poo",0, true); scene.Add(pooText);
        
        scene.Set2V(pooText.PosRef,(350,350));
        scene.Set4V(pooText.ColorRef,Color.White);
        scene.SetV(pooText.CharacterSizeRef,40f);

        KeyFrameHandler pooTextAlpha= new KeyFrameHandler(pooText.ColorRef+3);
        pooTextAlpha.Add(0f, 0f);
        pooTextAlpha.Add(1f, 1f);
        
        scene.Animator.Add("ButtonA", pooTextAlpha);
        #endregion
        
        Serialize(scene);
        
        while (Renderer.Window.IsOpen)
        {
            scene.Animator.Update();

            Renderer.Update();
        }
    }
    
    public static void Serialize(Scene scene)
    {
        SceneJsonSerializer.InputFunctions = Input.InputFunctions;
        
        JsonSerializerOptions options = new JsonSerializerOptions(JsonSerializerOptions.Default);
        options.WriteIndented = true;
        options.Converters.Add(new SceneJsonSerializer());
        
        File.WriteAllText("hello.json", JsonSerializer.Serialize(scene, options));
    }
}