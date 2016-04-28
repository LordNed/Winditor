using System;
using OpenTK;
using System.Collections.Generic;

// http://pastebin.com/raw/p8EqPs8p
// http://pastebin.com/raw/QRHEcsW2
namespace Editor
{
    class WTransformGizmo : PrimitiveComponent
    {
        public enum TransformMode
        {
            None,
            Translation,
            Rotation,
            Scale
        }

        enum SelectedAxes
        {
            None,
            X, Y, Z,
            XY, XZ, YZ,
            All,
        }

        struct AxisDistanceResult
        {
            public SelectedAxes Axis;
            public float Distance;

            public AxisDistanceResult(SelectedAxes axis, float intersectDist)
            {
                Axis = axis;
                Distance = intersectDist;
            }
        }

        private WTransform m_transform;
        private TransformMode m_mode;
        private SelectedAxes m_selectedAxes;

        private WPlane m_translationPlane;
        private bool m_hasSetMouseOffset;
        private bool m_isTransforming;
        private bool m_hasTransformed;
        private Vector3 m_hitPoint;
        private Vector3 m_moveDir;
        private Vector2 m_wrapOffset;
        private float m_cameraDistance;

        private bool mFlipScaleX;
        private bool mFlipScaleY;
        private bool mFlipScaleZ;

        // Delta Transforms
        private Vector3 m_deltaTranslation;
        private Vector3 m_totalTranslation;
        private Vector3 m_translateOffset;

        private Quaternion m_deltaRotation;
        private Vector3 m_totalRotation; // Stored as Vec3 for UI Purposes.
        private float m_rotateOffset;


        private Vector3 m_deltaScale;
        private float m_scaleOffset;

        // hack...
        private WLineBatcher m_lineBatcher;
        private SimpleObjRenderer[] m_meshes;


        public WTransformGizmo(WLineBatcher lines)
        {
            // hack...
            m_lineBatcher = lines;


            m_transform = new WTransform();
            m_mode = TransformMode.Translation;
            m_selectedAxes = SelectedAxes.None;

            string[] meshNames = new[]
            {
                /*"TranslateCenter", "TranslateX", "TranslateY", "TranslateZ", "TranslateLinesXY", "TranslateLinesXZ", "TranslateLinesYZ", "RotateX", "RotateY", "RotateZ",*/ "ScaleCenter", "ScaleX", "ScaleY", "ScaleZ", "ScaleLinesXY", "ScaleLinesXZ", "ScaleLinesYZ", 
            };

            m_meshes = new SimpleObjRenderer[meshNames.Length];
            for (int i = 0; i < m_meshes.Length; i++)
            {
                Obj obj = new Obj();
                obj.Load("resources/editor/" + meshNames[i] + ".obj");
                m_meshes[i] = new SimpleObjRenderer(obj);
            }
        }

        public void SetMode(TransformMode mode)
        {
            m_mode = mode;

            switch (mode)
            {
                case TransformMode.Translation:
                    m_deltaRotation = Quaternion.Identity;
                    //m_deltaScale = Vector3.One;
                    break;
                case TransformMode.Rotation:
                    m_deltaTranslation = Vector3.Zero;
                    //m_deltaScale = Vector3.One;
                    break;
            }
        }

        private void StartTransform()
        {
            m_isTransforming = true;
            m_hasTransformed = false;
            m_hasSetMouseOffset = false;
            m_totalTranslation = Vector3.Zero;
            m_totalRotation = Vector3.Zero;
            m_transform.Rotation = Quaternion.Identity;
            m_transform.LocalScale = Vector3.One;
            m_wrapOffset = Vector2.Zero;

            if (m_mode == TransformMode.Rotation)
            {
                // We need to determine which side of the gizmo they are on, so that goes the expected direction when
                // we pull up/down in screenspace.
                Vector3 rotAxis;
                if (m_selectedAxes == SelectedAxes.X) rotAxis = Vector3.UnitX;
                if (m_selectedAxes == SelectedAxes.Y) rotAxis = Vector3.UnitY;
                else rotAxis = Vector3.UnitZ;

                Vector3 dirFromGizmoToHitPoint = (m_hitPoint - m_transform.Position).Normalized();
                m_moveDir = Vector3.Cross(rotAxis, dirFromGizmoToHitPoint);
            }
        }

