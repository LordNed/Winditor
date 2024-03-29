﻿using JStudio.J3D;
using OpenTK;
using System;
using System.Collections.Generic;

namespace WindEditor
{
    public enum MapLayer
    {
        Default,
        Layer0,
        Layer1,
        Layer2,
        Layer3,
        Layer4,
        Layer5,
        Layer6,
        Layer7,
        Layer8,
        Layer9,
        LayerA,
        LayerB,
    }

    public class WActorNode : WDOMNode, IRenderable
    {
        public string FourCC { get; protected set; }
        public MapLayer Layer { get; set; }

        public List<IPropertyValue> Properties { get; protected set; }

        public TevColorOverride ColorOverrides { get; protected set; }

        private SimpleObjRenderer m_objRender;
        private J3D m_actorMesh;
        private IPropertyValue m_namePropertyValueCache;

        public WActorNode(string fourCC, WWorld world) :base(world)
        {
            Properties = new List<IPropertyValue>();
            FourCC = fourCC;
            ColorOverrides = new TevColorOverride();
        }

        public bool TryGetValue<T>(string propertyName, out T value)
        {
            IPropertyValue prop = Properties.Find(x => x.Name == propertyName);
            if(prop != null)
            {
                try
                {
                    value = (T)prop.GetValue();
                    return true;
                }
                catch(Exception /*ex*/) { }
            }

            value = default(T);
            return false;
        }

        public void PostFinishedLoad()
        {
            m_namePropertyValueCache = Properties.Find(x => x.Name == "Name");

            if (FourCC == "ACTR" || FourCC == "SCOB" || FourCC == "TGOB" || FourCC == "PLYR" || FourCC == "SHIP")
            {
                if (m_namePropertyValueCache != null)
                {
                    m_namePropertyValueCache.PropertyChanged += OnActorNamePropertyChanged;
                }
            }

            UpdateActorMeshFromNameProperty();
        }

        private void OnActorNamePropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            UpdateActorMeshFromNameProperty();
        }

        private void UpdateActorMeshFromNameProperty()
        {
            TStringPropertyValue stringProperty = m_namePropertyValueCache as TStringPropertyValue;
            if(stringProperty != null)
            {
                //m_actorMesh = WResourceManager.LoadActorByName((string)stringProperty.GetValue());
                if(m_actorMesh != null)
                {
                    // Create and set up some initial lighting options so character's aren't drawn super brightly
                    // until we support actually loading room environment lighting and apply it (see below).
                    var lightPos = new Vector4(250, 200, 250, 0);
                    var mainLight = new JStudio.OpenGL.GXLight(lightPos, -lightPos.Normalized(), new Vector4(1, 0, 1, 1), new Vector4(1.075f, 0, 0, 0), new Vector4(1.075f, 0, 0, 0));
                    var secondaryLight = new JStudio.OpenGL.GXLight(lightPos, -lightPos.Normalized(), new Vector4(0, 0, 1, 1), new Vector4(1.075f, 0, 0, 0), new Vector4(1.075f, 0, 0, 0));

                    Quaternion lightRot = Quaternion.FromAxisAngle(Vector3.UnitY, (float)Math.PI/2f);
                    Vector3 newLightPos = Vector3.Transform(new Vector3(-500, 0, 0), lightRot) + new Vector3(0, 50, 0);

                    secondaryLight.Position = new Vector4(newLightPos, 0);

                    m_actorMesh.SetHardwareLight(0, mainLight);
                    m_actorMesh.SetHardwareLight(1, secondaryLight);
                    m_actorMesh.SetTextureOverride("ZBtoonEX", "resources/textures/ZBtoonEX.png");
                    m_actorMesh.SetTextureOverride("ZAtoon", "resources/textures/ZAtoon.png");

                    m_objRender = null;
                }
            }

            if (m_actorMesh == null)
            {
                m_objRender = WResourceManager.LoadObjResource("resources/editor/EditorCube.obj", new Vector4(1, 1, 1, 1));
            }
        }

        public override void Tick(float deltaTime)
        {
            base.Tick(deltaTime);
        }

