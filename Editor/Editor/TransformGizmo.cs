using System;
using OpenTK;
using System.Collections.Generic;

// http://pastebin.com/raw/p8EqPs8p
// http://pastebin.com/raw/QRHEcsW2
namespace Editor
{
    public enum FTransformMode
    {
        None,
        Translation,
        Rotation,
        Scale
    }

    public enum FTransformSpace
    {
        Local,
        World
    }

    public enum FSelectedAxes
    {
        None,//    = 0x0,
        X,//       = 0x1,
        Y,//       = 0x2,
        Z,//       = 0x4,
        XY,//      = X | Y,
        XZ,//      = X | Z,
        YZ,//      = Y | Z,
        All,//     = X | Y | Z
    }

    class WTransformGizmo : PrimitiveComponent
    {
        public FSelectedAxes SelectedAxes { get { return m_selectedAxes; } }
        public FTransformMode Mode { get { return m_mode; } }
        public FTransformSpace TransformSpace { get { return m_transformSpace; } }
        public bool IsTransforming { get { return m_isTransforming; } }

        // Delta Transforms
        public Vector3 DeltaTranslation { get { return m_deltaTranslation; } }

        struct AxisDistanceResult
        {
            public FSelectedAxes Axis;
            public float Distance;

            public AxisDistanceResult(FSelectedAxes axis, float intersectDist)
            {
                Axis = axis;
                Distance = intersectDist;
            }
        }

        //private WTransform m_transform;
        private FTransformMode m_mode;
        private FSelectedAxes m_selectedAxes = FSelectedAxes.None;
        private FTransformSpace m_transformSpace = FTransformSpace.World;

        private bool m_isTransforming;
        private bool m_hasTransformed;
        private Vector2 m_wrapOffset;
        private float m_cameraDistance;
        private float m_gizmoSize = 0.25f;

        // Transform Values - stored outside of the transform so we can store their values without modifying how they're rendered.

        /// <summary> What is the current position of the gizmo. </summary>
        private Vector3 m_position = Vector3.Zero;
        private Quaternion m_rotation = Quaternion.Identity;
        private Quaternion m_localRotation = Quaternion.Identity;
        /// <summary> Current scale of the gizmo, modified by the distance on screen.</summary>
        private Vector3 m_scale = Vector3.One;
        private bool mFlipScaleX;
        private bool mFlipScaleY;
        private bool mFlipScaleZ;

        // Delta Transforms
        /// <summary> What is the delta translation for this frame. </summary>
        private Vector3 m_deltaTranslation;
        /// <summary> The total translation of the gizmo since the gizmo started moving. Cleared the next time the gizmo is clicked. </summary>
        private Vector3 m_totalTranslation;
        /// <summary> The delta rotation as a <see cref="Quaternion"/> for this frame. </summary>
        private Quaternion m_deltaRotation;
        /// <summary> The total rotation of the gizmo since it was last clicked as a <see cref="Quaternion"/>. </summary>
        private Quaternion m_currentRotation;
        /// <summary> The total rotation of the gizmo since it was last clicked as a <see cref="Vector3"/> for UI purposes. </summary>
        private Vector3 m_totalRotation;
        /// <summary> The delta in scale for this frame. Set to 1,1,1 when no change is made. </summary>
        private Vector3 m_deltaScale = Vector3.One;
        /// <summary> The total scale of the gizmo since the last time the gizmo was clicked. Cleared the next time it is clicked. </summary>
        private Vector3 m_totalScale = Vector3.One;

        // Transform Helpers
        private WPlane m_translationPlane;
        private Vector3 m_translateOffset;
        private float m_rotateOffset;
        private float m_scaleOffset;
        private bool m_hasSetMouseOffset;
        private Vector3 m_hitPoint;
        private Vector3 m_moveDir;

        // hack...
        private WLineBatcher m_lineBatcher;
        private SimpleObjRenderer[][] m_gizmoMeshes;