        private void EndTransform()
        {
            m_isTransforming = false;
            m_selectedAxes = SelectedAxes.None;
        }


        public override void Tick(float deltaTime)
        {
            if (WInput.GetKeyDown(System.Windows.Input.Key.Q) && !WInput.GetMouseButton(1))
            {
                SetMode(TransformMode.None);
            }
            if (WInput.GetKeyDown(System.Windows.Input.Key.W) && !WInput.GetMouseButton(1))
            {
                SetMode(TransformMode.Translation);
            }
            if (WInput.GetKeyDown(System.Windows.Input.Key.E) && !WInput.GetMouseButton(1))
            {
                SetMode(TransformMode.Rotation);
            }
            if (WInput.GetKeyDown(System.Windows.Input.Key.R) && !WInput.GetMouseButton(1))
            {
                SetMode(TransformMode.Scale);
            }

            if (WInput.GetMouseButtonDown(0))
            {
                WRay mouseRay = WSceneView.ProjectScreenToWorld(WInput.MousePosition);
                if (CheckSelectedAxes(mouseRay))
                {
                    Console.WriteLine("TranslationGizmo clicked. Selected Axes: {0}", m_selectedAxes);
                    StartTransform();
                }
            }

            if (WInput.GetMouseButtonUp(0))
            {
                EndTransform();
            }

            if (m_isTransforming)
            {
                WRay mouseRay = WSceneView.ProjectScreenToWorld(WInput.MousePosition);
                Vector3 cameraPos = WSceneView.GetCameraPos();
                TransformFromInput(mouseRay, cameraPos);
            }

            // Update camera distance to our camera.
            if((!m_isTransforming) || (m_mode != TransformMode.Translation))
            {
                m_cameraDistance = (WSceneView.GetCameraPos() - m_transform.Position).Length;
            }

            WLinearColor[] gizmoColors = new[]
            {
                    WLinearColor.Red,
                    WLinearColor.Green,
                    WLinearColor.Blue,
                    WLinearColor.Blue,
                    WLinearColor.Red,
                    WLinearColor.Green,
                    WLinearColor.White,
                };

            List<AxisDistanceResult> results = new List<AxisDistanceResult>();

            var gizmoBoxes = GetAABBBoundsForMode(m_mode);
            for (int i = 0; i < gizmoBoxes.Length; i++)
            {
                m_lineBatcher.DrawBox(gizmoBoxes[i].Min + m_transform.Position, gizmoBoxes[i].Max + m_transform.Position, gizmoColors[i], 25, 0f);
            }

            // Update Highlight Status of Models. ToDo: Less awful.
            //m_meshes[1].Highlighted = ContainsAxis(m_selectedAxes, SelectedAxes.X);
            //m_meshes[2].Highlighted = ContainsAxis(m_selectedAxes, SelectedAxes.Y);
            //m_meshes[3].Highlighted = ContainsAxis(m_selectedAxes, SelectedAxes.Z);
            //m_meshes[4].Highlighted = m_selectedAxes == SelectedAxes.XY;
            //m_meshes[5].Highlighted = m_selectedAxes == SelectedAxes.XZ;
            //m_meshes[6].Highlighted = m_selectedAxes == SelectedAxes.YZ;
        }

