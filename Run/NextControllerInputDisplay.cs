using AbstractRendering;
using InputSFML;
using RenderSFML;

namespace Run;

public static class NextControllerInputDisplay
{
    private const string AssetsFolder = "ncid-assets/";

    private enum AssetId
    {
        Base,
        ButtonA,
        ButtonB,
        ButtonX,
        ButtonY,
        ButtonLb,
        ButtonLt,
        ButtonRb,
        ButtonRt,
        ButtonStart,
        ButtonBack,
        DPadUp,
        DPadDown,
        DPadLeft,
        DPadRight,
        Stick,
        StickRight
    }
    
    private static (string Path, Vec2 Size)[] _assets =
    {
        (AssetsFolder + "base.png", (128, 128)),
        (AssetsFolder + "buttA.png", (32, 32)),
        (AssetsFolder + "buttB.png", (32, 32)),
        (AssetsFolder + "buttX.png", (32, 32)),
        (AssetsFolder + "buttY.png", (32, 32)),
        (AssetsFolder + "buttLB.png", (32, 42)),
        (AssetsFolder + "buttLT.png", (32, 42)),
        (AssetsFolder + "buttRB.png", (32, 42)),
        (AssetsFolder + "buttRT.png", (32, 42)),
        (AssetsFolder + "buttS.png", (16, 16)),
        (AssetsFolder + "buttS2.png", (16, 16)),
        (AssetsFolder + "DUp.png", (32, 62)),
        (AssetsFolder + "DDown.png", (32, 62)),
        (AssetsFolder + "DLeft.png", (62, 32)),
        (AssetsFolder + "DRight.png", (62, 32)),
        (AssetsFolder + "stick.png", (8, 8)),
        (AssetsFolder + "stickR.png", (8, 8))
    };

    private static Scene scene;
    
