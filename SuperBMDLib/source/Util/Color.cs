using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace SuperBMDLib.Util
{
    public struct Color
    {
        /// <summary> Red component of the <see cref="Color"/>. </summary>
        public float R;

        /// <summary> Green component of the <see cref="Color"/>. </summary>
        public float G;

        /// <summary> Blue component of the <see cref="Color"/>. </summary>
        public float B;

        /// <summary> Alpha component of the <see cref="Color"/>. Defaults to 1f. </summary>
        public float A;

        /// <summary>
        /// Construct new <see cref="Color"/> with the given R, G, B, and A components.
        /// </summary>
        /// <param name="r">Red Component.</param>
        /// <param name="g">Green Component.</param>
        /// <param name="b">Blue Component.</param>
        /// <param name="a">Alpha Component.</param>
        public Color(float r, float g, float b, float a)
        {
            R = r;
            G = g;
            B = b;
            A = a;
        }

        /// <summary>
        /// Construct a <see cref="Color"/> with the given R, G, B, and an implicit A component set to 1f.
        /// </summary>
        /// <param name="r">Red Component.</param>
        /// <param name="g">Green Component.</param>
        /// <param name="b">Blue Component.</param>
        public Color(float r, float g, float b)
        {
            R = r;
            G = g;
            B = b;
            A = 1f;
        }

        #region Implicit Conversions
        /// <summary>
        /// Convert a <see cref="Color32"/> to a <see cref="Color"/>
        /// </summary>
        public static implicit operator Color(Color32 color32)
        {
            return new Color(color32.R / 255f, color32.G / 255f, color32.B / 255f, color32.A / 255f);
        }

        /// <summary>
        /// Convert a <see cref="Color24"/> to a <see cref="Color"/> with a opaque (1f) <see cref="A"/> channel.
        /// </summary>
        public static implicit operator Color(Color24 color24)
        {
            return new Color(color24.R / 255f, color24.G / 255f, color24.B / 255f, 1f);
        }

        /// <summary>
        /// Convert this <see cref="Color"/> to a <see cref="Color32"/>.
        /// </summary>
        public static implicit operator Color32(Color color)
        {
            return new Color32((byte)(color.R * 255), (byte)(color.G * 255), (byte)(color.B * 255), (byte)(color.A * 255));
        }

        public float this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0: return R;
                    case 1: return G;
                    case 2: return B;
                    case 3: return A;
                    default: throw new ArgumentOutOfRangeException("index", "Invalid Color Index!");
                }
            }
            set
            {
                switch (index)
                {
                    case 0: R = value; break;
                    case 1: G = value; break;
                    case 2: B = value; break;
                    case 3: A = value; break;
                    default: throw new ArgumentOutOfRangeException("index", "Invalid Color Index!");
                }
            }
        }

        public static bool operator ==(Color lhs, Color rhs)
        {
            return lhs.R == rhs.R && lhs.G == rhs.G && lhs.B == rhs.B && lhs.A == rhs.A;
        }

        public static bool operator !=(Color lhs, Color rhs)
        {
            return !(lhs == rhs);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Color))
                return false;

            Color other = (Color)obj;
            return this == other;
        }

        public override int GetHashCode()
        {
            return ((byte)(R * 255) << 24 | (byte)(R * 255) << 16 | (byte)(G * 255) << 8 | (byte)(A * 255));
        }

        public static Color FromHexString(string hexString)
        {
            if (string.IsNullOrEmpty(hexString))
                throw new ArgumentException("Empty/Null hex string!", "hexString");

            if (hexString.StartsWith("0x"))
                hexString = hexString.Substring(2);

            uint colorVal;
            bool bSuccess = uint.TryParse(hexString, NumberStyles.HexNumber, CultureInfo.CurrentCulture, out colorVal);
            if (!bSuccess)
                throw new ArgumentException("Not a valid hex number!", "hexString");

            Color outputColor = new Color();
            byte[] bytes = BitConverter.GetBytes(colorVal);
            for (int i = 0; i < 4; i++)
                outputColor[i] = bytes[i] / 255f;

            // This code is untested, comment this out once you've tested it.
            throw new NotImplementedException();
            //return outputColor;
        }
        #endregion

        public override string ToString()
        {
            return string.Format("RGBA({0:F3}, {1:F3}, {2:F3}, {3, F3})", R, G, B, A);
        }

        public string ToHexString()
        {
            StringBuilder sb = new StringBuilder(10);
            sb.Append("0x");
            sb.Append(R.ToString("X2"));
            sb.Append(G.ToString("X2"));
            sb.Append(B.ToString("X2"));
            sb.Append(A.ToString("X2"));
            return sb.ToString();
        }

        public Assimp.Color4D ToColor4D()
        {
            return new Assimp.Color4D(R, G, B, A);
        }

        public static Color Lerp(Color a, Color b, float t)
        {
            //t = MathE.ClampNormalized(t);
            return new Color(a.R + (b.R - a.R) * t, a.G + (b.G - a.G) * t, a.B + (b.B - a.B) * t, a.A + (b.A - a.A) * t);
        }

        #region Static Preset Colors
        public static Color White
        {
            get { return new Color(1f, 1f, 1f, 1f); }
        }

        public static Color Black
        {
            get { return new Color(0f, 0f, 0f, 1f); }
        }

        public static Color Red
        {
            get { return new Color(1f, 0f, 0f, 1f); }
        }

        public static Color Green
        {
            get { return new Color(0f, 1f, 0f, 1f); }
        }

        public static Color Blue
        {
            get { return new Color(0f, 0f, 1f, 1f); }
        }

        public static Color Orange
        {
            get { return new Color(1f, 0.647f, 0f, 1f); }
        }

        public static Color Coral
        {
            get { return new Color(1f, 0.5f, 0.314f, 1f); }
        }

        public static Color Yellow
        {
            get { return new Color(1f, 1f, 0f, 1f); }
        }

        public static Color Purple
        {
            get { return new Color(0.542f, 0.169f, 0.886f, 1f); }
        }

        public static Color Seagreen
        {
            get { return new Color(0.18f, 0.545f, 341f, 1f); }
        }

        public static Color Pink
        {
            get { return new Color(1f, 0.412f, 0.71f, 1f); }
        }

        public static Color DarkGrey
        {
            get { return new Color(0.4f, 0.4f, 0.4f, 1f); }
        }

        public static Color Grey
        {
            get { return new Color(0.7f, 0.7f, 0.7f, 1f); }
        }

        public static Color Transparent
        {
            get { return new Color(0f, 0f, 0f, 0f); }
        }
        #endregion
    }

    public struct Color24
    {
        public byte R, G, B;

        public Color24(byte r, byte g, byte b)
        {
            R = r;
            G = g;
            B = b;
        }

        public override string ToString()
        {
            return string.Format("[Color24] (r: {0} g: {1} b: {2})", R, G, B);
        }

        public byte this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0:
                        return R;
                    case 1:
                        return G;
                    case 2:
                        return B;

                    default:
                        throw new ArgumentOutOfRangeException("index");
                }
            }
            set
            {
                switch (index)
                {
                    case 0:
                        R = value;
                        break;

                    case 1:
                        G = value;
                        break;

                    case 2:
                        B = value;
                        break;

                    default:
                        throw new ArgumentOutOfRangeException("index");
                }
            }
        }
    }
}