        private bool CheckSelectedAxes(WRay ray)
        {
            // Convert the ray into local space so we can use axis-aligned checks, this solves the checking problem
            // when the gizmo is rotated due to being in Local mode.
            WRay localRay = new WRay();
            localRay.Direction = Vector3.Transform(ray.Direction, m_transform.Rotation.Inverted());
            localRay.Origin = Vector3.Transform(ray.Origin, m_transform.Rotation.Inverted()) - m_transform.Position;

            m_lineBatcher.DrawLine(localRay.Origin, localRay.Origin + (localRay.Direction * 10000), WLinearColor.White, 25, 5);
            List<AxisDistanceResult> results = new List<AxisDistanceResult>();

            if (m_mode == TransformMode.Translation)
            {
                AABox[] translationAABB = GetAABBBoundsForMode(TransformMode.Translation);
                for (int i = 0; i < translationAABB.Length; i++)
                {
                    float intersectDist;
                    if (WMath.RayIntersectsAABB(localRay, translationAABB[i].Min, translationAABB[i].Max, out intersectDist))
                    {
                        results.Add(new AxisDistanceResult((SelectedAxes)(i + 1), intersectDist));
                    }
                }
            }
            else if (m_mode == TransformMode.Rotation)
            {
                // We'll use a combination of AABB and Distance checks to give us the quarter-circles we need.
                AABox[] rotationAABB = GetAABBBoundsForMode(TransformMode.Rotation);

                for (int i = 0; i < rotationAABB.Length; i++)
                {
                    float intersectDist;
                    if (WMath.RayIntersectsAABB(localRay, rotationAABB[i].Min, rotationAABB[i].Max, out intersectDist))
                    {
                        Vector3 intersectPoint = localRay.Origin + (localRay.Direction * intersectDist);

                        // Convert this aabb check into a radius check so we clip it by the semi-circles
                        // that the rotation tool actually is.
                        if (intersectPoint.Length > 100f)
                            continue;

                        results.Add(new AxisDistanceResult((SelectedAxes)(i + 1), intersectDist));
                    }
                }
            }
            else if (m_mode == TransformMode.Scale)
            {
                AABox[] scaleAABB = GetAABBBoundsForMode(TransformMode.Scale);
                for (int i = 0; i < scaleAABB.Length; i++)
                {
                    float intersectDist;
                    if (WMath.RayIntersectsAABB(localRay, scaleAABB[i].Min, scaleAABB[i].Max, out intersectDist))
                    {
                        // Special-case here to give the center scale point overriding priority. Because we intersected
                        // it, we can just override its distance to zero to make it clickable through the other bounding boxes.
                        if ((SelectedAxes)i + 1 == SelectedAxes.All)
                            intersectDist = 0f;

                        results.Add(new AxisDistanceResult((SelectedAxes)(i + 1), intersectDist));
                    }
                }
            }

            if (results.Count == 0)
            {
                m_selectedAxes = SelectedAxes.None;
                return false;
            }

            // If we get an intersection, sort them by the closest intersection distance.
            results.Sort((x, y) => x.Distance.CompareTo(y.Distance));
            m_selectedAxes = results[0].Axis;

            // Store where the mouse hit on the first frame in world space. This means converting the ray back to worldspace.
            Vector3 localHitPoint = localRay.Origin + (localRay.Direction * results[0].Distance);
            m_hitPoint = Vector3.Transform(localHitPoint, m_transform.Rotation) + m_transform.Position;
            return true;
        }

