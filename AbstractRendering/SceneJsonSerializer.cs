using System.Text.Json;
using System.Text.Json.Serialization;

namespace AbstractRendering;

public class SceneJsonSerializer : JsonConverter<Scene>
{
    public static Dictionary<string, Animator.ValueFunction> InputFunctions;
    public override Scene? Read(ref Utf8JsonReader read, Type typeToConvert, JsonSerializerOptions options)
    {
        Scene scene = new Scene(InputFunctions);
        
        Read(ref read, JsonTokenType.StartObject);
        Property(ref read, "shapes");
        #region Shapes
        
        Read(ref read, JsonTokenType.StartArray);

        while (read.TokenType != JsonTokenType.EndArray)
        {
            Read(ref read, JsonTokenType.StartObject);

            string? shapeType = ReadString(ref read, "type");
            
            Property(ref read,"shape");
            BadJsonGuard(read.TokenType, JsonTokenType.StartObject);

            switch (shapeType)
            {
                case "line":
                    Line line = new Line();
                    scene.Add(line);
                    break;
                
                case "circle":
                    read.Read();
                    
                    Circle circle = new Circle();
                    circle.TextureId = ReadIntQuick(ref read, "textureId");
                    scene.Add(circle);
                    break;
                
                case "polygon":
                    read.Read();
                    
                    Polygon polygon = new Polygon();
                    polygon.TextureId = ReadIntQuick(ref read, "textureId");
                    scene.Add(polygon);
                    break;
                
                case "rectangle":
                    read.Read();
                    
                    Rectangle rectangle = new Rectangle();
                    rectangle.TextureId = ReadIntQuick(ref read, "textureId");
                    scene.Add(rectangle);
                    break;
                
                case "text":
                    read.Read();
                    
                    string? textMessage = ReadString(ref read, "message");
                    int fontId = ReadInt(ref read, "fontId");
                    bool centered = ReadBoolQuick(ref read, "centered");
                    
                    scene.Add(new Text(textMessage!, fontId, centered));
                    break;
                
                case "convex":
                    read.Read();

                    ConvexShape convexShape = new ConvexShape(ReadInt(ref read, "numVerts"));
                    convexShape.TextureId = ReadIntQuick(ref read, "textureId");
                    scene.Add(convexShape);
                    break;
            }
            
            read.Read();
            EndObject(ref read); EndObject(ref read);
        }
        
        read.Read();
        #endregion
        
        Property(ref read,"values");
        #region Values

        StartArray(ref read);

        List<float> values = new List<float>();

        while (read.TokenType != JsonTokenType.EndArray)
        {
            values.Add(ReadFloat(ref read));
        }

        scene.Values = values.ToArray();
        
        read.Read();
        #endregion
        
        Property(ref read,"animations");
        #region Animations

        StartArray(ref read);

        while (read.TokenType != JsonTokenType.EndArray)
        {
            StartObject(ref read);

            string? functionName = ReadString(ref read, "functionName");
            Property(ref read, "keyFrames");
            
            StartArray(ref read);
            
            List<KeyFrameHandler> keyFrameHandlers = new List<KeyFrameHandler>();
            while (read.TokenType != JsonTokenType.EndArray)
            {
                StartObject(ref read);
                
                int propertyId = ReadInt(ref read, "propertyId");
                KeyFrameHandler keyFrameHandler = new KeyFrameHandler(propertyId);
                
                Property(ref read,"keyFrameData");
                
                StartArray(ref read);

                while (read.TokenType != JsonTokenType.EndArray)
                {
                    StartObject(ref read);

                    float time = ReadFloat(ref read, "time");
                    float value = ReadFloat(ref read, "value");

                    EndObject(ref read);
                    keyFrameHandler.Add(time, value);
                }
                
                read.Read();EndObject(ref read);
                
                keyFrameHandlers.Add(keyFrameHandler);
            }
            
            read.Read();EndObject(ref read);
            
            scene.Animator.Add(functionName!, keyFrameHandlers.ToArray());
        }
        
        BadJsonGuard(read.TokenType, JsonTokenType.EndArray);
        read.Read();
        
        Property(ref read,"assets");
        StartObject(ref read);
        #region Tetures
        
        Property(ref read,"textures");
        StartArray(ref read);

        List<string> textures = new List<string>();

        while (read.TokenType != JsonTokenType.EndArray)
        {
            textures.Add(ReadString(ref read)!);
        }

        scene.SetTextures(textures.ToArray());
        
        read.Read();
        #endregion
        
        #region Tetures
        
        Property(ref read,"fonts");
        StartArray(ref read);

        List<string> fonts = new List<string>();

        while (read.TokenType != JsonTokenType.EndArray)
        {
            fonts.Add(ReadString(ref read)!);
        }

        scene.SetFonts(fonts.ToArray());
        
        read.Read();
        #endregion
        
        EndObject(ref read);
        

        read.Read();
        BadJsonGuard(read.TokenType, JsonTokenType.EndObject);
        #endregion

        return scene;
    }

