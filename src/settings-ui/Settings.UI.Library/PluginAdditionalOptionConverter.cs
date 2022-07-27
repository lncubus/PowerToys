// Copyright (c) Microsoft Corporation
// The Microsoft Corporation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Microsoft.PowerToys.Settings.UI.Library
{
    public class PluginAdditionalOptionConverter : JsonConverter<IPluginAdditionalOption>
    {
        public override IPluginAdditionalOption Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException("Expected JSON object.");
            }

            string key = null;
            string label = null;
            string description = null;

            PluginAdditionalOptionBase result = null;

            // This is a tricky part we have to determine the type of the object we are reading with its value.
            // If value could be parsed as boolean then it is a bool option.
            // If value could be parsed as integer then it is an integer option.
            // We don't implement enum reading right now as it is handled with integer values.
            reader.Read();
            while (reader.TokenType == JsonTokenType.PropertyName)
            {
                string propertyName = reader.GetString();
                switch (propertyName)
                {
                    case "Key":
                        reader.Read();
                        key = reader.GetString();
                        break;
                    case "DisplayLabel":
                        reader.Read();
                        label = reader.GetString();
                        break;
                    case "DisplayDescription":
                        reader.Read();
                        description = reader.GetString();
                        break;
                    case "Value":
                        reader.Read();
                        switch (reader.TokenType)
                        {
                            case JsonTokenType.True:
                            case JsonTokenType.False:
                                var boolValue = reader.GetBoolean();
                                result = new PluginAdditionalOptionBool()
                                {
                                    Value = boolValue,
                                };
                                break;
                            case JsonTokenType.Number:
                                var intValue = reader.GetInt32();
                                result = new PluginAdditionalOptionInt()
                                {
                                    Value = intValue,
                                };
                                break;
                            default:
                                reader.Skip();
                                break;
                        }

                        break;
                    default:
                        reader.Skip();
                        break;
                }

                reader.Read();
            }

            if (result == null)
            {
                throw new JsonException("JSON object with an unsupported value.");
            }

            result.Key = key;
            result.DisplayLabel = label;
            result.DisplayDescription = description;
            return result;
        }

        public override void Write(
            Utf8JsonWriter writer,
            IPluginAdditionalOption value,
            JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, value, value.GetType(), options);
        }

        public override bool CanConvert(Type typeToConvert)
        {
            return typeToConvert == typeof(IPluginAdditionalOption);
        }
    }
}
