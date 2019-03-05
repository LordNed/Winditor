// 
using GameFormatReader.Common;
using OpenTK;
using System.ComponentModel;
using System.Diagnostics;
using System;
using WindEditor.ViewModel;

namespace WindEditor
{
	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class Foliage : Actor
	{
		// Auto-Generated Properties from Templates
		private int m_Pattern;

		[WProperty("Foliage", "Pattern", true)]
		 public int Pattern
		{ 
			get { return m_Pattern; }
			set
			{
				m_Pattern = value;
				OnPropertyChanged("Pattern");
			}
		}
				

		private int m_Type;

		[WProperty("Foliage", "Type", true)]
		 public int Type
		{ 
			get { return m_Type; }
			set
			{
				m_Type = value;
				OnPropertyChanged("Type");
			}
		}
				

		private int m_ItemDrop;

		[WProperty("Foliage", "Item Drop", true)]
		 public int ItemDrop
		{ 
			get { return m_ItemDrop; }
			set
			{
				m_ItemDrop = value;
				OnPropertyChanged("ItemDrop");
			}
		}
				


		// Constructor
		public Foliage(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}

		// Copy constructor for Actor conversions
		public Foliage(Actor src) : base(src.FourCC, src.World)
		{
			Parameters = src.Parameters;
			Name = src.Name;
			FlagToSet = src.FlagToSet;
			EnemyNumber = src.EnemyNumber;
		}

		override public void PostLoad()
		{
			base.PostLoad();

			Pattern = (Parameters & 15) >> 0;
			Type = (Parameters & 48) >> 4;
			ItemDrop = (Parameters & 4032) >> 6;
		}

		override public void PreSave()
		{
			Parameters = 0;
			Parameters |= (Pattern & 15) << 0;
			Parameters |= (Type & 48) << 4;
			Parameters |= (ItemDrop & 4032) << 6;
		}
	}

} // namespace WindEditor
