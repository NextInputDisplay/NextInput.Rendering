﻿using AbstractRendering;
using SFML.Graphics;
using SFML.Window;

namespace RenderSFML;

public static class Renderer
{
    public static RenderWindow Window;
    
    public static void Init(WindowProperties options)
    {
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

    public static void Update(Scene scene)
    {
        // Process events
        Window.DispatchEvents();
        
        Window.Clear(SFML.Graphics.Color.Black);

        foreach (var drawable in scene.ToRender)
        {
            drawable.Draw();
        }


        // Finally, display the rendered frame on screen
        Window.Display();
    }
}