        public WTransformGizmo(WLineBatcher lines)
        {
            // hack...
            m_lineBatcher = lines;

            SetMode(FTransformMode.Translation);

            string[][] meshNames = new string[][]
            {
                new string[] {"TranslateCenter", "TranslateX", "TranslateY", "TranslateZ", "TranslateLinesXY", "TranslateLinesXZ", "TranslateLinesYZ" },
                new string[] { "RotateX", "RotateY", "RotateZ" },
                new string[] { "ScaleCenter", "ScaleX", "ScaleY", "ScaleZ", "ScaleLinesXY", "ScaleLinesXZ", "ScaleLinesYZ" }
            };

            m_gizmoMeshes = new SimpleObjRenderer[meshNames.Length][];
            for (int i = 0; i < m_gizmoMeshes.Length; i++)
            {
                m_gizmoMeshes[i] = new SimpleObjRenderer[meshNames[i].Length];
                
                for(int j = 0; j < m_gizmoMeshes[i].Length; j++)
                {
                    Obj obj = new Obj();
                    obj.Load("resources/editor/" + meshNames[i][j] + ".obj");
                    m_gizmoMeshes[i][j] = new SimpleObjRenderer(obj);
                }
            }
        }

        public void SetMode(FTransformMode mode)
        {
            m_mode = mode;

            switch (mode)
            {
                case FTransformMode.Translation:
                    m_deltaRotation = Quaternion.Identity;
                    m_deltaScale = Vector3.One;
                    break;
                case FTransformMode.Rotation:
                    m_deltaTranslation = Vector3.Zero;
                    m_deltaScale = Vector3.One;
                    break;
                case FTransformMode.Scale:
                    m_deltaTranslation = Vector3.Zero;
                    m_deltaRotation = Quaternion.Identity;
                    break;
            }
        }

        public void StartTransform()
        {
            m_isTransforming = true;
            m_hasTransformed = false;
            m_hasSetMouseOffset = false;
            m_wrapOffset = Vector2.Zero;
            m_totalTranslation = Vector3.Zero;
            m_totalRotation = Vector3.Zero;
            m_currentRotation = Quaternion.Identity;
            m_totalScale = Vector3.One;

            // Set the rotaiton direction.
            if (m_mode == FTransformMode.Rotation)
            {
                // We need to determine which side of the gizmo they are on, so that goes the expected direction when
                // we pull up/down in screenspace.
                Vector3 rotAxis;
                if (m_selectedAxes == FSelectedAxes.X) rotAxis = Vector3.UnitX;
                if (m_selectedAxes == FSelectedAxes.Y) rotAxis = Vector3.UnitY;
                else rotAxis = Vector3.UnitZ;

                Vector3 dirFromGizmoToHitPoint = (m_hitPoint - m_position).Normalized();
                m_moveDir = Vector3.Cross(rotAxis, dirFromGizmoToHitPoint);
            }

            // Set the scale direction.
            else if (m_mode == FTransformMode.Scale)
            {
                // If we're transforming only on one axis, then the directon is in the selected axis.
                if(GetNumSelectedAxes() == 1)
                {
                    if (ContainsAxis(m_selectedAxes, FSelectedAxes.X)) m_moveDir = Vector3.Transform(Vector3.UnitX, m_rotation);
                    if (ContainsAxis(m_selectedAxes, FSelectedAxes.Y)) m_moveDir = Vector3.Transform(Vector3.UnitY, m_rotation);
                    else                                              m_moveDir = Vector3.Transform(Vector3.UnitZ, m_rotation);
                }
                // Two axes however, means interpolate between both.
                if(GetNumSelectedAxes() == 2)
                {
                    Vector3 axisA = ContainsAxis(m_selectedAxes, FSelectedAxes.X) ? Vector3.Transform(Vector3.UnitX, m_rotation) : Vector3.Transform(Vector3.UnitY, m_rotation);
                    Vector3 axisB = ContainsAxis(m_selectedAxes, FSelectedAxes.Z) ? Vector3.Transform(Vector3.UnitZ, m_rotation) : Vector3.Transform(Vector3.UnitY, m_rotation);
                    m_moveDir = (axisA + axisB) / 2f;
                }
            }
        }

