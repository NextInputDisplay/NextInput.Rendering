using System.Text.Json;
using AbstractRendering;
using InputSFML;
using RenderSFML;

namespace Run;

public class ReloadingFile
{
    private const string ReloadedFileNamePrefix = "reloading";
    
    private const string ReloadedFileNameExtension = "json";
    
    private const string ReloadedFileName = ReloadedFileNamePrefix + "." + ReloadedFileNameExtension;

    private static bool _shouldReload = false;
    
    public static void Run()
    {
        if (!File.Exists(ReloadedFileName))
            return;
        
        SceneJsonSerializer.InputFunctions = Input.InputFunctions;
        SceneMapper.InputFunctions = Input.InputFunctions;
        
        JsonSerializerOptions? options = new JsonSerializerOptions(JsonSerializerOptions.Default);
        options.WriteIndented = true;
        options.Converters.Add(new SceneJsonSerializer());

        FileSystemWatcher watcher = new FileSystemWatcher(AppDomain.CurrentDomain.BaseDirectory);
        
        watcher.EnableRaisingEvents = true;
        watcher.Filter = "*." + ReloadedFileNameExtension;
        watcher.Created += (sender, e) =>
        {
            if (e.Name == ReloadedFileName)
            {
                Console.WriteLine("Reloading");
                _shouldReload = true;
            }
        };
        watcher.Renamed += (sender, e) =>
        {
            if (e.Name == ReloadedFileName)
            {
                Console.WriteLine("Reloading");
                _shouldReload = true;
            }
        };
        watcher.Changed += (_, e) =>
        {
            if (e.Name == ReloadedFileName)
            {
                Console.WriteLine("Reloading");
                _shouldReload = true;
            }
        };
        
        while (true)
        {
            Scene scene = JsonSerializer.Deserialize<Scene>(File.ReadAllText(ReloadedFileName), options)!;
            
            WindowProperties properties = new WindowProperties()
            {
                Size = new Vec2(1000, 1000),
                Title = "Tester"
            };
            
            Renderer.Init(scene!,properties);
        
            Input.Init();
        
            while (Renderer.Window.IsOpen)
            {
                if (_shouldReload)
                    break;
                
                Input.Update();
            
                scene!.Animator.Update();

                Renderer.Update();
            }

            if (!Renderer.Window.IsOpen)
                Environment.Exit(0);
            
            Renderer.Window.Close();
            _shouldReload = false;
        }
    }
}