using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;
using RenderSFML;
using AbstractRendering;
using InputSFML;

namespace Run;

public class Program
{
    public static void Main(string[] args)
    {
        SceneJsonSerializer.InputFunctions = Input.InputFunctions;
        
        JsonSerializerOptions options = new JsonSerializerOptions(JsonSerializerOptions.Default);
        options.WriteIndented = true;
        options.Converters.Add(new SceneJsonSerializer());
        
        Scene scene = JsonSerializer.Deserialize<Scene>(File.ReadAllText("hello.json"), options);
        //Scene scene = JsonSerializer.Deserialize<Scene>(input, options);

        WindowProperties properties = new WindowProperties()
        {
            Size = new Vec2(800, 600),
            Title = "Refrag loves the balls"
        };

        Renderer.Init(scene,properties);
        
        Renderer.LoadTextures("test.png");
        Renderer.LoadFonts("AgaveNerdFont-Regular.ttf");
        
        //File.WriteAllText("hello.json", JsonSerializer.Serialize(scene, options));
        
        while (Renderer.Window.IsOpen)
        {
            scene.Animator.Update();

            Renderer.Update();
        }

    }
}