        public void EndTransform()
        {
            m_isTransforming = false;
            m_selectedAxes = FSelectedAxes.None;
        }

        private int GetNumSelectedAxes()
        {
            // ToDo: This seems kind of stupid...
            switch (m_selectedAxes)
            {
                case FSelectedAxes.X: 
                case FSelectedAxes.Y:
                case FSelectedAxes.Z: return 1;
                case FSelectedAxes.XY:
                case FSelectedAxes.YZ:
                case FSelectedAxes.XZ: return 2;
                case FSelectedAxes.All: return 3;
            }

            return 0;
        }

        public void IncrementSize()
        {
            m_gizmoSize += 0.05f;
        }

        public void DecrementSize()
        {
            m_gizmoSize = WMath.Clamp(m_gizmoSize - 0.05f, 0.05f, float.MaxValue);
        }

        public void SetPosition(Vector3 position)
        {
            m_position = position;
        }

        public void SetLocalRotation(Quaternion orientation)
        {
            m_localRotation = orientation;
            if (m_transformSpace == FTransformSpace.Local)
                m_rotation = orientation;
        }

        public void SetTransformSpace(FTransformSpace space)
        {
            m_transformSpace = space;
            if (space == FTransformSpace.World)
                m_rotation = Quaternion.Identity;
            else
                m_rotation = m_localRotation;
        }

        public override void Tick(float deltaTime)
        {
            // Update camera distance to our camera.
            if ((!m_isTransforming) || (m_mode != FTransformMode.Translation))
            {
                m_cameraDistance = (WSceneView.GetCameraPos() - m_position).Length; // ToDo: This is still bad.
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
                //m_lineBatcher.DrawBox(gizmoBoxes[i].Min + m_transform.Position, gizmoBoxes[i].Max + m_transform.Position, gizmoColors[i], 25, 0f);
            }

            // Update Highlight Status of Models.
            int gizmoIndex = (int)m_mode - 1;
            if (gizmoIndex >= 0)
            {
                for(int i = 0; i < m_gizmoMeshes[gizmoIndex].Length; i++)
                {
                    FSelectedAxes axis = (FSelectedAxes)(m_mode == FTransformMode.Rotation ? i + 1 : i); // Rotation doesn't have a center, thus it needs an index fixup.
                    m_gizmoMeshes[gizmoIndex][i].Highlighted = ContainsAxis(m_selectedAxes, axis);
                }
            }

            //switch (m_mode)
            //{
            //    case TransformMode.Translation:
            //        Console.WriteLine("m_position: {0} m_deltaTranslation: {1} m_totalTranslation: {2}", m_position, m_deltaTranslation, m_totalTranslation);
            //        break;
            //    case TransformMode.Rotation:
            //        Console.WriteLine("m_rotation: {0} m_localRotation: {1} m_deltaRotation: {2} m_currentRotation: {3} m_totalRotation(UI): {4}", m_rotation, m_localRotation, m_deltaRotation, m_currentRotation, m_totalRotation);
            //        break;
            //    case TransformMode.Scale:
            //        Console.WriteLine("m_scale: {0} m_deltaScale: {1} m_totalScale: {2}", m_scale, m_deltaScale, m_totalScale);
            //        break;
            //    default:
            //        break;
            //}
        }

