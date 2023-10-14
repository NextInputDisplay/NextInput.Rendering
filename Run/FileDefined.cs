using System.Text.Json;
using RenderSFML;
using AbstractRendering;
using Google.Protobuf;
using InputSFML;

using SceneProto = AbstractRendering.Protos.Scene;

namespace Run;

public class FileDefined
{
    public static void Run()
    {
        SceneJsonSerializer.InputFunctions = Input.InputFunctions;
        SceneMapper.InputFunctions = Input.InputFunctions;
        
        JsonSerializerOptions? options = new JsonSerializerOptions(JsonSerializerOptions.Default);
        options.WriteIndented = true;
        options.Converters.Add(new SceneJsonSerializer());

        Scene? scene;

        if (File.Exists("hello.bin"))
            scene = SceneProto.Parser.ParseFrom(File.ReadAllBytes("hello.bin")).Map();
        else
            scene = JsonSerializer.Deserialize<Scene>(File.ReadAllText("hello.json"), options);
        

        SceneProto proto = scene.Map();
        
        File.WriteAllBytes("hello.bin", proto.ToByteArray());

        WindowProperties properties = new WindowProperties()
        {
            Size = new Vec2(1000, 570),
            Title = "Refrag loves the balls"
        };

        Renderer.Init(scene!,properties);
        
        Input.Init();
        
        while (Renderer.Window.IsOpen)
        {
            Input.Update();
            
            scene!.Animator.Update();

            Renderer.Update();
        }

    }
}
