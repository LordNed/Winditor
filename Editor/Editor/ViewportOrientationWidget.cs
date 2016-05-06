using OpenTK;

namespace Editor
{
    class WViewportOrientationWidget
    {
        private SimpleObjRenderer[] m_gizmoMeshes;

        public WViewportOrientationWidget()
        {
            string[] meshNames = new string[] { "TranslateCenter", "TranslateX", "TranslateY", "TranslateZ" };

            m_gizmoMeshes = new SimpleObjRenderer[meshNames.Length];
            for (int i = 0; i < m_gizmoMeshes.Length; i++)
            {
                    Obj obj = new Obj();
                    obj.Load("resources/editor/" + meshNames[i] + ".obj");
                    m_gizmoMeshes[i] = new SimpleObjRenderer(obj);
            }
        }

        public void Render(Quaternion viewportOrientation, Matrix4 viewMatrix, Matrix4 projMatrix, Matrix4 modelMatrix)
        {
            //Matrix4 modelMatrix = Matrix4.CreateFromQuaternion(Quaternion.Invert(viewportOrientation));

            for(int i = 0; i < m_gizmoMeshes.Length; i++)
            {
                m_gizmoMeshes[i].Render(viewMatrix, projMatrix, modelMatrix);
            }
        }
    }
}