        private bool TransformFromInput(WRay ray, Vector3 cameraPos)
        {
            if (m_mode != TransformMode.Translation)
                WrapCursor();

            int numAxis = (m_selectedAxes == SelectedAxes.X || m_selectedAxes == SelectedAxes.Y || m_selectedAxes == SelectedAxes.Z) ? 1 : 2;
            if (m_selectedAxes == SelectedAxes.All)
                numAxis = 3;

            // Store the cursor position in viewport coordinates.
            Vector2 screenDimensions = App.GetScreenGeometry();
            Vector2 cursorPos = App.GetCursorPosition();
            Vector2 mouseCoords = new Vector2(((2f * cursorPos.X) / screenDimensions.X) - 1f, (1f - ((2f * cursorPos.Y) / screenDimensions.Y))); //[-1,1] range

            if (m_mode == TransformMode.Translation)
            {
                // Create a Translation Plane
                Vector3 axisA, axisB;

                if (numAxis == 1)
                {
                    if (m_selectedAxes == SelectedAxes.X)
                        axisB = Vector3.UnitX;
                    else if (m_selectedAxes == SelectedAxes.Y)
                        axisB = Vector3.UnitY;
                    else
                        axisB = Vector3.UnitZ;

                    Vector3 dirToCamera = (m_transform.Position - cameraPos).Normalized();
                    axisA = Vector3.Cross(axisB, dirToCamera);
                }
                else //if(numAxis == 2)
                {
                    axisA = ContainsAxis(m_selectedAxes, SelectedAxes.X) ? Vector3.UnitX : Vector3.UnitZ;
                    axisB = ContainsAxis(m_selectedAxes, SelectedAxes.Y) ? Vector3.UnitY : Vector3.UnitZ;
                }

                Vector3 planeNormal = Vector3.Cross(axisA, axisB).Normalized();
                m_translationPlane = new WPlane(planeNormal, m_transform.Position);
                float intersectDist;
                bool bIntersectsPlane = m_translationPlane.RayIntersectsPlane(ray, out intersectDist);

                //Console.WriteLine("planeNormal: {0} axisA: {1}, axisB: {2} numAxis: {3} selectedAxes: {4} intersectsPlane: {5} @dist: {6}", planeNormal, axisA, axisB, numAxis, m_selectedAxes, bIntersectsPlane, intersectDist);
                if (bIntersectsPlane)
                {
                    Vector3 hitPos = ray.Origin + (ray.Direction * intersectDist);
                    Vector3 localOffset = Vector3.Transform(hitPos - m_transform.Position, m_transform.Rotation.Inverted());

                    // Calculate a new position
                    Vector3 newPos = m_transform.Position;
                    if (ContainsAxis(m_selectedAxes, SelectedAxes.X))
                        newPos += Vector3.UnitX * localOffset.X;
                    if (ContainsAxis(m_selectedAxes, SelectedAxes.Y))
                        newPos += Vector3.UnitY * localOffset.Y;
                    if (ContainsAxis(m_selectedAxes, SelectedAxes.Z))
                        newPos += Vector3.UnitZ * localOffset.Z;


                    // Check the new location to see if it's skyrocked off into the distance due to near-plane raytracing issues.
                    Vector3 newPosDirToCamera = (newPos - cameraPos).Normalized();
                    float dot = Math.Abs(Vector3.Dot(planeNormal, newPosDirToCamera));

                    //Console.WriteLine("hitPos: {0} localOffset: {1} newPos: {2}, dotResult: {3}", hitPos, localOffset, newPos, dot);
                    if (dot < 0.02f)
                        return false;

                    // This is used to set the offset to the gizmo the mouse cursor is from the origin of the gizmo on the first frame
                    // that you click on the gizmo.
                    if (!m_hasSetMouseOffset)
                    {
                        m_translateOffset = m_transform.Position - newPos;
                        m_deltaTranslation = Vector3.Zero;
                        m_hasSetMouseOffset = true;
                        return false;
                    }
                    else
                    {
                        // Apply Translation
                        m_deltaTranslation = Vector3.Transform(newPos - m_transform.Position + m_translateOffset, m_transform.Rotation.Inverted());
                        //Console.WriteLine("deltaTranslation: {0}", m_deltaTranslation);
                        if (!ContainsAxis(m_selectedAxes, SelectedAxes.X)) m_deltaTranslation.X = 0f;
                        if (!ContainsAxis(m_selectedAxes, SelectedAxes.Y)) m_deltaTranslation.Y = 0f;
                        if (!ContainsAxis(m_selectedAxes, SelectedAxes.Z)) m_deltaTranslation.Z = 0f;

                        m_totalTranslation += m_deltaTranslation;
                        m_transform.Position += Vector3.Transform(m_deltaTranslation, m_transform.Rotation);

                        if (!m_hasTransformed && (m_deltaTranslation != Vector3.Zero))
                            m_hasTransformed = true;

                        return m_hasTransformed;
                    }
                }
                else
                {
                    // Our raycast missed the plane
                    m_deltaTranslation = Vector3.Zero;
                    return false;
                }
            }
            else if (m_mode == TransformMode.Rotation)
            {
                Vector3 rotationAxis;
                if (m_selectedAxes == SelectedAxes.X) rotationAxis = Vector3.UnitX;
                else if (m_selectedAxes == SelectedAxes.Y) rotationAxis = Vector3.UnitY;
                else rotationAxis = Vector3.UnitZ;

                // Convert these from [0-1] to [-1, 1] to match our mouse coords.
                Vector2 lineOrigin = (WSceneView.UnprojectWorldToViewport(m_hitPoint) * 2) - Vector2.One;
                Vector2 lineEnd = (WSceneView.UnprojectWorldToViewport(m_hitPoint + m_moveDir) * 2) - Vector2.One;

                lineOrigin.Y = -lineOrigin.Y;
                lineEnd.Y = -lineEnd.Y;


                Vector2 lineDir = (lineEnd - lineOrigin).Normalized();
                float rotAmount = Vector2.Dot(lineDir, mouseCoords + m_wrapOffset - lineOrigin) * 180f;
                //Console.WriteLine("lineDir: {0} rotAmount: {1} mc-lo: {2}", lineDir, rotAmount, (mouseCoords-lineOrigin));

                if (float.IsNaN(rotAmount))
                {
                    Console.WriteLine("rotAmountNaN!");
                    return false;
                }

                if (!m_hasSetMouseOffset)
                {
                    m_rotateOffset = -rotAmount;
                    m_deltaRotation = Quaternion.Identity;
                    m_hasSetMouseOffset = true;
                    return false;
                }

                // Apply Rotation
                float oldRotAmount = rotAmount;
                rotAmount += m_rotateOffset;

                Quaternion oldRot = m_transform.Rotation;
                m_transform.Rotation = Quaternion.FromAxisAngle(rotationAxis, WMath.DegreesToRadians(rotAmount));
                m_deltaRotation = m_transform.Rotation * oldRot.Inverted();

                //if(m_transformSpace == TransformSpace.Local)
                //m_transform.Rotation *= m_deltaRotation;

                // Add to Total Rotation recorded for UI.
                if (m_selectedAxes == SelectedAxes.X) m_totalRotation.X = rotAmount;
                else if (m_selectedAxes == SelectedAxes.Y) m_totalRotation.Y = rotAmount;
                else m_totalRotation.Z = rotAmount;

                if (!m_hasTransformed && rotAmount != 0f)
                    m_hasTransformed = true;

                return m_hasTransformed;
            }
            else if (m_mode == TransformMode.Scale)
            {
                // Create a line in screen space.
                // Convert these from [0-1] to [-1, 1] to match our mouse coords.
                Vector2 lineOrigin = (WSceneView.UnprojectWorldToViewport(m_transform.Position) * 2) - Vector2.One;
                lineOrigin.Y = -lineOrigin.Y;

                // Determine the appropriate world space directoin using the selected axes and then conver this for use with
                // screen-space controlls. This has to be done every frame because the axes can be flipped while the gizmo
                // is transforming, so we can't pre-calculate this.
                Vector3 dirX = mFlipScaleX ? -Vector3.UnitX : Vector3.UnitX; // ToDo, transform these by our rotation.
                Vector3 dirY = mFlipScaleY ? -Vector3.UnitY : Vector3.UnitY; // ToDo, transform these by our rotation.
                Vector3 dirZ = mFlipScaleZ ? -Vector3.UnitZ : Vector3.UnitZ; // ToDo, transform these by our rotation.
                Vector2 lineDir;

                // If there is only one axis, then the world space direction is the selected axis.
                if (numAxis == 1)
                {
                    Vector3 worldDir;
                    if (ContainsAxis(m_selectedAxes, SelectedAxes.X)) worldDir = dirX;
                    if (ContainsAxis(m_selectedAxes, SelectedAxes.Y)) worldDir = dirY;
                    else worldDir = dirZ;

                    Vector2 worldPoint = (WSceneView.UnprojectWorldToViewport(m_transform.Position + worldDir) * 2) - Vector2.One;
                    worldPoint.Y = -lineOrigin.Y;

                    lineDir = (worldPoint - lineOrigin).Normalized();
                }
                // If there's two axii selected, then convert both to screen space and average them out to get the line direction.
                else if (numAxis == 2)
                {
                    Vector3 axisA = ContainsAxis(m_selectedAxes, SelectedAxes.X) ? dirX : dirY;
                    Vector3 axisB = ContainsAxis(m_selectedAxes, SelectedAxes.Z) ? dirZ : dirY;

                    Vector2 screenA = (WSceneView.UnprojectWorldToViewport(m_transform.Position + axisA) * 2) - Vector2.One;
                    screenA.Y = -screenA.Y;
                    Vector2 screenB = (WSceneView.UnprojectWorldToViewport(m_transform.Position + axisB) * 2) - Vector2.One;
                    screenB.Y = -screenB.Y;

                    screenA = (screenA - lineOrigin).Normalized();
                    screenB = (screenB - lineOrigin).Normalized();
                    lineDir = ((screenA + screenB) / 2f).Normalized();
                }
                // There's three axis, just use up.
                else lineDir = Vector2.UnitY;

                float scaleAmount = Vector2.Dot(lineDir, mouseCoords + m_wrapOffset - lineOrigin) * 5f;

                // Set their initial offset if we haven't already
                if(!m_hasSetMouseOffset)
                {
                    m_scaleOffset = -scaleAmount;
                    m_deltaScale = Vector3.One;
                    m_hasSetMouseOffset = true;
                    return false;
                }

                // Apply the scale
                scaleAmount = scaleAmount + m_scaleOffset + 1f;

                // A multiplier is applied to the scale amount if it's less than one to prevent it dropping into the negatives.
                // ???
                if (scaleAmount < 1f)
                    scaleAmount = 1f / (-(scaleAmount - 1f) + 1f);

                Vector3 oldScale = m_transform.LocalScale;
                Vector3 totalScale = Vector3.One;
                //m_totalScale = Vector3.One;
                //if (ContainsAxis(m_selectedAxes, SelectedAxes.X)) m_totalScale.X = scaleAmount;
                //if (ContainsAxis(m_selectedAxes, SelectedAxes.Y)) m_totalScale.Y = scaleAmount;
                //if (ContainsAxis(m_selectedAxes, SelectedAxes.Z)) m_totalScale.Z = scaleAmount;

                if (ContainsAxis(m_selectedAxes, SelectedAxes.X)) totalScale.X = scaleAmount;
                if (ContainsAxis(m_selectedAxes, SelectedAxes.Y)) totalScale.Y = scaleAmount;
                if (ContainsAxis(m_selectedAxes, SelectedAxes.Z)) totalScale.Z = scaleAmount;

                //m_deltaScale = m_totalScale / oldScale;
                m_deltaScale = new Vector3(totalScale.X / oldScale.X, totalScale.Y / oldScale.Y, totalScale.Z / oldScale.Z);
                m_transform.LocalScale = m_deltaScale;

                if (!m_hasTransformed && (scaleAmount != 1f))
                    m_hasTransformed = true;

                return m_hasTransformed;
            }

            return false;
        }

