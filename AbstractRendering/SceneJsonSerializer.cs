using System.Text.Json;
using System.Text.Json.Serialization;

namespace AbstractRendering;

public class SceneJsonSerializer : JsonConverter<Scene>
{
    public static Dictionary<string, Animator.ValueFunction> InputFunctions;

    public override Scene? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        void BadJsonGuard(JsonTokenType current, JsonTokenType target)
        {
            if (current != target)
                throw new JsonException($"Expected \"{target}\" token but got \"{current}\" token");
        }
        
        void BadJsonBoolGuard(JsonTokenType current)
        {
            if (current != JsonTokenType.False && current != JsonTokenType.True)
                throw new JsonException($"Expected \"true\" or \"false\" token but got \"{current}\" token");
        }

        void BadJsonPropertyNameGuard(string current, string target)
        {
            if (current != target)
                throw new JsonException($"Expected \"{target}\" property but got \"{current}\" property");
        }

        Scene scene = new Scene(InputFunctions);
        
        BadJsonGuard(reader.TokenType, JsonTokenType.StartObject);
        reader.Read();
        BadJsonGuard(reader.TokenType, JsonTokenType.PropertyName);

        string? shapesPropertyName = reader.GetString();
        BadJsonPropertyNameGuard(shapesPropertyName!, "shapes");

        reader.Read();
        BadJsonGuard(reader.TokenType, JsonTokenType.StartArray);
        reader.Read();

        while (reader.TokenType != JsonTokenType.EndArray)
        {
            BadJsonGuard(reader.TokenType, JsonTokenType.StartObject);
            reader.Read();
            BadJsonGuard(reader.TokenType, JsonTokenType.PropertyName);
            
            string? typePropertyName = reader.GetString();
            BadJsonPropertyNameGuard(typePropertyName!, "type");
            
            reader.Read();
            BadJsonGuard(reader.TokenType, JsonTokenType.String);
            
            string? shapeType = reader.GetString();

            reader.Read();
            BadJsonGuard(reader.TokenType, JsonTokenType.PropertyName);
            
            string? shapePropertyName = reader.GetString();
            BadJsonPropertyNameGuard(shapePropertyName!, "shape");
            
            reader.Read();
            BadJsonGuard(reader.TokenType, JsonTokenType.StartObject);

            switch (shapeType)
            {
                case "line":
                    Line pooLine = new Line();
                    scene.Add(pooLine);
                    break;
                case "circle":
                    reader.Read();
                    BadJsonGuard(reader.TokenType, JsonTokenType.PropertyName);
                    
                    string? textureId = reader.GetString();
                    BadJsonPropertyNameGuard(textureId!, "textureId");
                    
                    reader.Read();
                    BadJsonGuard(reader.TokenType, JsonTokenType.Number);
            
                    int texId = reader.GetInt32();
                    
                    Circle pooCircle = new Circle();
                    pooCircle.TextureId = texId;
                    scene.Add(pooCircle);
                    break;
                case "polygon":
                    reader.Read();
                    BadJsonGuard(reader.TokenType, JsonTokenType.PropertyName);
                    
                    string? textureIdP = reader.GetString();
                    BadJsonPropertyNameGuard(textureIdP!, "textureId");
                    
                    reader.Read();
                    BadJsonGuard(reader.TokenType, JsonTokenType.Number);
            
                    int texIdP = reader.GetInt32();
                    Polygon polygon = new Polygon();
                    polygon.TextureId = texIdP;
                    scene.Add(polygon);
                    break;
                case "rectangle":
                    reader.Read();
                    BadJsonGuard(reader.TokenType, JsonTokenType.PropertyName);
                    
                    string? textureIdR = reader.GetString();
                    BadJsonPropertyNameGuard(textureIdR!, "textureId");
                    
                    reader.Read();
                    BadJsonGuard(reader.TokenType, JsonTokenType.Number);
            
                    int texIdR = reader.GetInt32();
                    Rectangle rectangle = new Rectangle();
                    rectangle.TextureId = texIdR;
                    scene.Add(rectangle);
                    break;
                
                case "text":
                    reader.Read();
                    BadJsonGuard(reader.TokenType, JsonTokenType.PropertyName);
                    
                    string? messageProperty = reader.GetString();
                    BadJsonPropertyNameGuard(messageProperty!, "message");
                    
                    reader.Read();
                    BadJsonGuard(reader.TokenType, JsonTokenType.String);
            
                    string? textMessage = reader.GetString();
                    
                    reader.Read();
                    BadJsonGuard(reader.TokenType, JsonTokenType.PropertyName);
                    
                    string? fontIdProperty = reader.GetString();
                    BadJsonPropertyNameGuard(fontIdProperty!, "fontId");
                    
                    reader.Read();
                    BadJsonGuard(reader.TokenType, JsonTokenType.Number);
            
                    int fontId = reader.GetInt32();
                    
                    reader.Read();
                    BadJsonGuard(reader.TokenType, JsonTokenType.PropertyName);
                    
                    string? centeredProperty = reader.GetString();
                    BadJsonPropertyNameGuard(centeredProperty!, "centered");
                    
                    reader.Read();
                    BadJsonBoolGuard(reader.TokenType);
                    bool centered = reader.GetBoolean();

                    var pooText = new Text(textMessage, fontId, centered);
                    scene.Add(pooText);
                    break;
                
                case "convex":
                    reader.Read();
                    BadJsonGuard(reader.TokenType, JsonTokenType.PropertyName);
                    
                    string? numVertsProperty = reader.GetString();
                    BadJsonPropertyNameGuard(numVertsProperty!, "numVerts");
                    
                    reader.Read();
                    BadJsonGuard(reader.TokenType, JsonTokenType.Number);
            
                    int numVerts = reader.GetInt32();
                    
                    reader.Read();
                    BadJsonGuard(reader.TokenType, JsonTokenType.PropertyName);
                    
                    string? textureIdC = reader.GetString();
                    BadJsonPropertyNameGuard(textureIdC!, "textureId");
                    
                    reader.Read();
                    BadJsonGuard(reader.TokenType, JsonTokenType.Number);
            
                    int texIdC = reader.GetInt32();
                    ConvexShape convexShape = new ConvexShape(numVerts);
                    convexShape.TextureId = texIdC;
                    scene.Add(convexShape);
                    break;
            }
            
            reader.Read();
            BadJsonGuard(reader.TokenType, JsonTokenType.EndObject);
            
            reader.Read();
            BadJsonGuard(reader.TokenType, JsonTokenType.EndObject);

            reader.Read();
        }
        