        public bool CheckSelectedAxes(WRay ray)
        {
            // Convert the ray into local space so we can use axis-aligned checks, this solves the checking problem
            // when the gizmo is rotated due to being in Local mode.
            WRay localRay = new WRay();
            localRay.Direction = Vector3.Transform(ray.Direction, m_rotation.Inverted());
            localRay.Origin = Vector3.Transform(ray.Origin, m_rotation.Inverted()) - m_position;

            //m_lineBatcher.DrawLine(localRay.Origin, localRay.Origin + (localRay.Direction * 10000), WLinearColor.White, 25, 5);
            List<AxisDistanceResult> results = new List<AxisDistanceResult>();

            if (m_mode == FTransformMode.Translation)
            {
                AABox[] translationAABB = GetAABBBoundsForMode(FTransformMode.Translation);
                for (int i = 0; i < translationAABB.Length; i++)
                {
                    float intersectDist;
                    if (WMath.RayIntersectsAABB(localRay, translationAABB[i].Min, translationAABB[i].Max, out intersectDist))
                    {
                        results.Add(new AxisDistanceResult((FSelectedAxes)(i + 1), intersectDist));
                    }
                }
            }
            else if (m_mode == FTransformMode.Rotation)
            {
                // We'll use a combination of AABB and Distance checks to give us the quarter-circles we need.
                AABox[] rotationAABB = GetAABBBoundsForMode(FTransformMode.Rotation);

                float screenScale = 0f;
                for (int i = 0; i < 3; i++) screenScale += m_scale[i];
                screenScale /= 3f;

                for (int i = 0; i < rotationAABB.Length; i++)
                {
                    float intersectDist;
                    if (WMath.RayIntersectsAABB(localRay, rotationAABB[i].Min, rotationAABB[i].Max, out intersectDist))
                    {
                        Vector3 intersectPoint = localRay.Origin + (localRay.Direction * intersectDist);
                        // Convert this aabb check into a radius check so we clip it by the semi-circles
                        // that the rotation tool actually is.
                        if (intersectPoint.Length > 105f * screenScale)
                        {
                            continue;
                        }

                        results.Add(new AxisDistanceResult((FSelectedAxes)(i + 1), intersectDist));
                    }
                }
            }
            else if (m_mode == FTransformMode.Scale)
            {
                AABox[] scaleAABB = GetAABBBoundsForMode(FTransformMode.Scale);
                for (int i = 0; i < scaleAABB.Length; i++)
                {
                    float intersectDist;
                    if (WMath.RayIntersectsAABB(localRay, scaleAABB[i].Min, scaleAABB[i].Max, out intersectDist))
                    {
                        // Special-case here to give the center scale point overriding priority. Because we intersected
                        // it, we can just override its distance to zero to make it clickable through the other bounding boxes.
                        if ((FSelectedAxes)i + 1 == FSelectedAxes.All)
                            intersectDist = 0f;

                        results.Add(new AxisDistanceResult((FSelectedAxes)(i + 1), intersectDist));
                    }
                }
            }

            if (results.Count == 0)
            {
                m_selectedAxes = FSelectedAxes.None;
                return false;
            }

            // If we get an intersection, sort them by the closest intersection distance.
            results.Sort((x, y) => x.Distance.CompareTo(y.Distance));
            m_selectedAxes = results[0].Axis;

            // Store where the mouse hit on the first frame in world space. This means converting the ray back to worldspace.
            Vector3 localHitPoint = localRay.Origin + (localRay.Direction * results[0].Distance);
            m_hitPoint = Vector3.Transform(localHitPoint, m_rotation) + m_position;
            return true;
        }