    public override void Write(Utf8JsonWriter writer, Scene value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();
        
        writer.WritePropertyName("shapes");
        writer.WriteStartArray();
        foreach (Drawable drawable in value.ToRender)
        {
            writer.WriteStartObject();
            writer.WritePropertyName("type");
            
            switch (drawable)
            {
                case Line line:
                    WriteShape(writer, "line").End();
                    break;
                case ConvexShape convexShape:
                    WriteShape(writer, "convex")
                        .Add("numVerts", convexShape.NumVerts)
                        .Add("textureId", convexShape.TextureId)
                    .End();
                    break;
                case Circle circle:
                    WriteShape(writer, "circle")
                        .Add("textureId", circle.TextureId)
                    .End();
                    break;
                case Polygon polygon:
                    WriteShape(writer, "polygon")
                        .Add("textureId", polygon.TextureId)
                    .End();
                    break;
                case Rectangle rectangle:
                    WriteShape(writer, "rectangle")
                        .Add("textureId", rectangle.TextureId)
                    .End();
                    break;
                case Text text:
                    WriteShape(writer, "text")
                        .Add("message", text.Message)
                        .Add("fontId", text.FontId)
                        .Add("centered", text.Centered)
                    .End();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(drawable));
            }

            writer.WriteEndObject();
        }
        writer.WriteEndArray();
        
        writer.WritePropertyName("values");
        writer.WriteStartArray();
        foreach (var f in value.Values)
        {
            writer.WriteNumberValue(f);
        }
        writer.WriteEndArray();
        
        writer.WritePropertyName("animations");
        writer.WriteStartArray();
        foreach (var h in value.Animator.Handlers)
        {
            writer.WriteStartObject();
            writer.WritePropertyName("functionName");
            writer.WriteStringValue(h.FunctionName);
            
            writer.WritePropertyName("keyFrames");
            writer.WriteStartArray();
            foreach (KeyFrameHandler kf in h.KeyFrameHandlers)
            {
                writer.WriteStartObject();
                
                writer.WritePropertyName("propertyId");
                writer.WriteNumberValue(kf.PropertyPointer);
                
                writer.WritePropertyName("keyFrameData");
                writer.WriteStartArray();
                foreach (KeyFrame keyFrameData in kf.KeyFrames)
                {
                    WriteObj(writer)
                        .Add("time",keyFrameData.Time)
                        .Add("value",keyFrameData.Value)
                    .End();
                }
                writer.WriteEndArray();
                
                writer.WriteEndObject();
            }
            writer.WriteEndArray();
            writer.WriteEndObject();
        }
        writer.WriteEndArray();
        
        writer.WritePropertyName("assets");

        writer.WriteStartObject();
        writer.WritePropertyName("textures");
        
        writer.WriteStartArray();
        foreach (var s in value.Textures)
        {
            writer.WriteStringValue(s);
        }
        writer.WriteEndArray();
    
        writer.WritePropertyName("fonts");
        