        private bool ContainsAxis(SelectedAxes valToCheck, SelectedAxes majorAxis)
        {
            if (!(majorAxis == SelectedAxes.X || majorAxis == SelectedAxes.Y || majorAxis == SelectedAxes.Z))
                throw new ArgumentException("Only use X, Y, or Z here.", "majorAxis");

            switch (majorAxis)
            {
                case SelectedAxes.X:
                    return valToCheck == SelectedAxes.X || valToCheck == SelectedAxes.XY || valToCheck == SelectedAxes.XZ;
                case SelectedAxes.Y:
                    return valToCheck == SelectedAxes.Y || valToCheck == SelectedAxes.XY || valToCheck == SelectedAxes.YZ;
                case SelectedAxes.Z:
                    return valToCheck == SelectedAxes.Z || valToCheck == SelectedAxes.XZ || valToCheck == SelectedAxes.YZ;
            }

            return false;
        }

        private AABox[] GetAABBBoundsForMode(TransformMode mode)
        {
            switch (mode)
            {
                case TransformMode.Translation:
                    float boxLength = 100f;
                    float boxHalfWidth = 5;

                    var translationAABB =  new[]
                    {
                        // X Axis
                        new AABox(new Vector3(0, -boxHalfWidth, -boxHalfWidth), new Vector3(boxLength, boxHalfWidth, boxHalfWidth)),
                        // Y Axis
                        new AABox(new Vector3(-boxHalfWidth, 0, -boxHalfWidth), new Vector3(boxHalfWidth, boxLength, boxHalfWidth)),
                        // Z Axis
                        new AABox(new Vector3(-boxHalfWidth, -boxHalfWidth, 0), new Vector3(boxHalfWidth, boxHalfWidth, boxLength)),
                        // YX Axes
                        new AABox(new Vector3(0, 0, -2), new Vector3(boxHalfWidth*6, boxHalfWidth*6, 2)),
                        // YZ Axes
                        new AABox(new Vector3(0, -2, 0), new Vector3(boxHalfWidth*6, 2, boxHalfWidth*6)),
                        // XZ Axes
                        new AABox(new Vector3(-2, 0, 0), new Vector3(2, boxHalfWidth*6, boxHalfWidth*6)),
                    };
                    for (int i = 0; i < translationAABB.Length; i++)
                        translationAABB[i].ScaleBy(m_transform.LocalScale);
                    return translationAABB;

                case TransformMode.Rotation:
                    float radius = 100f;

                    var rotationAABB = new[]
                    {
                         // Y Axis (XZ Plane)
                        new AABox(new Vector3(-2, 0, 0), new Vector3(2, radius, radius)),
                        // Z Axis (XY Plane)
                        new AABox(new Vector3(0, -2, 0), new Vector3(radius, 2, radius)),
                        // X Axis (YZ Plane)
                        new AABox(new Vector3(0, 0, -2), new Vector3(radius, radius, 2)),
                    };

                    for (int i = 0; i < rotationAABB.Length; i++)
                        rotationAABB[i].ScaleBy(m_transform.LocalScale);
                    return rotationAABB;

                case TransformMode.Scale:
                    float scaleLength = 100f;
                    float scaleHalfWidth = 5;
                    float scaleCornerSize = 38;

                    var scaleAABB =  new[]
                    {
                        // X Axis
                        new AABox(new Vector3(0, -scaleHalfWidth, -scaleHalfWidth), new Vector3(scaleLength, scaleHalfWidth, scaleHalfWidth)),
                        // Y Axis
                        new AABox(new Vector3(-scaleHalfWidth, 0, -scaleHalfWidth), new Vector3(scaleHalfWidth, scaleLength, scaleHalfWidth)),
                        // Z Axis
                        new AABox(new Vector3(-scaleHalfWidth, -scaleHalfWidth, 0), new Vector3(scaleHalfWidth, scaleHalfWidth, scaleLength)),
                        // YX Axes
                        new AABox(new Vector3(0, 0, -2), new Vector3(scaleCornerSize, scaleCornerSize, 2)),
                        // YZ Axes
                        new AABox(new Vector3(0, -2, 0), new Vector3(scaleCornerSize, 2, scaleCornerSize)),
                        // XZ Axes
                        new AABox(new Vector3(-2, 0, 0), new Vector3(2, scaleCornerSize, scaleCornerSize)),
                        // Center
                        new AABox(new Vector3(-7, -7, -7), new Vector3(7, 7, 7))
                  };

                    for (int i = 0; i < scaleAABB.Length; i++)
                        scaleAABB[i].ScaleBy(m_transform.LocalScale);
                    return scaleAABB;
            }

            return new AABox[0];
        }