        reader.Read();
        BadJsonGuard(reader.TokenType, JsonTokenType.PropertyName);
        
        string? valuesPropertyName = reader.GetString();
        BadJsonPropertyNameGuard(valuesPropertyName!, "values");

        reader.Read();
        BadJsonGuard(reader.TokenType, JsonTokenType.StartArray);
        reader.Read();

        List<float> values = new List<float>();

        while (reader.TokenType != JsonTokenType.EndArray)
        {
            BadJsonGuard(reader.TokenType, JsonTokenType.Number);

            float value = (float)reader.GetDouble();
            values.Add(value);
            
            reader.Read();
        }

        scene.Values = values.ToArray();
        
        reader.Read();
        BadJsonGuard(reader.TokenType, JsonTokenType.PropertyName);
        
        string? animationsPropertyName = reader.GetString();
        BadJsonPropertyNameGuard(animationsPropertyName!, "animations");
        
        reader.Read();
        BadJsonGuard(reader.TokenType, JsonTokenType.StartArray);

        reader.Read();

        while (reader.TokenType != JsonTokenType.EndArray)
        {
            BadJsonGuard(reader.TokenType, JsonTokenType.StartObject);
            reader.Read();
            
            BadJsonGuard(reader.TokenType, JsonTokenType.PropertyName);
            
            string? functionNameProperty = reader.GetString();
            BadJsonPropertyNameGuard(functionNameProperty!, "functionName");
            
            reader.Read();
            BadJsonGuard(reader.TokenType, JsonTokenType.String);
            
            string? functionName = reader.GetString();

            List<KeyFrameHandler> keyFrameHandlers = new List<KeyFrameHandler>();

            reader.Read();
            BadJsonGuard(reader.TokenType, JsonTokenType.PropertyName);
            
            string? keyFramesProperty = reader.GetString();
            BadJsonPropertyNameGuard(keyFramesProperty!, "keyFrames");
            
            reader.Read();
            BadJsonGuard(reader.TokenType, JsonTokenType.StartArray);

            reader.Read();

            while (reader.TokenType != JsonTokenType.EndArray)
            {
                BadJsonGuard(reader.TokenType, JsonTokenType.StartObject);
                reader.Read();
                
                BadJsonGuard(reader.TokenType, JsonTokenType.PropertyName);
                
                string? propIdProperty = reader.GetString();
                BadJsonPropertyNameGuard(propIdProperty!, "propertyId");

                reader.Read();
                BadJsonGuard(reader.TokenType, JsonTokenType.Number);

                int propertyId = reader.GetInt32();

                KeyFrameHandler keyFrameHandler = new KeyFrameHandler(propertyId);
                
                reader.Read();
                BadJsonGuard(reader.TokenType, JsonTokenType.PropertyName);
                
                string? keyFrameDataProperty = reader.GetString();
                BadJsonPropertyNameGuard(keyFrameDataProperty!, "keyFrameData");
                
                reader.Read();
                BadJsonGuard(reader.TokenType, JsonTokenType.StartArray);

                reader.Read();

                while (reader.TokenType != JsonTokenType.EndArray)
                {
                    BadJsonGuard(reader.TokenType, JsonTokenType.StartObject);
                    reader.Read();
                    
                    BadJsonGuard(reader.TokenType, JsonTokenType.PropertyName);

                    string? timeProperty = reader.GetString();
                    BadJsonPropertyNameGuard(timeProperty!, "time");

                    reader.Read();
                    BadJsonGuard(reader.TokenType, JsonTokenType.Number);

                    float time = (float)reader.GetDouble();
                    
                    reader.Read();
                    
                    BadJsonGuard(reader.TokenType, JsonTokenType.PropertyName);

                    string? valueProperty = reader.GetString();
                    BadJsonPropertyNameGuard(valueProperty!, "value");
                    
                    reader.Read();
                    BadJsonGuard(reader.TokenType, JsonTokenType.Number);

                    float value = (float)reader.GetDouble();

                    reader.Read();
                    BadJsonGuard(reader.TokenType, JsonTokenType.EndObject);
                    
                    reader.Read();
                    
                    keyFrameHandler.Add(time, value);
                }
                reader.Read();
                BadJsonGuard(reader.TokenType, JsonTokenType.EndObject);
                
                reader.Read();
                
                keyFrameHandlers.Add(keyFrameHandler);
                
            }
            reader.Read();
            BadJsonGuard(reader.TokenType, JsonTokenType.EndObject);
            
            reader.Read();
            
            scene.Animator.Add(functionName, keyFrameHandlers.ToArray());
        }

