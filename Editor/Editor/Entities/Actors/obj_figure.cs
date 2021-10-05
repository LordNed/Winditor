using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WArchiveTools;
using WArchiveTools.FileSystem;
using WindEditor.ViewModel;
using OpenTK;
using GameFormatReader.Common;
using WArchiveTools.Compression;

namespace WindEditor
{
	public partial class obj_figure
	{
		#region Constants
		private const int l_figure_dat_tbl_offset = 0x24A8;

		private static readonly string[] l_arcname_tbl =
		{
			"Figure",
			"Figure2",
			"Figure1",
			"Figure0",
			"Figure6",
			"Figure5",
			"Figure3",
			"Figure4",
			"Figure2a",
			"Figure2b",
			"Figure6a",
			"Figure6b",
			"Figure6c",
		};

		private const long l_CharaData_address = 0x8035ABE4;
		#endregion

		public override void PostLoad()
		{
			base.PostLoad();
			UpdateModel();
		}

		public override void PreSave()
		{

		}

		private void UpdateModel()
        {
			// Loads the necessary information about which figurine model to load dynamically from the REL and the DOL.

			m_actorMeshes = WResourceManager.LoadActorResource("Figurine Stand");

			int modelFileID;
			int modelArcIndex;

			int figurineIndex = (int)WhichFigurine;

			string figurine_rel_path = Path.Combine(WSettingsManager.GetSettings().RootDirectoryPath, "files", "rels/d_a_obj_figure.rel");
			MemoryStream figurine_rel_data = null;
			using (EndianBinaryReader reader = new EndianBinaryReader(File.ReadAllBytes(figurine_rel_path), Endian.Big))
			{
				figurine_rel_data = Yaz0.Decode(reader);
			}
			using (EndianBinaryReader reader = new EndianBinaryReader(figurine_rel_data, Endian.Big))
			{
				int l_figure_dat_entry_offset = l_figure_dat_tbl_offset + figurineIndex * 0xC;
				modelFileID = reader.ReadInt32At(l_figure_dat_entry_offset + 0x00);
				modelArcIndex = reader.ReadInt32At(l_figure_dat_entry_offset + 0x08);
			}

			if (modelArcIndex == -1)
			{
				string main_dol_path = Path.Combine(WSettingsManager.GetSettings().RootDirectoryPath, "sys", "main.dol");
				using (FileStream strm = new FileStream(main_dol_path, FileMode.Open, FileAccess.Read))
				{
					EndianBinaryReader reader = new EndianBinaryReader(strm, Endian.Big);
					long l_CharaData_entry_address = l_CharaData_address + figurineIndex * 0x12;
					long l_CharaData_entry_offset = DOL.AddressToOffset(l_CharaData_entry_address, reader);
					modelArcIndex = reader.ReadByteAt(l_CharaData_entry_offset + 0x10);
				}
			}

			var arc_name = l_arcname_tbl[modelArcIndex];
			string arc_path = Path.Combine(WSettingsManager.GetSettings().RootDirectoryPath, "files", "res/Object/", arc_name + ".arc");
			if (File.Exists(arc_path))
			{
				VirtualFilesystemDirectory model_arc = ArchiveUtilities.LoadArchive(arc_path);
				if (model_arc.FindByID((ushort)modelFileID) != null)
				{
					var figurine_model = WResourceManager.LoadModelFromVFS(model_arc, fileID: (ushort)modelFileID);
					figurine_model.SetOffsetTranslation(new Vector3(0, 100, 0));
					m_actorMeshes.Add(figurine_model);
				}
			}
		}
	}
}
