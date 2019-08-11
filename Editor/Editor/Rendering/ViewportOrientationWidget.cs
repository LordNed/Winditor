using OpenTK;
using System;

namespace WindEditor
{
    class WViewportOrientationWidget : IDisposable
    {
        private SimpleObjRenderer[] m_gizmoMeshes;

        // To detect redundant calls
        private bool m_hasbeenDisposed = false;

        public WViewportOrientationWidget()
        {
            string[] meshNames = new string[] { "TranslateCenter", "TranslateX", "TranslateY", "TranslateZ" };

            m_gizmoMeshes = new SimpleObjRenderer[meshNames.Length];
            for (int i = 0; i < m_gizmoMeshes.Length; i++)
            {
                    Obj obj = new Obj();
                    obj.Load("resources/editor/" + meshNames[i] + ".obj");
                    m_gizmoMeshes[i] = new SimpleObjRenderer(obj, new OpenTK.Vector4(1, 1, 1, 1));
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

        #region IDisposable Support
        ~WViewportOrientationWidget()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(false);
        }

        protected virtual void Dispose(bool manualDispose)
        {
            if (!m_hasbeenDisposed)
            {
                if (manualDispose)
                {
                    // Dispose managed state (managed objects).
                    for (int i = 0; i < m_gizmoMeshes.Length; i++)
                        m_gizmoMeshes[i].Dispose();
                }

                // Free unmanaged resources (unmanaged objects) and override a finalizer below.
                // Set large fields to null.

                m_hasbeenDisposed = true;
            }
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
