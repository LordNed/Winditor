﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using WindEditor.Minitors;

namespace WindEditor.Serialization
{
    public class WDOMNodeJsonConverter : JsonConverter
    {
        private WWorld m_world;
        private WDOMNode m_parent;

        public WDOMNodeJsonConverter(WWorld world, WDOMNode parent)
        {
            m_world = world;
            m_parent = parent;
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(SerializableDOMNode) || objectType.IsSubclassOf(typeof(SerializableDOMNode));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var nodeJson = JObject.Load(reader);
            var actorName = (string)nodeJson["Name"];

            SerializableDOMNode newNode;
            if (m_parent is WDOMLayeredGroupNode)
            {
                if (actorName == null)
                    return null;

                WDOMLayeredGroupNode layerNode = m_parent as WDOMLayeredGroupNode;
                string unlayedFourCC = layerNode.FourCC.ToString();
                MapLayer layer = ChunkHeader.FourCCToLayer(ref unlayedFourCC);
                FourCC fourcc = FourCCConversion.GetEnumFromString(unlayedFourCC);

                Type newObjType = WResourceManager.GetTypeByName(actorName);
                if (newObjType == typeof(Actor))
                    return null;

                newNode = (SerializableDOMNode)Activator.CreateInstance(newObjType, fourcc, m_world);
                newNode.Layer = layer;
            }
            else if (m_parent is WDOMGroupNode)
            {
                WDOMGroupNode groupNode = m_parent as WDOMGroupNode;
                FourCC fourcc = groupNode.FourCC;

                if (fourcc == FourCC.ACTR || fourcc == FourCC.SCOB || fourcc == FourCC.TRES)
                    return null;

                if (fourcc == FourCC.TGDR || fourcc == FourCC.TGSC || fourcc == FourCC.TGOB)
                {
                    if (actorName == null)
                        return null;

                    Type newObjType = WResourceManager.GetTypeByName(actorName);
                    if (newObjType == typeof(Actor))
                        return null;

                    newNode = (SerializableDOMNode)Activator.CreateInstance(newObjType, fourcc, m_world);
                }
                else
                {
                    Type newObjType = FourCCConversion.GetTypeFromEnum(groupNode.FourCC);

                    newNode = (SerializableDOMNode)Activator.CreateInstance(newObjType, fourcc, m_world);
                }
            }
            else
            {
                return null;
            }

            newNode.SetParent(m_parent);

            try
            {
                var wproperties = newNode.GetType().GetProperties().Where(prop =>
                {
                    CustomAttributeData[] custom_attributes = prop.CustomAttributes.ToArray();
                    CustomAttributeData wproperty_attribute = custom_attributes.FirstOrDefault(x => x.AttributeType.Name == "WProperty");
                    if (wproperty_attribute == null)
                        return false;
                    CustomAttributeData jsonignore_attribute = custom_attributes.FirstOrDefault(x => x.AttributeType.Name == "JsonIgnoreAttribute");
                    if (jsonignore_attribute != null)
                        return false;
                    return true;
                });

                foreach (var prop in wproperties)
                {
                    JToken jsonValue = nodeJson[prop.Name];
                    if (jsonValue == null)
                        continue;

                    if (prop.PropertyType == typeof(WTransform))
                    {
                        if (!(jsonValue is JObject))
                        {
                            continue;
                        }
                        JObject jsonValueObject = (JObject)jsonValue;
                        WTransform transform = prop.GetValue(newNode, null) as WTransform;
                        if (transform != null)
                        {
                            if (jsonValueObject.ContainsKey("Position"))
                            {
                                var position = transform.Position;

                                position.X = ((float?)jsonValueObject["Position"]["X"]).GetValueOrDefault();
                                position.Y = ((float?)jsonValueObject["Position"]["Y"]).GetValueOrDefault();
                                position.Z = ((float?)jsonValueObject["Position"]["Z"]).GetValueOrDefault();

                                transform.Position = position;
                            }
                            if (jsonValueObject.ContainsKey("Rotation"))
                            {
                                var rotation = transform.Rotation;

                                rotation.X = ((float?)jsonValueObject["Rotation"]["X"]).GetValueOrDefault();
                                rotation.Y = ((float?)jsonValueObject["Rotation"]["Y"]).GetValueOrDefault();
                                rotation.Z = ((float?)jsonValueObject["Rotation"]["Z"]).GetValueOrDefault();
                                rotation.W = ((float?)jsonValueObject["Rotation"]["W"]).GetValueOrDefault();

                                transform.Rotation = rotation;
                            }
                            if (jsonValueObject.ContainsKey("LocalScale"))
                            {
                                var localScale = transform.LocalScale;

                                localScale.X = ((float?)jsonValueObject["LocalScale"]["X"]).GetValueOrDefault(1.0f);
                                localScale.Y = ((float?)jsonValueObject["LocalScale"]["Y"]).GetValueOrDefault(1.0f);
                                localScale.Z = ((float?)jsonValueObject["LocalScale"]["Z"]).GetValueOrDefault(1.0f);

                                transform.LocalScale = localScale;
                            }
                        }
                    }
                    else if (prop.PropertyType == typeof(MessageReference))
                    {
                        ushort messageID = (ushort)jsonValue;
                        MessageReference msgRef = new MessageReference(messageID);
                        prop.SetValue(newNode, msgRef);
                    }
                    else if (prop.PropertyType == typeof(Path_v2))
                    {
                        int pathIndex = (int)jsonValue;

                        WDOMNode cur_object = m_parent;
                        while (cur_object.Parent != null)
                        {
                            cur_object = cur_object.Parent;
                        }
                        List<Path_v2> pathsList = cur_object.GetChildrenOfType<Path_v2>();

                        if (pathIndex < 0)
                        {
                            prop.SetValue(newNode, null);
                        }
                        else if (pathIndex < pathsList.Count)
                        {
                            Path_v2 path = pathsList[pathIndex];
                            prop.SetValue(newNode, path);
                        }
                    }
                    else if (prop.PropertyType == typeof(ExitData))
                    {
                        int exitIndex = (int)jsonValue;

                        WScene scene;
                        CustomAttributeData[] custom_attributes = prop.CustomAttributes.ToArray();
                        CustomAttributeData wproperty_attribute = custom_attributes.FirstOrDefault(x => x.AttributeType.Name == "WProperty");
                        SourceScene source_scene = (SourceScene)wproperty_attribute.ConstructorArguments[4].Value;
                        if (source_scene == SourceScene.Stage)
                        {
                            scene = m_world.Map.SceneList.First(x => x.GetType() == typeof(WStage)) as WScene;
                        }
                        else
                        {
                            WDOMNode cur_object = m_parent;
                            while (cur_object.Parent != null)
                            {
                                cur_object = cur_object.Parent;
                            }
                            scene = cur_object as WScene;
                        }
                        List<ExitData> exitsList = scene.GetChildrenOfType<ExitData>();

                        if (exitIndex < 0)
                        {
                            prop.SetValue(newNode, null);
                        }
                        else if (exitIndex < exitsList.Count)
                        {
                            ExitData exit = exitsList[exitIndex];
                            prop.SetValue(newNode, exit);
                        }
                    }
                    else if (prop.PropertyType == typeof(MapEvent))
                    {
                        int eventIndex = (int)jsonValue;

                        WStage stage = m_world.Map.SceneList.First(x => x.GetType() == typeof(WStage)) as WStage;
                        List<MapEvent> eventsList = stage.GetChildrenOfType<MapEvent>();

                        if (eventIndex < 0)
                        {
                            prop.SetValue(newNode, null);
                        }
                        else if (eventIndex < eventsList.Count)
                        {
                            MapEvent evnt = eventsList[eventIndex];
                            prop.SetValue(newNode, evnt);
                        }
                    }
                    else
                    {
                        var value = Convert.ChangeType(jsonValue, prop.PropertyType);
                        if (value != null)
                        {
                            prop.SetValue(newNode, value);
                        }
                    }
                }

                newNode.PostLoad();

                return newNode;
            }
            catch (Exception)
            {
                // Creating the entity failed, so remove it from the scene.
                newNode.SetParent(null);
                throw;
            }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var node = (SerializableDOMNode)value;

            var wproperties = node.GetType().GetProperties().Where(prop =>
            {
                CustomAttributeData[] custom_attributes = prop.CustomAttributes.ToArray();
                CustomAttributeData wproperty_attribute = custom_attributes.FirstOrDefault(x => x.AttributeType.Name == "WProperty");
                if (wproperty_attribute == null)
                    return false;
                CustomAttributeData jsonignore_attribute = custom_attributes.FirstOrDefault(x => x.AttributeType.Name == "JsonIgnoreAttribute");
                if (jsonignore_attribute != null)
                    return false;
                return true;
            });

            Dictionary<string, object> objPropsDict = new Dictionary<string, object>();

            foreach (var prop in wproperties)
            {
                object propValue = prop.GetValue(node, null);

                if (prop.PropertyType == typeof(MessageReference))
                {
                    propValue = (propValue as MessageReference).MessageID;
                } else if (prop.PropertyType == typeof(Path_v2))
                {
                    WDOMNode cur_object = node;
                    while (cur_object.Parent != null)
                    {
                        cur_object = cur_object.Parent;
                    }
                    List<Path_v2> pathsList = cur_object.GetChildrenOfType<Path_v2>();

                    propValue = pathsList.IndexOf(propValue as Path_v2);
                } else if (prop.PropertyType == typeof(ExitData))
                {
                    WScene scene;
                    CustomAttributeData[] custom_attributes = prop.CustomAttributes.ToArray();
                    CustomAttributeData wproperty_attribute = custom_attributes.FirstOrDefault(x => x.AttributeType.Name == "WProperty");
                    SourceScene source_scene = (SourceScene)wproperty_attribute.ConstructorArguments[4].Value;
                    if (source_scene == SourceScene.Stage)
                    {
                        scene = m_world.Map.SceneList.First(x => x.GetType() == typeof(WStage)) as WScene;
                    } else
                    {
                        WDOMNode cur_object = node;
                        while (cur_object.Parent != null)
                        {
                            cur_object = cur_object.Parent;
                        }
                        scene = cur_object as WScene;
                    }
                    List<ExitData> exitsList = scene.GetChildrenOfType<ExitData>();

                    propValue = exitsList.IndexOf(propValue as ExitData);
                }
                else if (prop.PropertyType == typeof(MapEvent))
                {
                    WStage stage = m_world.Map.SceneList.First(x => x.GetType() == typeof(WStage)) as WStage;
                    List<MapEvent> eventsList = stage.GetChildrenOfType<MapEvent>();

                    propValue = eventsList.IndexOf(propValue as MapEvent);
                }

                objPropsDict[prop.Name] = propValue;
            }

            serializer.Serialize(writer, objPropsDict);
        }
    }
}
