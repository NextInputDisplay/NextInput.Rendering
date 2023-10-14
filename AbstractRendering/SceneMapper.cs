using AbstractRendering.Protos;
using Google.Protobuf.Collections;
using SceneProto = AbstractRendering.Protos.Scene;

namespace AbstractRendering;

public static class SceneMapper
{
    // FIXME: Don't use a field for this
    public static Dictionary<string, Animator.ValueFunction> InputFunctions;
    
    public static SceneProto Map(this Scene scene)
    {
        SceneProto proto = new SceneProto();

        foreach (Drawable drawable in scene.ToRender)
            proto.Shapes.Add(drawable.Map());
        
        proto.Values.AddRange(scene.Values);

        foreach (var h in scene.Animator.Handlers)
            proto.Animations.Add(h.Map());

        proto.Assets = new Assets();

        foreach (string font in scene.Fonts)
            proto.Assets.Fonts.Add(font);
        
        foreach (string texture in scene.Textures)
            proto.Assets.Textures.Add(texture);

        return proto;
    }

    public static Scene Map(this SceneProto proto)
    {
        Scene scene = new Scene(InputFunctions);

        foreach (Protos.Shape shape in proto.Shapes)
        {
            scene.Add(shape.Type switch
            {
                ShapeType.Line => shape.Line.Map(),
                ShapeType.Text => shape.Text.Map(),
                ShapeType.Convex => shape.Convex.Map(),
                ShapeType.Circle => shape.Circle.Map(),
                ShapeType.Rectangle => shape.Rectangle.Map(),
                ShapeType.Polygon => shape.Polygon.Map(),
                _ => throw new ArgumentOutOfRangeException()
            });
        }

        scene.Values = proto.Values.ToArray();

        // TODO: Extract some of this to methods
        foreach (Animation animation in proto.Animations)
        {
            List<KeyFrameHandler> keyFrameHandlers = new List<KeyFrameHandler>();
            
            foreach (Protos.KeyFrame keyFrame in animation.KeyFrames)
            {
                KeyFrameHandler handler = new KeyFrameHandler(keyFrame.PropertyId);
                
                foreach (KeyFrameData keyFrameData in keyFrame.KeyFrameData)
                    handler.Add(keyFrameData.Time, keyFrameData.Value);
                
                keyFrameHandlers.Add(handler);
            }
            
            scene.Animator.Add(animation.FunctionName, keyFrameHandlers.ToArray());
        }
        
        scene.SetTextures(proto.Assets.Textures.ToArray());
        scene.SetFonts(proto.Assets.Fonts.ToArray());

        return scene;
    }

    public static Line Map(this LineShape lineShape) => new Line();
    
    public static Circle Map(this CircleShape circleShape) => new Circle { TextureId = circleShape.TextureId };

    public static Polygon Map(this PolygonShape polygonShape) => new Polygon { TextureId = polygonShape.TextureId };

    public static Rectangle Map(this RectangleShape rectangleShape) =>
        new Rectangle { TextureId = rectangleShape.TextureId };

    public static Text Map(this TextShape rectangleShape) =>
        new Text(rectangleShape.Message, rectangleShape.FontId, rectangleShape.Centered);
    
    public static ConvexShape Map(this Protos.ConvexShape convexShape) =>
        new ConvexShape(convexShape.NumVerts) { TextureId = convexShape.TextureId };

    public static Animation Map(this Handler handler)
    {
        Animation animation = new Animation()
        {
            FunctionName = handler.FunctionName
        };

        foreach (KeyFrameHandler kf in handler.KeyFrameHandlers)
            animation.KeyFrames.Add(kf.Map());

        return animation;
    }

    public static Protos.KeyFrame Map(this KeyFrameHandler keyFrameHandler)
    {
        Protos.KeyFrame keyFrame = new Protos.KeyFrame()
        {
            PropertyId = keyFrameHandler.PropertyPointer,
        };
        
        keyFrame.KeyFrameData.AddRange(keyFrameHandler.KeyFrames.Map());

        return keyFrame;
    }

    public static KeyFrameData Map(this KeyFrame keyFrame)
    {
        return new KeyFrameData()
        {
            Time = keyFrame.Time,
            Value = keyFrame.Value
        };
    }
    
    public static RepeatedField<KeyFrameData> Map(this IEnumerable<KeyFrame> keyFrames)
    {
        RepeatedField<KeyFrameData> keyFramesData = new RepeatedField<KeyFrameData>();
        foreach (KeyFrame keyFrame in keyFrames)
            keyFramesData.Add(keyFrame.Map());

        return keyFramesData;
    }

    public static Protos.Shape Map(this Drawable drawable)
    {
        return drawable switch
        {
            Line => new Protos.Shape { Type = ShapeType.Line, Line = new LineShape() },
            ConvexShape convexShape => new Protos.Shape
            {
                Type = ShapeType.Convex,
                Convex = new Protos.ConvexShape
                {
                    NumVerts = convexShape.NumVerts,
                    TextureId = convexShape.TextureId
                }
            },
            Circle circle => new Protos.Shape
            {
                Type = ShapeType.Circle,
                Circle = new CircleShape
                {
                    TextureId = circle.TextureId
                }
            },
            Polygon polygon => new Protos.Shape
            {
                Type = ShapeType.Polygon,
                Polygon = new PolygonShape
                {
                    TextureId = polygon.TextureId
                }
            },
            Rectangle rectangle => new Protos.Shape
            {
                Type = ShapeType.Rectangle,
                Rectangle = new RectangleShape
                {
                    TextureId = rectangle.TextureId
                }
            },
            Text text => new Protos.Shape
            {
                Type = ShapeType.Text,
                Text = new TextShape
                {
                    Message = text.Message,
                    FontId = text.FontId,
                    Centered = text.Centered
                }
            },
            _ => throw new ArgumentOutOfRangeException(nameof(drawable))
        };
    }
}