    public static void Run()
    {
        scene = new Scene(Input.InputFunctions);
        scene.Values = new float[1024];

        WindowProperties properties = new WindowProperties()
        {
            Size = new Vec2(335, 156),
            Title = "Next Controller Input Display"
        };

        scene.SetTextures(_assets.Select(x => x.Path).ToArray());
        
        Renderer.Init(scene, properties);
        
        Vec2 buttonAPos = (60, 110);
        Vec2 buttonBPos = (110, 60);
        Vec2 buttonXPos = (10, 60);
        Vec2 buttonYPos = (60, 10);
        Vec2 buttonStartPos = (82, 68);
        Vec2 buttonBackPos = (53, 68);
        
        Vec2 buttonLBPos = (10, 10);
        Vec2 buttonLTPos = (10, 10);
        Vec2 buttonRBPos = (110, 10);
        Vec2 buttonRTPos = (110, 10);
        
        Vec2 baseImgPos = (185, 13);
        Vec2 lineRStartPos = (baseImgPos.X + 64, baseImgPos.Y + 64);
        Vec2 lineStartPos = (baseImgPos.X + 64, baseImgPos.Y + 64);
        
        Vec2 dUpPos = (232, 12);
        Vec2 dDownPos = (232, 78);
        Vec2 dLeftPos = (185, 60);
        Vec2 dRightPos = (250, 60);
        
        Rectangle baseImg = AddImageAssetAtPosition(AssetId.Base, baseImgPos);
        
        Rectangle buttonA = AddImageAssetAtPosition(AssetId.ButtonA, buttonAPos);
        SimpleButtonKf aPressed = new SimpleButtonKf(buttonA.ColorRef + 4, 1f, 0f);
        scene.Animator.Add("ButtonA", aPressed);
        
        Rectangle buttonB = AddImageAssetAtPosition(AssetId.ButtonB, buttonBPos);
        SimpleButtonKf bPressed = new SimpleButtonKf(buttonB.ColorRef + 4, 1f, 0f);
        scene.Animator.Add("ButtonB", bPressed);
        
        Rectangle buttonX = AddImageAssetAtPosition(AssetId.ButtonX, buttonXPos);
        SimpleButtonKf xPressed = new SimpleButtonKf(buttonX.ColorRef + 4, 1f, 0f);
        scene.Animator.Add("ButtonX", xPressed);
        
        Rectangle buttonY = AddImageAssetAtPosition(AssetId.ButtonY, buttonYPos);
        SimpleButtonKf yPressed = new SimpleButtonKf(buttonY.ColorRef + 4, 1f, 0f);
        scene.Animator.Add("ButtonY", yPressed);
        
        Rectangle buttonStart = AddImageAssetAtPosition(AssetId.ButtonStart, buttonStartPos);
        SimpleButtonKf startPressed = new SimpleButtonKf(buttonStart.ColorRef + 4, 1f, 0f);
        scene.Animator.Add("ButtonStart", startPressed);
        
        Rectangle buttonBack = AddImageAssetAtPosition(AssetId.ButtonBack, buttonBackPos);
        SimpleButtonKf backPressed = new SimpleButtonKf(buttonBack.ColorRef + 4, 1f, 0f);
        scene.Animator.Add("ButtonSelect", backPressed);
        
        Rectangle buttonRB = AddImageAssetAtPosition(AssetId.ButtonRb, buttonRBPos);
        SimpleButtonKf rbPressed = new SimpleButtonKf(buttonRB.ColorRef + 4, 1f, 0f);
        scene.Animator.Add("ButtonR", rbPressed);
        
        Rectangle buttonRT = AddImageAssetAtPosition(AssetId.ButtonRt, buttonRTPos);
        SimpleButtonKf rtPressed = new SimpleButtonKf(buttonRT.ColorRef + 4, 1f, 0f);
        scene.Animator.Add("ButtonR2", rtPressed);
        
        Rectangle buttonLB = AddImageAssetAtPosition(AssetId.ButtonLb, buttonLBPos);
        SimpleButtonKf lbPressed = new SimpleButtonKf(buttonLB.ColorRef + 4, 1f, 0f);
        scene.Animator.Add("ButtonL", lbPressed);
        
        Rectangle buttonLT = AddImageAssetAtPosition(AssetId.ButtonLt, buttonLTPos);
        SimpleButtonKf ltPressed = new SimpleButtonKf(buttonLT.ColorRef + 4, 1f, 0f);
        scene.Animator.Add("ButtonL2", ltPressed);
        
        Rectangle buttonDUp = AddImageAssetAtPosition(AssetId.DPadUp, dUpPos);
        SimpleButtonKf dUpPressed = new SimpleButtonKf(buttonDUp.ColorRef + 4, 1f, 0f);
        scene.Animator.Add("DpadUp", dUpPressed);
        
        Rectangle buttonDDown = AddImageAssetAtPosition(AssetId.DPadDown, dDownPos);
        SimpleButtonKf dDownPressed = new SimpleButtonKf(buttonDDown.ColorRef + 4, 1f, 0f);
        scene.Animator.Add("DpadDown", dDownPressed);
        
        Rectangle buttonDLeft = AddImageAssetAtPosition(AssetId.DPadLeft, dLeftPos);
        SimpleButtonKf dLeftPressed = new SimpleButtonKf(buttonDLeft.ColorRef + 4, 1f, 0f);
        scene.Animator.Add("DpadLeft", dLeftPressed);
        
        Rectangle buttonDRight = AddImageAssetAtPosition(AssetId.DPadRight, dRightPos);
        SimpleButtonKf dRightPressed = new SimpleButtonKf(buttonDRight.ColorRef + 4, 1f, 0f);
        scene.Animator.Add("DpadRight", dRightPressed);
        
        Line lineR = new Line(); scene.Add(lineR);
        scene.Set2V(lineR.StartRef, lineRStartPos);
        scene.SetV(lineR.WidthRef, 1.3f);
        scene.Set4V(lineR.ColorRef, Color.Red);
        
        KeyFrameHandler rightStickMoveX = new KeyFrameHandler(lineR.EndRef);
        rightStickMoveX.Add(0f,lineRStartPos.X - 64);
        rightStickMoveX.Add(1f,lineRStartPos.X + 64);
        KeyFrameHandler rightStickMoveY = new KeyFrameHandler(lineR.EndRef+1);
        rightStickMoveY.Add(0f,lineRStartPos.Y - 64);
        rightStickMoveY.Add(1f,lineRStartPos.Y + 64);
        
        Rectangle stickR = AddImageAssetAtPosition(AssetId.StickRight, lineRStartPos);
        
        KeyFrameHandler rightStickMoveXTip = new KeyFrameHandler(stickR.P1Ref);
        rightStickMoveXTip.Add(0f,lineRStartPos.X - 64 - 4);
        rightStickMoveXTip.Add(1f,lineRStartPos.X + 64 - 4);
        KeyFrameHandler rightStickMoveYTip = new KeyFrameHandler(stickR.P1Ref + 1);
        rightStickMoveYTip.Add(0f,lineRStartPos.Y - 64 - 4);
        rightStickMoveYTip.Add(1f,lineRStartPos.Y + 64 - 4);
        
        KeyFrameHandler rightStickMoveXTip2 = new KeyFrameHandler(stickR.P2Ref);
        rightStickMoveXTip2.Add(0f,lineRStartPos.X - 64 + 4);
        rightStickMoveXTip2.Add(1f,lineRStartPos.X + 64 + 4);
        
        KeyFrameHandler rightStickMoveYTip2 = new KeyFrameHandler(stickR.P2Ref + 1);
        rightStickMoveYTip2.Add(0f,lineRStartPos.Y - 64 + 4);
        rightStickMoveYTip2.Add(1f,lineRStartPos.Y + 64 + 4);
        
        scene.Animator.Add("AdjRightStickX", rightStickMoveX, rightStickMoveXTip, rightStickMoveXTip2);
        scene.Animator.Add("AdjRightStickY", rightStickMoveY, rightStickMoveYTip, rightStickMoveYTip2);
        
        Line line = new Line(); scene.Add(line);
        scene.Set2V(line.StartRef, lineStartPos);
        scene.SetV(line.WidthRef, 2f);
        scene.Set4V(line.ColorRef, Color.Black);
        
        KeyFrameHandler leftStickMoveX = new KeyFrameHandler(line.EndRef);
        leftStickMoveX.Add(0f,lineStartPos.X - 64);
        leftStickMoveX.Add(1f,lineStartPos.X + 64);
        KeyFrameHandler leftStickMoveY = new KeyFrameHandler(line.EndRef+1);
        leftStickMoveY.Add(0f,lineStartPos.Y - 64);
        leftStickMoveY.Add(1f,lineStartPos.Y + 64);
        
        Rectangle stick = AddImageAssetAtPosition(AssetId.Stick, lineStartPos);
        
        KeyFrameHandler leftStickMoveXTip = new KeyFrameHandler(stick.P1Ref);
        leftStickMoveXTip.Add(0f,lineStartPos.X - 64 - 4);
        leftStickMoveXTip.Add(1f,lineStartPos.X + 64 - 4);
        KeyFrameHandler leftStickMoveYTip = new KeyFrameHandler(stick.P1Ref + 1);
        leftStickMoveYTip.Add(0f,lineStartPos.Y - 64 - 4);
        leftStickMoveYTip.Add(1f,lineStartPos.Y + 64 - 4);
        
        KeyFrameHandler leftStickMoveXTip2 = new KeyFrameHandler(stick.P2Ref);
        leftStickMoveXTip2.Add(0f,lineStartPos.X - 64 + 4);
        leftStickMoveXTip2.Add(1f,lineStartPos.X + 64 + 4);
        
        KeyFrameHandler leftStickMoveYTip2 = new KeyFrameHandler(stick.P2Ref + 1);
        leftStickMoveYTip2.Add(0f,lineStartPos.Y - 64 + 4);
        leftStickMoveYTip2.Add(1f,lineStartPos.Y + 64 + 4);
        
        scene.Animator.Add("AdjLeftStickX", leftStickMoveX, leftStickMoveXTip, leftStickMoveXTip2);
        scene.Animator.Add("AdjLeftStickY", leftStickMoveY, leftStickMoveYTip, leftStickMoveYTip2);
        
        Input.Init();
        
        while (Renderer.Window.IsOpen)
        {
            Input.Update();
            scene.Animator.Update();
            Renderer.Update();

            //Console.WriteLine(scene.GetV(buttonA.ColorRef + 4));
            
            Thread.Sleep(15);
        }
    }

    private static Rectangle AddImageAssetAtPosition(AssetId asset, Vec2 position)
    {
        Rectangle imgAsset = new Rectangle();
        scene.Add(imgAsset);
        
        imgAsset.TextureId = (int)asset;
        Vec2 imgSize = _assets[(int)asset].Size;
        
        float imgX = position.X;
        float imgY = position.Y;
        
        // Top left corner
        scene.Set2V(imgAsset.P1Ref, (imgX, imgY));
        // Bottom right corner
        scene.Set2V(imgAsset.P2Ref, (imgX + imgSize.X, imgY + imgSize.Y));

        return imgAsset;
    }
}