        public  bool TransformFromInput(WRay ray, Vector3 cameraPos)
        {
            if (m_mode != FTransformMode.Translation)
                WrapCursor();

            // Store the cursor position in viewport coordinates.
            Vector2 screenDimensions = App.GetScreenGeometry();
            Vector2 cursorPos = App.GetCursorPosition();
            Vector2 mouseCoords = new Vector2(((2f * cursorPos.X) / screenDimensions.X) - 1f, (1f - ((2f * cursorPos.Y) / screenDimensions.Y))); //[-1,1] range

            if (m_mode == FTransformMode.Translation)
            {
                // Create a Translation Plane
                Vector3 axisA, axisB;

                if (GetNumSelectedAxes() == 1)
                {
                    if (m_selectedAxes == FSelectedAxes.X)           axisB = Vector3.UnitX;
                    else if (m_selectedAxes == FSelectedAxes.Y)      axisB = Vector3.UnitY;
                    else                                            axisB = Vector3.UnitZ;

                    Vector3 dirToCamera = (m_position - cameraPos).Normalized();
                    axisA = Vector3.Cross(axisB, dirToCamera);
                }
                else
                {
                    axisA = ContainsAxis(m_selectedAxes, FSelectedAxes.X) ? Vector3.UnitX : Vector3.UnitZ;
                    axisB = ContainsAxis(m_selectedAxes, FSelectedAxes.Y) ? Vector3.UnitY : Vector3.UnitZ;
                }

                Vector3 planeNormal = Vector3.Cross(axisA, axisB).Normalized();
                m_translationPlane = new WPlane(planeNormal, m_position);

                float intersectDist;
                if (m_translationPlane.RayIntersectsPlane(ray, out intersectDist))
                {
                    Vector3 hitPos = ray.Origin + (ray.Direction * intersectDist);
                    Vector3 localDelta = Vector3.Transform(hitPos - m_position, m_rotation.Inverted());

                    // Calculate a new position
                    Vector3 newPos = m_position;
                    if (ContainsAxis(m_selectedAxes, FSelectedAxes.X))
                        newPos += Vector3.Transform(Vector3.UnitX, m_rotation) * localDelta.X;
                    if (ContainsAxis(m_selectedAxes, FSelectedAxes.Y))
                        newPos += Vector3.Transform(Vector3.UnitY, m_rotation) * localDelta.Y;
                    if (ContainsAxis(m_selectedAxes, FSelectedAxes.Z))
                        newPos += Vector3.Transform(Vector3.UnitZ, m_rotation) * localDelta.Z;


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
                        m_translateOffset = m_position - newPos;
                        m_deltaTranslation = Vector3.Zero;
                        m_hasSetMouseOffset = true;
                        return false;
                    }

                    // Apply Translation
                    m_deltaTranslation = Vector3.Transform(newPos - m_position + m_translateOffset, m_rotation.Inverted());

                    if (!ContainsAxis(m_selectedAxes, FSelectedAxes.X)) m_deltaTranslation.X = 0f;
                    if (!ContainsAxis(m_selectedAxes, FSelectedAxes.Y)) m_deltaTranslation.Y = 0f;
                    if (!ContainsAxis(m_selectedAxes, FSelectedAxes.Z)) m_deltaTranslation.Z = 0f;

                    m_totalTranslation += m_deltaTranslation;
                    m_position += Vector3.Transform(m_deltaTranslation, m_rotation);

                    if (!m_hasTransformed && (m_deltaTranslation != Vector3.Zero))
                        m_hasTransformed = true;

                    return m_hasTransformed;
                }
                else
                {
                    // Our raycast missed the plane
                    m_deltaTranslation = Vector3.Zero;
                    return false;
                }
            }
            else if (m_mode == FTransformMode.Rotation)
            {
                Vector3 rotationAxis;
                if (m_selectedAxes == FSelectedAxes.X) rotationAxis = Vector3.UnitX;
                else if (m_selectedAxes == FSelectedAxes.Y) rotationAxis = Vector3.UnitY;
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
                rotAmount += m_rotateOffset;
                Quaternion oldRot = m_currentRotation;
                m_currentRotation = Quaternion.FromAxisAngle(rotationAxis, WMath.DegreesToRadians(rotAmount));
                m_deltaRotation = m_currentRotation * oldRot.Inverted();

                if(m_transformSpace == FTransformSpace.Local)
                    m_rotation *= m_deltaRotation;

                // Add to Total Rotation recorded for UI.
                if (m_selectedAxes == FSelectedAxes.X) m_totalRotation.X = rotAmount;
                else if (m_selectedAxes == FSelectedAxes.Y) m_totalRotation.Y = rotAmount;
                else m_totalRotation.Z = rotAmount;

                if (!m_hasTransformed && rotAmount != 0f)
                    m_hasTransformed = true;

                return m_hasTransformed;
            }
            else if (m_mode == FTransformMode.Scale)
            {
                // Create a line in screen space.
                // Convert these from [0-1] to [-1, 1] to match our mouse coords.
                Vector2 lineOrigin = (WSceneView.UnprojectWorldToViewport(m_position) * 2) - Vector2.One;
                lineOrigin.Y = -lineOrigin.Y;

                // Determine the appropriate world space directoin using the selected axes and then conver this for use with
                // screen-space controlls. This has to be done every frame because the axes can be flipped while the gizmo
                // is transforming, so we can't pre-calculate this.
                Vector3 dirX = Vector3.Transform(mFlipScaleX ? -Vector3.UnitX : Vector3.UnitX, m_rotation);
                Vector3 dirY = Vector3.Transform(mFlipScaleY ? -Vector3.UnitY : Vector3.UnitY, m_rotation);
                Vector3 dirZ = Vector3.Transform(mFlipScaleZ ? -Vector3.UnitZ : Vector3.UnitZ, m_rotation);
                Vector2 lineDir;

                // If there is only one axis, then the world space direction is the selected axis.
                if (GetNumSelectedAxes() == 1)
                {
                    Vector3 worldDir;
                    if (ContainsAxis(m_selectedAxes, FSelectedAxes.X)) worldDir = dirX;
                    if (ContainsAxis(m_selectedAxes, FSelectedAxes.Y)) worldDir = dirY;
                    else worldDir = dirZ;

                    Vector2 worldPoint = (WSceneView.UnprojectWorldToViewport(m_position + worldDir) * 2) - Vector2.One;
                    worldPoint.Y = -lineOrigin.Y;

                    lineDir = (worldPoint - lineOrigin).Normalized();
                }
                // If there's two axii selected, then convert both to screen space and average them out to get the line direction.
                else if (GetNumSelectedAxes() == 2)
                {
                    Vector3 axisA = ContainsAxis(m_selectedAxes, FSelectedAxes.X) ? dirX : dirY;
                    Vector3 axisB = ContainsAxis(m_selectedAxes, FSelectedAxes.Z) ? dirZ : dirY;

                    Vector2 screenA = (WSceneView.UnprojectWorldToViewport(m_position + axisA) * 2) - Vector2.One;
                    screenA.Y = -screenA.Y;
                    Vector2 screenB = (WSceneView.UnprojectWorldToViewport(m_position + axisB) * 2) - Vector2.One;
                    screenB.Y = -screenB.Y;

                    screenA = (screenA - lineOrigin).Normalized();
                    screenB = (screenB - lineOrigin).Normalized();
                    lineDir = ((screenA + screenB) / 2f).Normalized();
                }
                // There's three axis, just use up.
                else
                    lineDir = Vector2.UnitY;

                float scaleAmount = Vector2.Dot(lineDir, mouseCoords + m_wrapOffset - lineOrigin) * 5f;

                // Set their initial offset if we haven't already
                if (!m_hasSetMouseOffset)
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

                Vector3 oldScale = m_totalScale;
                m_totalScale = Vector3.One;
                if (ContainsAxis(m_selectedAxes, FSelectedAxes.X)) m_totalScale.X = scaleAmount;
                if (ContainsAxis(m_selectedAxes, FSelectedAxes.Y)) m_totalScale.Y = scaleAmount;
                if (ContainsAxis(m_selectedAxes, FSelectedAxes.Z)) m_totalScale.Z = scaleAmount;

                m_deltaScale = new Vector3(m_totalScale.X / oldScale.X, m_totalScale.Y / oldScale.Y, m_totalScale.Z / oldScale.Z);

                if (!m_hasTransformed && (scaleAmount != 1f))
                    m_hasTransformed = true;

                return m_hasTransformed;
            }