        public override void SetTimeOfDay(float timeOfDay)
        {
            base.SetTimeOfDay(timeOfDay);

            WRoom room = Parent as WRoom;
            if (room != null)
            {
                var envLighting = room.EnvironmentLighting;
                if (envLighting != null)
                {
                    var curLight = envLighting.Lerp(EnvironmentLightingConditions.WeatherPreset.Default, true, timeOfDay);

                    // ToDo: Once Room Lighting works again, it can be applied per-frame to actors here if you
                    // override m_actorMesh.SetHardwareLight for main and secondary lighting based on what is in the
                    // actual environment lighting.
                }
            }
        }

        public override FAABox GetBoundingBox()
        {
            FAABox modelABB = m_objRender != null ? m_objRender.GetAABB() : m_actorMesh.BoundingBox;
            modelABB.ScaleBy(Transform.LocalScale);

            return new FAABox(modelABB.Min + Transform.Position, modelABB.Max + Transform.Position);
        }

        public bool Raycast(FRay ray, out float closestDistance)
        {
            // Convert the ray to local space of this node since all of our raycasts are local.
            FRay localRay = WMath.TransformRay(ray, Transform.Position, Transform.LocalScale, Transform.Rotation.Inverted().ToSinglePrecision());
            bool bHit = false;
            if (m_actorMesh != null)
            {
                bHit = m_actorMesh.Raycast(localRay, out closestDistance, true);
            }
            else
            {
                bHit = WMath.RayIntersectsAABB(localRay, m_objRender.GetAABB().Min, m_objRender.GetAABB().Max, out closestDistance);
            }

            if (bHit)
            {
                // Convert the hit point back to world space...
                Vector3 localHitPoint = localRay.Origin + (localRay.Direction * closestDistance);
                localHitPoint = Vector3.Transform(localHitPoint + Transform.Position, Transform.Rotation.ToSinglePrecision());

                // Now get the distance from the original ray origin and the new worldspace hit point.
                closestDistance = (localHitPoint - ray.Origin).Length;
            }

            return bHit;
        }

        #region IRenderable
        void IRenderable.AddToRenderer(WSceneView view)
        {
            view.AddOpaqueMesh(this);
        }

        void IRenderable.Draw(WSceneView view)
        {
            var bbox = GetBoundingBox();
            m_world.DebugDrawBox(bbox.Center, (bbox.Max - bbox.Min) / 2, Transform.Rotation.ToSinglePrecision(), (Flags & NodeFlags.Selected) == NodeFlags.Selected ? WLinearColor.White : WLinearColor.Black, 0, 0);

            Matrix4 trs = Matrix4.CreateScale(Transform.LocalScale) * Matrix4.CreateFromQuaternion(Transform.Rotation.ToSinglePrecision()) * Matrix4.CreateTranslation(Transform.Position);

            if (m_actorMesh != null)
            {
                for (int i = 0; i < 4; i++)
                {
                    if(ColorOverrides.ColorsEnabled[i])
                        m_actorMesh.SetTevColorOverride(i, ColorOverrides.Colors[i]);

                    if (ColorOverrides.ConstColorsEnabled[i])
                        m_actorMesh.SetTevkColorOverride(i, ColorOverrides.ConstColors[i]);
                }

                m_actorMesh.Render(view.ViewMatrix, view.ProjMatrix, trs);
            }
            else
                m_objRender.Render(view.ViewMatrix, view.ProjMatrix, trs);
        }

        Vector3 IRenderable.GetPosition()
        {
            Vector3 modelOffset = Vector3.Zero;
            if (m_actorMesh != null)
                modelOffset += m_actorMesh.BoundingBox.Center;

            return Transform.Position + modelOffset;
        }

        float IRenderable.GetBoundingRadius()
        {
            Vector3 lScale = Transform.LocalScale;
            float largestMax = lScale[0];
            for (int i = 1; i < 3; i++)
                if (lScale[i] > largestMax)
                    largestMax = lScale[i];

            float boundingSphere = 86f; // Default Editor Cube
            if (m_actorMesh != null)
                boundingSphere = m_actorMesh.BoundingSphere.Radius;
            if (m_objRender != null)
                boundingSphere = m_objRender.GetAABB().Extents.Length;
            return largestMax * boundingSphere;
        }
        #endregion

        public override string ToString()
        {
            if (m_namePropertyValueCache != null)
                return string.Format("[{0}] {1}", FourCC, (string)m_namePropertyValueCache.GetValue());

            return FourCC;
        }
    }
}