        private void WrapCursor()
        {
            Vector2 screenSize = App.GetScreenGeometry();
            Vector2 cursorPos = App.GetCursorPosition();

            // Horizontal
            if (cursorPos.X >= screenSize.X - 1)
            {
                App.SetCursorPosition(1, cursorPos.Y);
                m_wrapOffset.X += 2f;
            }
            else if (cursorPos.X <= 0)
            {
                App.SetCursorPosition(screenSize.X - 2, cursorPos.Y);
                m_wrapOffset.X -= 2f;
            }

            cursorPos = App.GetCursorPosition();

            // Vertical
            if (cursorPos.Y >= screenSize.Y - 1)
            {
                App.SetCursorPosition(cursorPos.X, 1);
                m_wrapOffset.Y -= 2f;
            }
            else if (cursorPos.Y <= 0)
            {
                App.SetCursorPosition(cursorPos.X, screenSize.Y - 2);
                m_wrapOffset.Y += 2f;
            }
        }

        #region Rendering
        public override void ReleaseResources()
        {
            for (int i = 0; i < m_meshes.Length; i++)
                m_meshes[i].ReleaseResources();
        }

        public override void Render(Matrix4 viewMatrix, Matrix4 projMatrix)
        {
            // hack
                m_transform.LocalScale = Vector3.One * (0.1f * (m_cameraDistance / 100f));

            // Construct a model matrix for the gizmo mesh to render at.
            Matrix4 modelMatrix = Matrix4.CreateScale(m_transform.LocalScale) * Matrix4.CreateFromQuaternion(m_transform.Rotation) * Matrix4.CreateTranslation(m_transform.Position);
            for (int i = 0; i < m_meshes.Length; i++)
                m_meshes[i].Render(viewMatrix, projMatrix, modelMatrix);
        }
        #endregion
    }
}
