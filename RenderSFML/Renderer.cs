using AbstractRendering;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace RenderSFML;

public static class Renderer
{
    public static RenderWindow Window;
    public static Texture[] Textures;
    public static Font[] Fonts;
    public static Scene Scene;
    
    public static void LoadTextures(params string[] filenames)
    {
        Textures = new Texture[filenames.Length];
        for (int i = 0; i < filenames.Length; i++)
        {
            Textures[i] = new Texture(filenames[i]);
        }
    }

    public static void LoadFonts(params string[] filenames)
    {
        Fonts = new Font[filenames.Length];
        for (int i = 0; i < filenames.Length; i++)
        {
            Fonts[i] = new Font(filenames[i]);
        }
    }
    
    public static void Init(Scene scene, WindowProperties options)
    {
        Scene = scene;
        var mode = new VideoMode((uint)options.Size.X, (uint)options.Size.Y);
        var settings = new ContextSettings{ AntialiasingLevel = 8 };
        Window = new RenderWindow(mode, options.Title, Styles.Default, settings);
        
        Window.KeyPressed += (_, e) =>
        {
            if (e.Code == Keyboard.Key.Escape) Window.Close();
        };

        Window.Closed += (_,_) => { Window.Close(); };

        Implementation.Init();
    }
    
    public static void Update()
    {
        // Process events
        Window.DispatchEvents();
        
        Window.Clear(SFML.Graphics.Color.Black);

        foreach (var drawable in Scene.ToRender)
        {
            drawable.Draw();
        }

        // Finally, display the rendered frame on screen
        Window.Display();
    }
}