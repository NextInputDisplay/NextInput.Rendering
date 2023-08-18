using System.Text.Json;
using RenderSFML;
using AbstractRendering;
using InputSFML;

namespace Run;

public class FileDefined
{
    public static void Run()
    {
        SceneJsonSerializer.InputFunctions = Input.InputFunctions;
        
        JsonSerializerOptions options = new JsonSerializerOptions(JsonSerializerOptions.Default);
        options.WriteIndented = true;
        options.Converters.Add(new SceneJsonSerializer());
        
        Scene scene = JsonSerializer.Deserialize<Scene>(File.ReadAllText("hello.json"), options);

        WindowProperties properties = new WindowProperties()
        {
            Size = new Vec2(800, 600),
            Title = "Refrag loves the balls"
        };

        Renderer.Init(scene,properties);
        
        Renderer.LoadTextures("test.png");
        Renderer.LoadFonts("AgaveNerdFont-Regular.ttf");
        
        while (Renderer.Window.IsOpen)
        {
            scene.Animator.Update();

            Renderer.Update();
        }

    }
}
