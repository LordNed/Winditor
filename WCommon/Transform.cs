using OpenTK;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;

namespace WindEditor
{
    public class WTransform : INotifyPropertyChanged
    {
        // WPF
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary> The parent of this transform that this transform moves locally to. null if a root object. </summary>
        [JsonIgnore]
        public WTransform Parent
        {
            get { return m_parent; }
            set
            {
                // Remove ourself from our current parents children (if we have any)
                if (m_parent != null)
                {
                    m_parent.m_children.Remove(this);
                }

                // Then assign our new parent, and add ourself to that new parent's children (if non null)
                m_parent = value;

                if (m_parent != null)
                {
                    m_parent.m_children.Add(this);
                }
            }
        }

        [JsonIgnore]
        public BindingVector3 PositionBase { get; set; }
        [JsonIgnore]
        public BindingVector3 RotationBase { get; set; }
        [JsonIgnore]
        public BindingVector3 ScaleBase { get; set; }

        [JsonIgnore]
        public bool UsesXRotation = true;
        [JsonIgnore]
        public bool UsesYRotation = true;
        [JsonIgnore]
        public bool UsesZRotation = true;
        [JsonIgnore]
        public string RotationOrder = "ZYX";

        /// <summary> Position of the object in global space. </summary>
        public Vector3 Position
        {
            get
            {
                // Walk the tree until there's no more parents and add up our local position distributed across all.
                Vector3 transformedPoint = LocalPosition;
                WTransform curParent = m_parent;
                while (curParent != null)
                {
                    transformedPoint = (Vector3)Vector3d.Transform((Vector3d)transformedPoint, curParent.LocalRotation);
                    transformedPoint += curParent.LocalPosition;

                    curParent = curParent.m_parent;
                }

                return transformedPoint;
            }
            set
            {
                Vector3 transformedPoint = value;
                WTransform curParent = m_parent;
                while (curParent != null)
                {
                    // Calculate the inverse of the parent's rotation so we can subtract that rotation.
                    Quaterniond inverseRot = curParent.LocalRotation.Inverted();

                    transformedPoint -= curParent.LocalPosition;
                    transformedPoint = (Vector3)Vector3d.Transform((Vector3d)transformedPoint, inverseRot);

                    curParent = curParent.m_parent;
                }

                LocalPosition = transformedPoint;

                OnPropertyChanged("Position");
            }
        }

        /// <summary> Rotation of the object in global space. </summary>
        public Quaterniond Rotation
        {
            get
            {
                // Walk the tree until no more parents adding up the local rotations of each.
                Quaterniond transformedRot = LocalRotation;
                WTransform curParent = m_parent;
                while (curParent != null)
                {
                    transformedRot = curParent.LocalRotation * transformedRot;

                    curParent = curParent.m_parent;
                }

                return transformedRot;
            }
            set
            {
                Quaterniond transformedRot = value;
                WTransform curParent = m_parent;
                while (curParent != null)
                {
                    // Calculate the inverse of the parents rotation so we can subtract that rotation.
                    Quaterniond inverseRot = curParent.LocalRotation.Inverted();
                    transformedRot = inverseRot * transformedRot;

                    curParent = curParent.m_parent;
                }

                // Note: We have to set the RotationBase BindingVector3 *before* the m_localRotation quaternion.
                // If we set the BindingVector3 afterwards, its property change notification would overwrite the quaternion with a conversion.
                RotationBase.BackingVector = transformedRot.ToIdealEulerAngles(RotationOrder, UsesXRotation, UsesYRotation, UsesZRotation);
                m_localRotation = transformedRot;

                OnPropertyChanged("Rotation");
            }
        }

        public Vector3 RotationAsIdealEulerAngles()
        {
            return Rotation.ToIdealEulerAngles(RotationOrder, UsesXRotation, UsesYRotation, UsesZRotation);
        }

        /// <summary> Local scale of the object. Global scale is not supported. </summary>
        public Vector3 LocalScale
        {
            get { return ScaleBase.BackingVector; }
            set
            {
                ScaleBase.BackingVector = value;
                OnPropertyChanged("ScaleBase");
            }
        }

