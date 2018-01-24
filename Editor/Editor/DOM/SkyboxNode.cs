using JStudio.J3D;
using System.IO;
using OpenTK;

namespace WindEditor
{
    public class WSkyboxNode : WDOMNode, IRenderable
    {
        private J3DNode m_vrSky; // Sky
        private J3DNode m_vrKasumiMae; // "Haze Before" - Horizon Gradient
        private J3DNode m_vrUsoUmi; // "False Sea" - This is the model seen underneath the edge of the sea generated around the player.
        private J3DNode m_vrBackCloud; // Cloud Layer

        public WSkyboxNode(WWorld world) : base(world)
        {
            IsVisible = false;
        }

        public override void Tick(float deltaTime)
        {
            base.Tick(deltaTime);

            // Sky
            if (m_vrSky != null)
            {
                // Background
                m_vrSky.Model.SetTevkColorOverride(0, WLinearColor.FromHexString("0x1E3C5AFF"));
                // Floor
                m_vrSky.Model.SetTevColorOverride(0, WLinearColor.FromHexString("0xC8FFFFFF"));
            }

            // Horizon Color
            if (m_vrKasumiMae != null)
                m_vrKasumiMae.Model.SetTevColorOverride(0, WLinearColor.FromHexString("0x325a82FF"));

            // False Sea Color
            if (m_vrUsoUmi != null)
                m_vrUsoUmi.Model.SetTevkColorOverride(0, WLinearColor.FromHexString("0x0A0A3CFF"));

            // Cloud Color
            if (m_vrBackCloud != null)
                m_vrBackCloud.Model.SetTevColorOverride(0, WLinearColor.FromHexString("0x8278966E"));
        }

        public void UpdatePosition(Vector3 camPosition)
        {
            if (m_vrSky != null)
            {
                m_vrSky.Transform.Position = camPosition;
                m_vrSky.Transform.LocalScale = new Vector3(1000, 1000, 1000);
            }
            if (m_vrKasumiMae != null)
            {
                m_vrKasumiMae.Transform.Position = camPosition;
                m_vrKasumiMae.Transform.LocalScale = new Vector3(1000, 1000, 1000);
            }
            if (m_vrUsoUmi != null)
            {
                m_vrUsoUmi.Transform.Position = camPosition;
                m_vrUsoUmi.Transform.LocalScale = new Vector3(1000, 1000, 1000);
            }
            if (m_vrBackCloud != null)
            {
                m_vrBackCloud.Transform.Position = camPosition;
                m_vrBackCloud.Transform.LocalScale = new Vector3(1000, 1000, 1000);
            }
        }

        public void LoadSkyboxModelsFromFixedModelList(string rootFolder)
        {
            string[] fileNames = new[] { "vr_sky", "vr_kasumi_mae", "vr_uso_umi", "vr_back_cloud" };
            string[] extNames = new[] { ".bmd", ".bdl" };

            foreach (var model in fileNames)
            {
                foreach (var ext in extNames)
                {
                    string fullPath = Path.Combine(rootFolder, model + ext);
                    if (File.Exists(fullPath))
                    {
                        J3D j3dModel = WResourceManager.LoadResource(fullPath);
                        J3DNode modelNode = new J3DNode(j3dModel, m_world);

                        switch (model)
                        {
                            case "vr_sky": m_vrSky = modelNode; break;
                            case "vr_kasumi_mae": m_vrKasumiMae = modelNode; break;
                            case "vr_uso_umi": m_vrUsoUmi = modelNode; break;
                            case "vr_back_cloud": m_vrBackCloud = modelNode; break;
                        }
                    }
                }
            }
        }

        #region IRenderable
        void IRenderable.AddToRenderer(WSceneView view)
        {
            view.AddOpaqueMesh(this);
        }

        void IRenderable.Draw(WSceneView view)
        {
            DrawIfNotNull(m_vrSky, view);
            DrawIfNotNull(m_vrKasumiMae, view);
            DrawIfNotNull(m_vrUsoUmi, view);
            DrawIfNotNull(m_vrBackCloud, view);
        }

        Vector3 IRenderable.GetPosition()
        {
            return Transform.Position;
        }

        float IRenderable.GetBoundingRadius()
        {
            return float.MaxValue;
        }
        #endregion

        private void DrawIfNotNull(IRenderable skyPart, WSceneView view)
        {
            if (skyPart == null)
                return;

            skyPart.Draw(view);
        }
    }
}
