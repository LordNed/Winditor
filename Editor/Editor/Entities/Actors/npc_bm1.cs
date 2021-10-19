using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindEditor.ViewModel;

namespace WindEditor
{
	public partial class npc_bm1
	{
		public override void PostLoad()
		{
			UpdateModel();
			base.PostLoad();

            TypeSpecificCategories["Name"] = new Dictionary<object, string[]>();
            TypeSpecificCategories["Name"]["Bm1"] = new string[] { "Quill (Bm1)" };
            TypeSpecificCategories["Name"]["Bm2"] = new string[] { "Skett and Akoot (Bm2)" };
            TypeSpecificCategories["Name"]["Bm3"] = new string[] { "Basht and Bisht and Hoskit (Bm3)" };
            TypeSpecificCategories["Name"]["Bm4"] = new string[] { "Ilari and Pashli (Bm4)" };
            TypeSpecificCategories["Name"]["Bm5"] = new string[] { "Namali and Kogoli (Bm5)" };
		}

		public override void PreSave()
		{

		}

		private void UpdateModel()
		{
			m_actorMeshes.Clear();
			m_objRender = null;
			switch (Name)
			{
				case "Bm1":
					m_actorMeshes = WResourceManager.LoadActorResource("Quill");
					break;
				case "Bm2":
					m_actorMeshes = WResourceManager.LoadActorResource("Skett and Akoot");
					break;
				case "Bm3":
					if (BashtandBishtandHoskitType <= BashtandBishtandHoskitTypeEnum.Bisht)
					{
						m_actorMeshes = WResourceManager.LoadActorResource("Basht and Bisht");
					} else if (BashtandBishtandHoskitType == BashtandBishtandHoskitTypeEnum.Hoskit)
					{
						m_actorMeshes = WResourceManager.LoadActorResource("Hoskit");
					} else
					{
						m_objRender = WResourceManager.LoadObjResource("resources/editor/EditorCube.obj", new OpenTK.Vector4(1f, 1f, 1f, 1f));
					}
					break;
				case "Bm4":
					if (IlariandPashliType <= IlariandPashliTypeEnum.Ilari_2)
					{
						m_actorMeshes = WResourceManager.LoadActorResource("Ilari");
					}
					else if (IlariandPashliType == IlariandPashliTypeEnum.Pashli)
					{
						m_actorMeshes = WResourceManager.LoadActorResource("Pashli");
					}
					else
					{
						m_objRender = WResourceManager.LoadObjResource("resources/editor/EditorCube.obj", new OpenTK.Vector4(1f, 1f, 1f, 1f));
					}
					break;
				case "Bm5":
					if (NamaliandKogoliType == NamaliandKogoliTypeEnum.Namali)
					{
						m_actorMeshes = WResourceManager.LoadActorResource("Namali");
					}
					else if (NamaliandKogoliType == NamaliandKogoliTypeEnum.Kogoli)
					{
						m_actorMeshes = WResourceManager.LoadActorResource("Kogoli");
					}
					else
					{
						m_objRender = WResourceManager.LoadObjResource("resources/editor/EditorCube.obj", new OpenTK.Vector4(1f, 1f, 1f, 1f));
					}
					break;
				default:
					m_objRender = WResourceManager.LoadObjResource("resources/editor/EditorCube.obj", new OpenTK.Vector4(1f, 1f, 1f, 1f));
					break;
			}
		}
	}
}
