using System;
using System.IO;

namespace SuperBMDLib
{
    /// <summary>
    /// Container for arguments taken from the user's input.
    /// </summary>
    public struct Arguments
    {
        public string input_path;
        public string output_path;
        public string materials_path;
        public string texheaders_path;
        public string tristrip_mode;
        public bool rotate_model;
        public bool output_bdl;
        public bool generate_map_materials;

        /// <summary>
        /// Initializes a new Arguments instance from the arguments passed in to SuperBMD.
        /// </summary>
        /// <param name="args">Arguments from the user</param>
        public Arguments(string[] args)
        {
            input_path = "";
            output_path = "";
            materials_path = "";
            texheaders_path = "";
            tristrip_mode = "static";
            rotate_model = false;
            output_bdl = false;
            generate_map_materials = false;

            for (int i = 0; i < args.Length; i++)
            {
                switch (args[i])
                {
                    case "-i":
                    case "--input":
                        if (i + 1 >= args.Length)
                            throw new Exception("The parameters were malformed.");

                        input_path = args[i + 1];
                        i++;
                        break;
                    case "-o":
                    case "--output":
                        if (i + 1 >= args.Length)
                            throw new Exception("The parameters were malformed.");

                        output_path = args[i + 1];
                        i++;
                        break;
                    case "-m":
                    case "--materialPresets":
                        if (i + 1 >= args.Length)
                            throw new Exception("The parameters were malformed.");

                        materials_path = args[i + 1];
                        i++;
                        break;
                    case "-x":
                    case "--texHeaders":
                        if (i + 1 >= args.Length)
                            throw new Exception("The parameters were malformed.");

                        texheaders_path = args[i + 1];
                        i++;
                        break;
                    case "-t":
                    case "--tristrip":
                        if (i + 1 >= args.Length)
                            throw new Exception("The parameters were malformed.");

                        tristrip_mode = args[i + 1].ToLower();
                        i++;
                        break;
                    case "-r":
                    case "--rotate":
                        rotate_model = true;
                        break;
                    case "-b":
                    case "--bdl":
                        output_bdl = true;
                        break;
                    case "-glm":
                        generate_map_materials = true;
                        break;
                    default:
                        throw new Exception($"Unknown parameter \"{ args[i] }\"");
                }
            }

            ValidateArgs();
        }

        /// <summary>
        /// Ensures that all the settings parsed from the user's input are valid.
        /// </summary>
        /// <param name="args">Array of settings parsed from the user's input</param>
        private void ValidateArgs()
        {
            // Input
            if (input_path == "")
                throw new Exception("No input file was specified.");
            if (!File.Exists(input_path))
                throw new Exception($"Input file \"{ input_path }\" does not exist.");

            // Output
            if (output_path == "")
            {
                string input_without_ext = Path.Combine(Path.GetDirectoryName(input_path), Path.GetFileNameWithoutExtension(input_path));

                if (input_path.EndsWith(".bmd") || input_path.EndsWith(".bdl"))
                    output_path = input_without_ext + ".dae";
                else
                    output_path = input_without_ext + ".bmd";
            }

            // Material presets
            if (materials_path != "")
            {
                if (!File.Exists(materials_path))
                    throw new Exception($"Material presets file \"{ materials_path }\" does not exist.");
            }

            // Texture headers
            if (texheaders_path != "")
            {
                if (!File.Exists(texheaders_path))
                    throw new Exception($"Texture headers file \"{ texheaders_path }\" does not exist.");
            }

            // Tristrip options
            if (tristrip_mode != "static" && tristrip_mode != "all" && tristrip_mode != "none")
                throw new Exception($"Unknown tristrip option \"{ tristrip_mode }\".");
        }
    }
}
