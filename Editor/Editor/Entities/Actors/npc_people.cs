using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindEditor.ViewModel;

namespace WindEditor
{
	public partial class npc_people
	{
		[WProperty("Misc.", "Name", true, "")]
		override public string Name
		{
			get { return m_Name; }
			set
			{
				m_Name = value;
				OnPropertyChanged("Name");
				UpdateModel();
			}
		}

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
			m_actorMeshes.Clear();
			m_objRender = null;
			bool isNight = false;
			int layerNum = (int)Layer - 1;
			if (layerNum >= 0 && layerNum % 2 == 1)
				isNight = true;
			switch (Name)
			{
				case "Uo1":
					m_actorMeshes = WResourceManager.LoadActorResource("Sam");
					break;
				case "Uo2":
					m_actorMeshes = WResourceManager.LoadActorResource("Gossack");
					break;
				case "Uo3":
					m_actorMeshes = WResourceManager.LoadActorResource("Garrickson");
					break;
				case "Ub1":
					m_actorMeshes = WResourceManager.LoadActorResource("Vera");
					break;
				case "Ub2":
					m_actorMeshes = WResourceManager.LoadActorResource("Pompie");
					break;
				case "Ub3":
					m_actorMeshes = WResourceManager.LoadActorResource("Missy");
					break;
				case "Ub4":
					m_actorMeshes = WResourceManager.LoadActorResource("Minenco");
					break;
				case "Uw1":
					m_actorMeshes = WResourceManager.LoadActorResource("Gillian");
					break;
				case "Uw2":
					m_actorMeshes = WResourceManager.LoadActorResource("Linda");
					break;
				case "Um1":
					m_actorMeshes = WResourceManager.LoadActorResource("Kreeb");
					break;
				case "Um2":
					m_actorMeshes = WResourceManager.LoadActorResource("Anton");
					break;
				case "Um3":
					if (isNight)
						m_actorMeshes = WResourceManager.LoadActorResource("Nighttime Kamo");
					else
						m_actorMeshes = WResourceManager.LoadActorResource("Daytime Kamo");
					break;
				case "Sa1":
					if (isNight)
						m_actorMeshes = WResourceManager.LoadActorResource("Nighttime Loot");
					else
						m_actorMeshes = WResourceManager.LoadActorResource("Daytime Loot");
					break;
				case "Sa2":
					m_actorMeshes = WResourceManager.LoadActorResource("Gummy");
					break;
				case "Sa3":
					if (isNight)
						m_actorMeshes = WResourceManager.LoadActorResource("Nighttime Kane");
					else
						m_actorMeshes = WResourceManager.LoadActorResource("Daytime Kane");
					break;
				case "Sa4":
					if (isNight)
						m_actorMeshes = WResourceManager.LoadActorResource("Nighttime Candy");
					else
						m_actorMeshes = WResourceManager.LoadActorResource("Daytime Candy");
					break;
				case "Sa5":
					if (isNight)
						m_actorMeshes = WResourceManager.LoadActorResource("Nighttime Dampa");
					else
						m_actorMeshes = WResourceManager.LoadActorResource("Daytime Dampa");
					break;
				case "Ug1":
					m_actorMeshes = WResourceManager.LoadActorResource("Potova");
					break;
				case "Ug2":
					m_actorMeshes = WResourceManager.LoadActorResource("Joanna");
					break;
				default:
					m_objRender = WResourceManager.LoadObjResource("resources/editor/EditorCube.obj", new OpenTK.Vector4(1f, 1f, 1f, 1f));
					break;
			}
		}
	}
}
