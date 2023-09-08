using System.Text.Json;
using RenderSFML;
using AbstractRendering;
using InputSFML;

namespace Run;

public class ProgramDefined
{
    public static void WiiU()
    {
        Scene scene = new Scene(Input.InputFunctions);
        scene.Values = new float[800];

        WindowProperties properties = new WindowProperties()
        {
            Size = new Vec2(1000, 570),
            Title = "Wii U GamePad"
        };
        
        scene.SetTextures("test.png");
        scene.SetFonts("AgaveNerdFont-Regular.ttf");

        Renderer.Init(scene,properties);

        Color backgroundColor = Color.Black;


        Vec2 ShoulderMin = new Vec2(6, 5);
        Vec2 ShoulderMax = new Vec2(10,8);
        
        Vec2 Shoulder2Min = new Vec2(19, 25);
        Vec2 Shoulder2Max = new Vec2(25,30);
        #region Shoulder Buttons

        float[] lButtonPoints =
        {
            34, 139,
            78, 103,
            116, 64,

            113.7f, 58,
            110.2f, 53.7f,
            108, 52,
            104, 51,

            97, 52,

            89,55,
            80, 60,
            69.1f,66.4f,
            56.4f, 75.8f,
            49.6f,82f,
            42.2f,89.7f,
            36.8f, 96.8f,
            31.8f,103.6f,
            27.8f,110,

            24.5f,118,
            23.6f,123.6f,

            24.5f,127f,
            26.8f,132.1f,
            30.1f,135.8f,
            
        };
        
        float[] l2ButtonPoints =
        {
            28.6f,97.6f,
            80,50.8f,
            
            57.7f,26.5f,
            53.7f,26.5f,
            5.8f,70,
            5.8f,74,
        };

        
        ConvexShape l2Button = new ConvexShape(l2ButtonPoints.Length/2); scene.Add(l2Button);
        scene.SetValues(l2Button.VertsRef,l2ButtonPoints);
        scene.Set4V(l2Button.OutlineColorRef, new Color(0.8f));
        scene.SetV(l2Button.OutlineWidthRef,2f);
        scene.Set4V(l2Button.ColorRef, new Color(0.8f));

        SimpleButtonKf l2ButtonXPos = new SimpleButtonKf(l2Button.PosRef, Shoulder2Min.X, Shoulder2Max.X);
        SimpleButtonKf l2ButtonYPos = new SimpleButtonKf(l2Button.PosRef+1, Shoulder2Min.Y, Shoulder2Max.Y);
        SimpleButtonKf l2ButtonColor = new SimpleButtonKf(l2Button.ColorRef + 3, 0f, 1f);
        scene.Animator.Add("ButtonL2", l2ButtonXPos, l2ButtonYPos, l2ButtonColor);
        
        
        

        ConvexShape lButton = new ConvexShape(lButtonPoints.Length/2); scene.Add(lButton);
        scene.SetValues(lButton.VertsRef,lButtonPoints);
        scene.Set4V(lButton.OutlineColorRef, new Color(0.8f));
        scene.SetV(lButton.OutlineWidthRef,2f);
        scene.Set4V(lButton.ColorRef, new Color(0.8f));

        SimpleButtonKf lButtonXPos = new SimpleButtonKf(lButton.PosRef, ShoulderMin.X, ShoulderMax.X);
        SimpleButtonKf lButtonYPos = new SimpleButtonKf(lButton.PosRef+1, ShoulderMin.Y, ShoulderMax.Y);
        SimpleButtonKf lButtonColor = new SimpleButtonKf(lButton.ColorRef + 2, 0f, 0.8f);
        scene.Animator.Add("ButtonL", lButtonXPos, lButtonYPos, lButtonColor);
        
        
        
        

        float[] rButtonPoints = new float[lButtonPoints.Length];
        bool flip = true;
        for (int i = 0; i < lButtonPoints.Length; i++)
        {
            float v = lButtonPoints[i];
            if (flip) v = properties.Size.X - v;
            rButtonPoints[i] = v;
            flip = !flip;
        }
        
        float[] r2ButtonPoints = new float[l2ButtonPoints.Length];
        flip = true;
        for (int i = 0; i < l2ButtonPoints.Length; i++)
        {
            float v = l2ButtonPoints[i];
            if (flip) v = properties.Size.X - v;
            r2ButtonPoints[i] = v;
            flip = !flip;
        }
        
        
        ConvexShape r2Button = new ConvexShape(r2ButtonPoints.Length/2); scene.Add(r2Button);
        scene.SetValues(r2Button.VertsRef,r2ButtonPoints);
        scene.Set4V(r2Button.OutlineColorRef, new Color(0.8f));
        scene.SetV(r2Button.OutlineWidthRef,2f);
        scene.Set4V(r2Button.ColorRef, new Color(0.8f));

        SimpleButtonKf r2ButtonXPos = new SimpleButtonKf(r2Button.PosRef, -Shoulder2Min.X, -Shoulder2Max.X);
        SimpleButtonKf r2ButtonYPos = new SimpleButtonKf(r2Button.PosRef+1, Shoulder2Min.Y, Shoulder2Max.Y);
        SimpleButtonKf r2ButtonColor = new SimpleButtonKf(r2Button.ColorRef + 3, 0f, 1f);
        scene.Animator.Add("ButtonR2", r2ButtonXPos, r2ButtonYPos, r2ButtonColor);
        
        
        
        ConvexShape rButton = new ConvexShape(rButtonPoints.Length/2); scene.Add(rButton);
        scene.SetValues(rButton.VertsRef,rButtonPoints);
        scene.Set4V(rButton.OutlineColorRef, new Color(0.8f));
        scene.SetV(rButton.OutlineWidthRef,2f);
        scene.Set4V(rButton.ColorRef, new Color(0.8f));

        SimpleButtonKf rButtonXPos = new SimpleButtonKf(rButton.PosRef, -ShoulderMin.X, -ShoulderMax.X);
        SimpleButtonKf rButtonYPos = new SimpleButtonKf(rButton.PosRef+1, ShoulderMin.Y, ShoulderMax.Y);
        SimpleButtonKf rButtonColor = new SimpleButtonKf(rButton.ColorRef + 2, 0f, 0.8f);
        scene.Animator.Add("ButtonR", rButtonXPos, rButtonYPos, rButtonColor);
        
        

        #endregion
        

        ConvexShape controllerOutline = new ConvexShape(84); scene.Add(controllerOutline);
        #region Controller Outline
        scene.SetValues(controllerOutline.VertsRef,
            // left side
            27,187,
            27,419,
            
            // bottom left curve
            28,432,
            29,439,
            31,449,
            34,457,
            36,463,
            39,470,
            43,476,
            47,483,
            52,490,
            59,498,
            67,506,
            75,513,
            85,520,
            96,527,
            109,533,
            121,538,
            135,542,
            151,545,
            165,546,
            
            // bottom side
            180, 547,
            818, 547,
            
            // bottom right curve
            1000-165,546,
            1000-151,545,
            1000-135,542,
            1000-121,538,
            1000-109,533,
            1000-96,527,
            1000-85,520,
            1000-75,513,
            1000-67,506,
            1000-59,498,
            1000-52,490,
            1000-47,483,
            1000-43,476,
            1000-39,470,
            1000-36,463,
            1000-34,457,
            1000-31,449,
            1000-29,439,
            1000-28,432, 
            
            // right side
            973, 419,
            973, 187,
            
            // top right curve
            1000-28,601-432,
            1000-29,601-439,
            1000-31,601-449,
            1000-34,601-457,
            1000-36,601-463,
            1000-39,601-470,
            1000-43,601-476,
            1000-47,601-483,
            1000-52,601-490,
            1000-59,601-498,
            1000-67,601-506,
            1000-75,601-513,
            1000-85,601-520,
            1000-96,601-527,
            1000-109,601-533,
            1000-121,601-538,
            1000-135,601-542,
            1000-151,601-545,
            1000-165,601-546,
            
            // top side
            818, 54,
            182, 54,
            
            // top left curve
            165,601-546,
            151,601-545,
            135,601-542,
            121,601-538,
            109,601-533,
            96,601-527,
            85,601-520,
            75,601-513,
            67,601-506,
            59,601-498,
            52,601-490,
            47,601-483,
            43,601-476,
            39,601-470,
            36,601-463,
            34,601-457,
            31,601-449,
            29,601-439,
            28,601-432
        );
        #endregion
        scene.Set4V(controllerOutline.ColorRef,backgroundColor);
        scene.Set4V(controllerOutline.OutlineColorRef,new Color(0.8f));
        scene.SetV(controllerOutline.OutlineWidthRef, 2f);
        
        #region Dpad

        ConvexShape dPadOutline = new ConvexShape(12); scene.Add(dPadOutline);
        #region Dpad OutLine
        scene.SetValues(dPadOutline.VertsRef,
            // left
            139,267,
            110,267,
            110,289,
            
            // down
            139,289,
            139,318,
            161,318,
            
            // right
            161,289,
            190,289,
            190,267,
            
            // up
            161,267,
            161,238,
            139,238
            
        );
        #endregion
        scene.Set4V(dPadOutline.OutlineColorRef,new Color(0.8f));
        scene.SetV(dPadOutline.OutlineWidthRef, 2f);
        
        ConvexShape dPadLeft = new ConvexShape(5); scene.Add(dPadLeft);
        #region Dpad Left
        scene.SetValues(dPadLeft.VertsRef,
            150,278,
            137,265,
            108,265,
            108,291,
            137,291
        );
        #endregion
        scene.Set4V(dPadLeft.ColorRef,new Color(0.8f));

        SimpleButtonKf dPadLeftPressed = new SimpleButtonKf(dPadLeft.ColorRef + 3, 0f,1f);
        scene.Animator.Add("DpadLeft", dPadLeftPressed);
        
        ConvexShape dPadDown = new ConvexShape(5); scene.Add(dPadDown);
        #region Dpad Left
        scene.SetValues(dPadDown.VertsRef,
            150,278,
            137,291,
            137,320,
            163,320,
            163,291
        );
        #endregion
        scene.Set4V(dPadDown.ColorRef,new Color(0.8f));
        
        SimpleButtonKf dPadDownPressed = new SimpleButtonKf(dPadDown.ColorRef + 3, 0f,1f);
        scene.Animator.Add("DpadDown", dPadDownPressed);
        
        
        ConvexShape dPadRight = new ConvexShape(5); scene.Add(dPadRight);
        #region Dpad Left
        scene.SetValues(dPadRight.VertsRef,
            150,278,
            163,291,
            192,291,
            192,265,
            163,265
        );
        #endregion
        scene.Set4V(dPadRight.ColorRef,new Color(0.8f));
                
        SimpleButtonKf dPadRightPressed = new SimpleButtonKf(dPadRight.ColorRef + 3, 0f,1f);
        scene.Animator.Add("DpadRight", dPadRightPressed);

        ConvexShape dPadUp = new ConvexShape(5); scene.Add(dPadUp);
        #region Dpad Left
        scene.SetValues(dPadUp.VertsRef,
            150,278,
            163,265,
            163,236,
            137,236,
            137,265
        );
        #endregion
        scene.Set4V(dPadUp.ColorRef,new Color(0.8f));
                        
        SimpleButtonKf dPadUpPressed = new SimpleButtonKf(dPadUp.ColorRef + 3, 0f,1f);
        scene.Animator.Add("DpadUp", dPadUpPressed);


        Line dPadLeftLine = new Line(); scene.Add(dPadLeftLine);
        scene.Set2V(dPadLeftLine.StartRef, (117,277));
        scene.Set2V(dPadLeftLine.EndRef, (129,277));
        scene.SetV(dPadLeftLine.WidthRef, 2f);
        scene.Set4V(dPadLeftLine.ColorRef, backgroundColor);
        
        Line dPadRightLine = new Line(); scene.Add(dPadRightLine);
        scene.Set2V(dPadRightLine.StartRef, (169,277));
        scene.Set2V(dPadRightLine.EndRef, (181,277));
        scene.SetV(dPadRightLine.WidthRef, 2f);
        scene.Set4V(dPadRightLine.ColorRef, backgroundColor);
        
        Line dPadUpLine = new Line(); scene.Add(dPadUpLine);
        scene.Set2V(dPadUpLine.StartRef, (150,257));
        scene.Set2V(dPadUpLine.EndRef, (150,245));
        scene.SetV(dPadUpLine.WidthRef, 2f);
        scene.Set4V(dPadUpLine.ColorRef, backgroundColor);
        
        Line dPadDownLine = new Line(); scene.Add(dPadDownLine);
        scene.Set2V(dPadDownLine.StartRef, (150,297));
        scene.Set2V(dPadDownLine.EndRef, (150,309));
        scene.SetV(dPadDownLine.WidthRef, 2f);
        scene.Set4V(dPadDownLine.ColorRef, backgroundColor);
        
        
        
        #endregion
        
        #region Face Buttons
        Circle downButton = new Circle(); scene.Add(downButton);
        scene.Set2V(downButton.PosRef, (854.5f,314f));
        scene.SetV(downButton.RadiusRef, 16);
        scene.Set4V(downButton.ColorRef, new Color(0f));
        scene.Set4V(downButton.OutlineColorRef, new Color(0.8f));
        scene.SetV(downButton.OutlineWidthRef, 2f);
        
        SimpleButtonKf downPressed = new SimpleButtonKf(downButton.ColorRef + 2, 0f,0.8f);
        scene.Animator.Add("ButtonA", downPressed);
        
        Circle upButton = new Circle(); scene.Add(upButton);
        scene.Set2V(upButton.PosRef, (854.5f,239f));
        scene.SetV(upButton.RadiusRef, 16);
        scene.Set4V(upButton.ColorRef, new Color(0f));
        scene.Set4V(upButton.OutlineColorRef, new Color(0.8f));
        scene.SetV(upButton.OutlineWidthRef, 2f);
        
        SimpleButtonKf upPressed = new SimpleButtonKf(upButton.ColorRef + 2, 0f,0.8f);
        scene.Animator.Add("ButtonY", upPressed);
        
        Circle rightButton = new Circle(); scene.Add(rightButton);
        scene.Set2V(rightButton.PosRef, (893f,276f));
        scene.SetV(rightButton.RadiusRef, 16);
        scene.Set4V(rightButton.ColorRef, new Color(0f));
        scene.Set4V(rightButton.OutlineColorRef, new Color(0.8f));
        scene.SetV(rightButton.OutlineWidthRef, 2f);
        
        SimpleButtonKf rightPressed = new SimpleButtonKf(rightButton.ColorRef + 2, 0f,0.8f);
        scene.Animator.Add("ButtonB", rightPressed);
        
        Circle leftButton = new Circle(); scene.Add(leftButton);
        scene.Set2V(leftButton.PosRef, (816f,276f));
        scene.SetV(leftButton.RadiusRef, 16);
        scene.Set4V(leftButton.ColorRef, new Color(0f));
        scene.Set4V(leftButton.OutlineColorRef, new Color(0.8f));
        scene.SetV(leftButton.OutlineWidthRef, 2f);
        
        SimpleButtonKf leftPressed = new SimpleButtonKf(leftButton.ColorRef + 2, 0f,0.8f);
        scene.Animator.Add("ButtonX", leftPressed);
        #endregion
        
        #region Left Stick
        
        Circle leftStickOutline = new Circle(); scene.Add(leftStickOutline);
        scene.Set2V(leftStickOutline.PosRef, (109,170));
        scene.SetV(leftStickOutline.RadiusRef, 42f);
        scene.Set4V(leftStickOutline.ColorRef,new Color(0.1f));
        
        Circle leftStick = new Circle(); scene.Add(leftStick);
        scene.Set2V(leftStick.PosRef, (109,170));
        scene.SetV(leftStick.RadiusRef, 32f);
        scene.Set4V(leftStick.ColorRef,Color.Black);
        scene.Set4V(leftStick.OutlineColorRef,new Color(0.8f));
        scene.SetV(leftStick.OutlineWidthRef, 2f);
        
        Circle leftStickRing = new Circle(); scene.Add(leftStickRing);
        scene.Set2V(leftStickRing.PosRef, (109,170));
        scene.SetV(leftStickRing.RadiusRef, 21f);
        scene.Set4V(leftStickRing.OutlineColorRef,new Color(0.8f));
        scene.SetV(leftStickRing.OutlineWidthRef, 1f);

        SimpleButtonKf leftStickL3 = new SimpleButtonKf(leftStick.ColorRef+2, 0f,0.8f);
        SimpleButtonKf leftStickRingL3 = new SimpleButtonKf(leftStickRing.OutlineColorRef+2,0.8f,0f);
        scene.Animator.Add("ButtonL3", leftStickL3,leftStickRingL3);
        

        KeyFrameHandler leftStickMoveX = new KeyFrameHandler(leftStick.PosRef);
        leftStickMoveX.Add(0f,89);
        leftStickMoveX.Add(1f,129);
        KeyFrameHandler leftStickMoveY = new KeyFrameHandler(leftStick.PosRef+1);
        leftStickMoveY.Add(0f,150);
        leftStickMoveY.Add(1f,190);
        KeyFrameHandler leftStickRingMoveX = new KeyFrameHandler(leftStickRing.PosRef);
        leftStickRingMoveX.Add(0f,89);
        leftStickRingMoveX.Add(1f,129);
        KeyFrameHandler leftStickRingMoveY = new KeyFrameHandler(leftStickRing.PosRef+1);
        leftStickRingMoveY.Add(0f,150);
        leftStickRingMoveY.Add(1f,190);
        
        scene.Animator.Add("AdjLeftStickX", leftStickMoveX, leftStickRingMoveX);
        scene.Animator.Add("AdjLeftStickY", leftStickMoveY, leftStickRingMoveY);
        
        #endregion
        
        #region Right Stick
        
        Circle rightStickOutline = new Circle(); scene.Add(rightStickOutline);
        scene.Set2V(rightStickOutline.PosRef, (892,170));
        scene.SetV(rightStickOutline.RadiusRef, 42f);
        scene.Set4V(rightStickOutline.ColorRef,new Color(0.1f));
        
        Circle rightStick = new Circle(); scene.Add(rightStick);
        scene.Set2V(rightStick.PosRef, (892,170));
        scene.SetV(rightStick.RadiusRef, 32f);
        scene.Set4V(rightStick.ColorRef,Color.Black);
        scene.Set4V(rightStick.OutlineColorRef,new Color(0.8f));
        scene.SetV(rightStick.OutlineWidthRef, 2f);
        
        Circle rightStickRing = new Circle(); scene.Add(rightStickRing);
        scene.Set2V(rightStickRing.PosRef, (892,170));
        scene.SetV(rightStickRing.RadiusRef, 21f);
        scene.Set4V(rightStickRing.OutlineColorRef,new Color(0.8f));
        scene.SetV(rightStickRing.OutlineWidthRef, 1f);
        
        SimpleButtonKf rightStickL3 = new SimpleButtonKf(rightStick.ColorRef+2, 0f,0.8f);
        SimpleButtonKf rightStickRingL3 = new SimpleButtonKf(rightStickRing.OutlineColorRef+2,0.8f,0f);
        scene.Animator.Add("ButtonR3", rightStickL3,rightStickRingL3);

        KeyFrameHandler rightStickMoveX = new KeyFrameHandler(rightStick.PosRef);
        rightStickMoveX.Add(0f,872);
        rightStickMoveX.Add(1f,912);
        KeyFrameHandler rightStickMoveY = new KeyFrameHandler(rightStick.PosRef+1);
        rightStickMoveY.Add(0f,150);
        rightStickMoveY.Add(1f,190);
        KeyFrameHandler rightStickRingMoveX = new KeyFrameHandler(rightStickRing.PosRef);
        rightStickRingMoveX.Add(0f,872);
        rightStickRingMoveX.Add(1f,912);
        KeyFrameHandler rightStickRingMoveY = new KeyFrameHandler(rightStickRing.PosRef+1);
        rightStickRingMoveY.Add(0f,150);
        rightStickRingMoveY.Add(1f,190);
        
        scene.Animator.Add("AdjRightStickX", rightStickMoveX, rightStickRingMoveX);
        scene.Animator.Add("AdjRightStickY", rightStickMoveY, rightStickRingMoveY);
        
        #endregion

        #region Screen

        Rectangle screen = new Rectangle(); scene.Add(screen);
        screen.TextureId = 0;
        scene.Set4V(screen.OutlineColorRef, new Color(0.8f));
        scene.SetV(screen.OutlineWidthRef, 2f);
        scene.Set2V(screen.P1Ref, (242,158));
        scene.Set2V(screen.P2Ref, (754,449));
        

        ConvexShape screenOutline = new ConvexShape(20); scene.Add(screenOutline);
        #region Screen Outline
        scene.SetValues(screenOutline.VertsRef,
            // left
            230,155,
            230,448,
            
            // bottom left curve
            231,454,
            232.5f,456.5f,
            235,458,
            
            // bottom
            242,459,
            754,459,
            
            // bottom right curve
            762,458,
            764.5f,456.5f,
            766,454,
            
            // right
            767,452,
            767,155,
            
            766,152,
            764.5f, 149.5f,
            762,148,
            
            
            // top
            754,147,
            242,147,
            
            235,148,
            232.5f,149.5f,
            231,152
            
        );
        #endregion
        
        scene.Set4V(screenOutline.OutlineColorRef, new Color(0.4f));
        scene.SetV(screenOutline.OutlineWidthRef, 2f);
        
        #endregion
        
        #region Start / Select
        
        Circle plusButton = new Circle(); scene.Add(plusButton);
        scene.Set2V(plusButton.PosRef,(812.5f,373.5f));
        scene.SetV(plusButton.RadiusRef,11f);
        scene.Set4V(plusButton.ColorRef, new Color(0.8f));
        scene.Set4V(plusButton.OutlineColorRef, new Color(0.8f));
        scene.SetV(plusButton.OutlineWidthRef, 2f);

        SimpleButtonKf plusPressed = new SimpleButtonKf(plusButton.ColorRef + 3, 0f, 1f);
        scene.Animator.Add("ButtonStart",plusPressed);

        Text plusText = new Text("+", 0, true); scene.Add(plusText);
        scene.Set2V(plusText.PosRef, (812.5f,373.5f));
        scene.Set4V(plusText.ColorRef, Color.Black);
        scene.SetV(plusText.CharacterSizeRef, 28f);
        
        
        Circle minusButton = new Circle(); scene.Add(minusButton);
        scene.Set2V(minusButton.PosRef,(812.5f,423));
        scene.SetV(minusButton.RadiusRef,11f);
        scene.Set4V(minusButton.ColorRef, new Color(0.8f));
        scene.Set4V(minusButton.OutlineColorRef, new Color(0.8f));
        scene.SetV(minusButton.OutlineWidthRef, 2f);
        
        Text minusText = new Text("-", 0, true); scene.Add(minusText);
        scene.Set2V(minusText.PosRef, (812.5f,423));
        scene.Set4V(minusText.ColorRef, Color.Black);
        scene.SetV(minusText.CharacterSizeRef, 28f);
        
        SimpleButtonKf minusPressed = new SimpleButtonKf(minusButton.ColorRef + 3, 0f, 1f);
        scene.Animator.Add("ButtonSelect",minusPressed);
        
        #endregion
        
        
        
        Serialize(scene, "hello.json");
        
        Input.Init();
        
        while (Renderer.Window.IsOpen)
        {
            Input.Update();
            scene.Animator.Update();
            //Input.PrintButtons();
            //Input.PrintAxes();

            Renderer.Update();
            Thread.Sleep(10);
        }
    }
    
    public static void Serialize(Scene scene, string filename)
    {
        SceneJsonSerializer.InputFunctions = Input.InputFunctions;
        
        JsonSerializerOptions options = new JsonSerializerOptions(JsonSerializerOptions.Default);
        options.WriteIndented = true;
        options.Converters.Add(new SceneJsonSerializer());
        
        File.WriteAllText(filename, JsonSerializer.Serialize(scene, options));
    }
}