        reader.Read();
        BadJsonGuard(reader.TokenType, JsonTokenType.EndObject);

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
                    writer.WriteStringValue("line");
                    
                    writer.WritePropertyName("shape");
                    writer.WriteStartObject();

                    writer.WriteEndObject();
                    
                    break;
                case ConvexShape convexShape:
                    writer.WriteStringValue("convex");
                    
                    writer.WritePropertyName("shape");
                    writer.WriteStartObject();
                    
                    writer.WritePropertyName("numVerts");
                    writer.WriteNumberValue(convexShape.NumVerts);
                    writer.WritePropertyName("textureId");
                    writer.WriteNumberValue(convexShape.TextureId);
                    
                    writer.WriteEndObject();
                    break;
                case Circle circle:
                    writer.WriteStringValue("circle");
                    
                    writer.WritePropertyName("shape");
                    writer.WriteStartObject();
                    
                    writer.WritePropertyName("textureId");
                    writer.WriteNumberValue(circle.TextureId);
                    
                    writer.WriteEndObject();
                    break;
                case Polygon polygon:
                    writer.WriteStringValue("polygon");
                    
                    writer.WritePropertyName("shape");
                    writer.WriteStartObject();
                    
                    writer.WritePropertyName("textureId");
                    writer.WriteNumberValue(polygon.TextureId);
                    
                    writer.WriteEndObject();
                    break;
                case Rectangle rectangle:
                    writer.WriteStringValue("rectangle");
                    
                    writer.WritePropertyName("shape");
                    writer.WriteStartObject();
                    
                    writer.WritePropertyName("textureId");
                    writer.WriteNumberValue(rectangle.TextureId);
                    
                    writer.WriteEndObject();
                    break;
                case Text text:
                    writer.WriteStringValue("text");
                    
                    writer.WritePropertyName("shape");
                    writer.WriteStartObject();
                    
                    writer.WritePropertyName("message");
                    writer.WriteStringValue(text.Message);
                    writer.WritePropertyName("fontId");
                    writer.WriteNumberValue(text.FontId);
                    writer.WritePropertyName("centered");
                    writer.WriteBooleanValue(text.Centered);

                    writer.WriteEndObject();
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
                    writer.WriteStartObject();
                    
                    writer.WritePropertyName("time");
                    writer.WriteNumberValue(keyFrameData.Time);
                    writer.WritePropertyName("value");
                    writer.WriteNumberValue(keyFrameData.Value);
                    
                    writer.WriteEndObject();
                }
                writer.WriteEndArray();
                
                writer.WriteEndObject();
            }
            writer.WriteEndArray();
            writer.WriteEndObject();
        }
        writer.WriteEndArray();
        
        writer.WriteEndObject();
    }
}