            return false;
        }

        private bool ContainsAxis(FSelectedAxes valToCheck, FSelectedAxes majorAxis)
        {
            switch (majorAxis)
            {
                case FSelectedAxes.X:
                    return valToCheck == FSelectedAxes.X || valToCheck == FSelectedAxes.XY || valToCheck == FSelectedAxes.XZ || valToCheck == FSelectedAxes.All;
                case FSelectedAxes.Y:
                    return valToCheck == FSelectedAxes.Y || valToCheck == FSelectedAxes.XY || valToCheck == FSelectedAxes.YZ || valToCheck == FSelectedAxes.All;
                case FSelectedAxes.Z:
                    return valToCheck == FSelectedAxes.Z || valToCheck == FSelectedAxes.XZ || valToCheck == FSelectedAxes.YZ || valToCheck == FSelectedAxes.All;
                case FSelectedAxes.XY:
                    return valToCheck == FSelectedAxes.XY || valToCheck == FSelectedAxes.All;
                case FSelectedAxes.XZ:
                    return valToCheck == FSelectedAxes.XZ || valToCheck == FSelectedAxes.All;
                case FSelectedAxes.YZ:
                    return valToCheck == FSelectedAxes.YZ || valToCheck == FSelectedAxes.All;
            }

            return false;
        }

