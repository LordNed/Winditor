using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WindEditor.Minitors.Text
{
    public class MessageTextValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (!(value is string))
                return new ValidationResult(false, "");

            string message = value as string;

            for (int i = 0; i < message.Length; i++)
            {
                if (message[i] == '[')
                {
                    List<char> tag_chars = new List<char>();

                    for (int j = i + 1; j < message.Length; j++)
                    {
                        if (message[j] == ']')
                        {
                            break;
                        }
                        else if (j == message.Length - 1)
                        {
                            return new ValidationResult(false, "Control tag error: Opening bracket with no closing bracket");
                        }

                        tag_chars.Add(message[j]);
                    }

                    string full_tag = new string(tag_chars.ToArray());

                    if (!ValidateControlTag(full_tag))
                    {
                        return new ValidationResult(false, $"Control tag error: Unknown tag { full_tag }");
                    }

                    i += full_tag.Length + 1;
                }
                else if (message[i] == ']')
                {
                    return new ValidationResult(false, "Control tag error: Closing bracket with no opening bracket");
                }
                else
                {
                    continue;
                }
            }

            return new ValidationResult(true, "");
        }

        private bool ValidateControlTag(string tag)
        {
            if (tag.Contains('='))
            {
                string[] split_tag = tag.Split('=');

                if (split_tag.Length < 2 || split_tag[1] == "")
                {
                    return false;
                }

                ushort tag_arg = Convert.ToUInt16(split_tag[1]);
                byte[] tag_arg_bytes = BitConverter.GetBytes(tag_arg);

                if (Enum.TryParse(split_tag[0], out SevenByteCode seven_result))
                {
                    return true;
                }
                else if (split_tag[0] == "sound")
                {
                    return true;
                }
                else if (split_tag[0] == "camera")
                {
                    return true;
                }
                else if (split_tag[0] == "animation")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if (Enum.TryParse(tag, out FiveByteCode five_result))
            {
                return true;
            }
            else if (Enum.TryParse(tag, out TextColor color_result))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
