using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using JStudio.J3D;

namespace WindEditor.a
{
    public interface IRenderable_a
    {
        void Update(float delta_time);
        void Render(WSceneView view, WTransform global_transform);
    }

    public enum LightingType
    {
        Actor,
        Room,
        Water,
        Exterior,
        Backfill,
        Skybox
    }

    public class J3DRenderable : IRenderable_a
    {
        private J3DRenderable m_Parent;

        private J3D m_Model;
        private LightingType m_LightType;
        private string m_SocketName;

        public List<J3DRenderable> Children { get; private set; }

        public J3DRenderable(J3D model_ref, J3DRenderable parent, LightingType light_type = LightingType.Actor, string socket = "")
        {
            Children = new List<J3DRenderable>();

            m_Parent = parent;
            m_Model = model_ref;
            m_LightType = light_type;
            m_SocketName = socket;
        }

        public void ApplyEnvironmentLighting(EnvironmentLightingPalette lights)
        {
            switch(m_LightType)
            {
                case LightingType.Actor:
                    ApplyLightOverrides(lights.ShadowColor, lights.ActorAmbientColor);
                    break;
                case LightingType.Backfill:
                    ApplyLightOverrides(lights.DoorBackfill, lights.DoorBackfill);
                    break;
                case LightingType.Room:
                    ApplyLightOverrides(lights.RoomLightColor, lights.RoomAmbientColor);
                    break;
                case LightingType.Water:
                    ApplyLightOverrides(lights.WaveColor, lights.OceanColor);
                    break;
                case LightingType.Exterior:
                    ApplyLightOverrides(lights.UnknownWhite1, lights.UnknownWhite2);
                    break;
                case LightingType.Skybox:
                    ApplySkyOverrides(lights.);
                    break;
            }

            foreach (var j in Children)
            {
                j.ApplyEnvironmentLighting(lights);
            }
        }

        private void ApplyLightOverrides(WLinearColor tev_override, WLinearColor tev_k_override)
        {
            m_Model.SetTevColorOverride(0, tev_override);
            m_Model.SetTevkColorOverride(0, tev_k_override);
        }

        private void ApplySkyOverrides(EnvironmentLightingSkyboxPalette sky_colors)
        {
            switch(m_Model.Name)
            {
                case "vr_back_cloud":
                    ApplyLightOverrides(sky_colors.HorizonCloudColor, sky_colors.HorizonCloudColor);
                    break;
                case "vr_kasumi_mae":
                    ApplyLightOverrides(sky_colors.HorizonColor, sky_colors.HorizonColor);
                    break;
                case "vr_sky":
                    ApplyLightOverrides(sky_colors.SkyColor, sky_colors.CenterCloudColor);
                    break;
                case "vr_uso_umi":
                    ApplyLightOverrides(sky_colors.FalseSeaColor, sky_colors.FalseSeaColor);
                    break;
            }
        }

        #region IRenderable Interface
        public void Update(float delta_time)
        {
            m_Model.Tick(delta_time);

            foreach (var j in Children)
            {
                j.Update(delta_time);
            }
        }

        public void Render(WSceneView view, WTransform global_transform)
        {
            Matrix4 model_matrix = Matrix4.Identity;
            m_Model.Render(view.ViewMatrix, view.ProjMatrix, model_matrix);
        }
        #endregion
    }

    public abstract class WDOMRenderNode : WDOMNode
    {
        public WTransform GlobalTransform { get; protected set; }

        public WDOMRenderNode(WWorld world, string name) : base(world)
        {
            Name = name;
        }

        public virtual void AddToRenderer(WSceneView view) { }
        public virtual void Render(WSceneView view) { }
    }

    public class WJ3DRenderNode : WDOMRenderNode
    {
        public J3DRenderable Renderable { get; set; }

        public WJ3DRenderNode(WWorld world, string name) : base(world, name)
        {
            
        }

        public override void AddToRenderer(WSceneView view)
        {
            
        }

        public override void Render(WSceneView view)
        {
            Renderable.Render(view, GlobalTransform);
        }
    }
}