        private AABox[] GetAABBBoundsForMode(FTransformMode mode)
        {
            switch (mode)
            {
                case FTransformMode.Translation:
                    float boxLength = 100f;
                    float boxHalfWidth = 5;

                    var translationAABB = new[]
                    {
                        // X Axis
                        new AABox(new Vector3(0, -boxHalfWidth, -boxHalfWidth), new Vector3(boxLength, boxHalfWidth, boxHalfWidth)),
                        // Y Axis
                        new AABox(new Vector3(-boxHalfWidth, 0, -boxHalfWidth), new Vector3(boxHalfWidth, boxLength, boxHalfWidth)),
                        // Z Axis
                        new AABox(new Vector3(-boxHalfWidth, -boxHalfWidth, 0), new Vector3(boxHalfWidth, boxHalfWidth, boxLength)),
                        // XY Axes
                        new AABox(new Vector3(0, 0, -2), new Vector3(boxHalfWidth*6, boxHalfWidth*6, 2)),
                        // YZ Axes
                        new AABox(new Vector3(0, -2, 0), new Vector3(boxHalfWidth*6, 2, boxHalfWidth*6)),
                        // XZ Axes
                        new AABox(new Vector3(-2, 0, 0), new Vector3(2, boxHalfWidth*6, boxHalfWidth*6)),
                    };
                    for (int i = 0; i < translationAABB.Length; i++)
                        translationAABB[i].ScaleBy(m_scale);
                    return translationAABB;

                case FTransformMode.Rotation:
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
                        rotationAABB[i].ScaleBy(m_scale);
                    return rotationAABB;

                case FTransformMode.Scale:
                    float scaleLength = 100f;
                    float scaleHalfWidth = 5;
                    float scaleCornerSize = 38;

                    var scaleAABB = new[]
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
                        scaleAABB[i].ScaleBy(m_scale);
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
            for (int i = 0; i < m_gizmoMeshes.Length; i++)
            {
                for (int j = 0; j < m_gizmoMeshes[i].Length; j++)
                    m_gizmoMeshes[i][j].ReleaseResources();
            }
        }

        public override void Render(Matrix4 viewMatrix, Matrix4 projMatrix)
        {
            m_scale = Vector3.One * m_gizmoSize * (m_cameraDistance / 100f);

            // Construct a model matrix for the gizmo mesh to render at.
            Matrix4 modelMatrix = Matrix4.CreateScale(m_scale) * Matrix4.CreateFromQuaternion(m_rotation) * Matrix4.CreateTranslation(m_position);

            int gizmoIndex = (int)m_mode-1;
            if(gizmoIndex >= 0)
            {
                for (int j = 0; j < m_gizmoMeshes[gizmoIndex].Length; j++)
                {
                    m_gizmoMeshes[gizmoIndex][j].Render(viewMatrix, projMatrix, modelMatrix);
                }
            }
        }
        #endregion
    }
}