        /// <summary> Local position relative to the parent. Equivelent to Position if parent is null. </summary>
        [JsonIgnore]
        public Vector3 LocalPosition
        {
            get { return PositionBase.BackingVector; }
            set
            {
                PositionBase.BackingVector = value;
                OnPropertyChanged("PositionBase");
            }
        }

        /// <summary> Local rotation relative to the parent. Equivelent to Rotation if parent is null. </summary>
        [JsonIgnore]
        public Quaterniond LocalRotation
        {
            get { return m_localRotation; }
            set
            {
                m_localRotation = value;
            }
        }

        /// <summary> The number of children in this transform. </summary>
        [JsonIgnore]
        public int ChildCount
        {
            get { return m_children.Count; }
        }

        /// <summary> The local up axis of this object as a direction in world space. </summary>
        [JsonIgnore]
        public Vector3 Up
        {
            get { return (Vector3)Vector3d.Transform(Vector3d.UnitY, Rotation); }
        }

        /// <summary> The local right axis of this object as a direction in world space. </summary>
        [JsonIgnore]
        public Vector3 Right
        {
            get { return (Vector3)Vector3d.Transform(Vector3d.UnitX, Rotation); }
        }

        /// <summary> The local forward axis of this object as a direction in world space. </summary>
        [JsonIgnore]
        public Vector3 Forward
        {
            get { return (Vector3)Vector3d.Transform(Vector3d.UnitZ, Rotation); }
        }

        /// <summary>
        /// Returns a child transform by the specified index. Throws <see cref="ArgumentOutOfRangeException"/> if an invalid child index is specified. </summary>
        /// <param name="index">Index of the child to return. Get total child count via <see cref="ChildCount"/>.</param>
        /// <returns></returns>
        public WTransform GetChild(int index)
        {
            if (index < 0 || index >= ChildCount)
                throw new ArgumentOutOfRangeException("index");

            return m_children[index];
        }

        private WTransform m_parent;
        //private Vector3 m_localPosition;
        //private Vector3 m_localScale;
        private Quaterniond m_localRotation;

        /// <summary> Children of this transform. It's marked as private and only accessible via GetEnumerator as we don't want people arbitrarily adding children. </summary>
        private List<WTransform> m_children;

        public WTransform()
        {
            PositionBase = new BindingVector3();
            RotationBase = new BindingVector3();
            ScaleBase = new BindingVector3();
            LocalPosition = Vector3.Zero;
            LocalRotation = Quaterniond.Identity;
            LocalScale = Vector3.One;
            Parent = null;

            m_children = new List<WTransform>();
            
            RotationBase.PropertyChanged += OnRotationBasePropertyChanged;
        }

        public void Rotate(Vector3 axis, float angleInDegrees)
        {
            Quaterniond rotQuat = Quaterniond.FromAxisAngle((Vector3d)axis, WMath.DegreesToRadians(angleInDegrees));
            Rotation = rotQuat * Rotation;
        }

        private void OnRotationBasePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            // Automatically update the quaternion rotation any time the euler rotation base is changed.
            m_localRotation = Quaterniond.Identity.FromEulerAnglesRobust(RotationBase.BackingVector, RotationOrder, UsesXRotation, UsesYRotation, UsesZRotation);
        }

        //public IEnumerator GetEnumerator()
        //{
        //    foreach (WTransform trns in m_children)
        //    {
        //        yield return trns;
        //    }
        //}

        public override bool Equals(object o)
        {
            WTransform otherTrns = o as WTransform;
            if (otherTrns == null)
                return false;

            return Position == otherTrns.Position && Rotation == otherTrns.Rotation && LocalScale == otherTrns.LocalScale;
        }

        public override int GetHashCode()
        {
            return Position.GetHashCode() ^ Rotation.GetHashCode();
        }

        public override string ToString()
        {
            return string.Format("Transform (Position: {0} Rotation: {1} LocalScale: {2})", Position, Rotation, LocalScale);
        }

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}