        writer.WriteStartArray();
        foreach (var s in value.Fonts)
        {
            writer.WriteStringValue(s);
        }
        writer.WriteEndArray();
        
        
        writer.WriteEndObject();
        
        
        writer.WriteEndObject();
    }
    
    
    #region Helper Functions

    class ObjectHelper
    {
        private Utf8JsonWriter _writer;
        public ObjectHelper(Utf8JsonWriter writer)
        {
            _writer = writer;
        }

        public ObjectHelper Add(string name, string value)
        {
            _writer.WritePropertyName(name);
            _writer.WriteStringValue(value);
            return this;
        }
        
        public ObjectHelper Add(string name, int value)
        {
            _writer.WritePropertyName(name);
            _writer.WriteNumberValue(value);
            return this;
        } 
        
        public ObjectHelper Add(string name, float value)
        {
            _writer.WritePropertyName(name);
            _writer.WriteNumberValue(value);
            return this;
        } 
        
        public ObjectHelper Add(string name, bool value)
        {
            _writer.WritePropertyName(name);
            _writer.WriteBooleanValue(value);
            return this;
        }

        public void End()
        {
            _writer.WriteEndObject();
        }
    }

    private ObjectHelper WriteShape(Utf8JsonWriter writer, string type)
    {
        writer.WriteStringValue(type);
        writer.WritePropertyName("shape");
        writer.WriteStartObject();

        return new ObjectHelper(writer);
    }
    
    private ObjectHelper WriteObj(Utf8JsonWriter writer)
    {
        writer.WriteStartObject();

        return new ObjectHelper(writer);
    }
    
    
    private void BadJsonGuard(JsonTokenType current, JsonTokenType target)
    {
        if (current != target)
            throw new JsonException($"Expected \"{target}\" token but got \"{current}\" token");
    }
        
    private void BadJsonBoolGuard(JsonTokenType current)
    {
        if (current != JsonTokenType.False && current != JsonTokenType.True)
            throw new JsonException($"Expected \"true\" or \"false\" token but got \"{current}\" token");
    }
    
    private void BadJsonPropertyNameGuard(string current, string target)
    {
        if (current != target)
            throw new JsonException($"Expected \"{target}\" property but got \"{current}\" property");
    }
    
    private void Read(ref Utf8JsonReader reader, JsonTokenType expectedType)
    {
        BadJsonGuard(reader.TokenType, expectedType);
        reader.Read();
    }    
    private void Property(ref Utf8JsonReader reader, string name)
    {
        BadJsonGuard(reader.TokenType, JsonTokenType.PropertyName);
        BadJsonPropertyNameGuard(reader.GetString()!, name);
        reader.Read();
    }
    
    private string? ReadString(ref Utf8JsonReader reader)
    {
        string? str = ReadStringQuick(ref reader);
        reader.Read();
        return str;
    }

    private string? ReadString(ref Utf8JsonReader reader, string title)
    {
        string? str = ReadStringQuick(ref reader, title);
        reader.Read();
        return str;
    }
    
    private string? ReadStringQuick(ref Utf8JsonReader reader)
    {
        BadJsonGuard(reader.TokenType, JsonTokenType.String);
        return reader.GetString();
    }

    private string? ReadStringQuick(ref Utf8JsonReader reader, string title)
    {
        Property(ref reader, title);
        BadJsonGuard(reader.TokenType, JsonTokenType.String);
        return reader.GetString();
    }

    private int ReadIntQuick(ref Utf8JsonReader reader, string title)
    {
        Property(ref reader,title);
        BadJsonGuard(reader.TokenType, JsonTokenType.Number);
        return reader.GetInt32();
    }
    
    private int ReadInt(ref Utf8JsonReader reader, string title)
    {
        int val = ReadIntQuick(ref reader, title);
        reader.Read();
        return val;
    }

    private bool ReadBoolQuick(ref Utf8JsonReader reader, string title)
    {
        Property(ref reader, "centered");
        BadJsonBoolGuard(reader.TokenType);
        return reader.GetBoolean();
    }

    private void EndObject(ref Utf8JsonReader reader)
    {
        BadJsonGuard(reader.TokenType, JsonTokenType.EndObject);
        reader.Read();
    }

    private float ReadFloat(ref Utf8JsonReader reader)
    {
        BadJsonGuard(reader.TokenType, JsonTokenType.Number);
        float value = (float)reader.GetDouble();
        reader.Read();
        return value;
    }
    
    private float ReadFloat(ref Utf8JsonReader reader, string title)
    {
        Property(ref reader, title);
        BadJsonGuard(reader.TokenType, JsonTokenType.Number);
        float value = (float)reader.GetDouble();
        reader.Read();
        return value;
    }

    private void StartArray(ref Utf8JsonReader reader)
    {
        BadJsonGuard(reader.TokenType, JsonTokenType.StartArray);
        reader.Read();
    }

    private void StartObject(ref Utf8JsonReader reader)
    {
        BadJsonGuard(reader.TokenType, JsonTokenType.StartObject);
        reader.Read();
    }
    
    #endregion
}