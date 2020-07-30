using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindEditor.Collision
{
    public enum SoundID
    {
        Normal = 0x00,
        Dirt = 0x01,
        Stone_Generic = 0x02,
        Grass = 0x03,
        Carpet_1 = 0x04,
        Carpet_2 = 0x05,
        Dry_Leaves = 0x06,
        Wood_Soft = 0x07,
        Wood_1 = 0x08,
        Wood_Planks = 0x09,
        Snowy = 0x0A,
        Wood_Hard = 0x0B,
        Wood_Solid = 0x0C,
        Stone_Loud = 0x0D,
        Stone_Dull = 0x0E,
        Rope = 0x0F,
        Wood_Rickety = 0x10,
        Metal_Generic = 0x11,
        Metal_Grate = 0x12,
        Water_Generic = 0x13,
        Water_Deep = 0x14,
        Sand = 0x15,
        Water_Puddle = 0x16,
        Lava = 0x17,
        Water_Shallow = 0x18,
        Glass = 0x19,
        Cloth = 0x1A,
        Unused_1 = 0x1B,
        Unused_2 = 0x1C,
        Unused_3 = 0x1D,
        Unused_4 = 0x1E,
        Unused_5 = 0x1F,
        Multi,
        None
    }

    public enum WallCode
    {
        Normal = 0x0,
        Climbable_Generic = 0x1,
        Wall = 0x2,
        Grabbable = 0x3,
        Climbable_Ladder = 0x4,
        Ladder_Top = 0x5,
        Unknown_6 = 0x6,
        Unknown_7 = 0x7,
        Unknown_8 = 0x8,
        Unknown_9 = 0x9,
        Unknown_10 = 0xA,
        Unknown_11 = 0xB,
        Unknown_12 = 0xC,
        Unknown_13 = 0xD,
        Unknown_14 = 0xE,
        Unknown_15 = 0xF,
        Multi,
        None
    }

    public enum SpecialCode
    {
        Normal = 0x0,
        Force_Slide_1 = 0x1,
        Force_Slide_2 = 0x2,
        No_Sidle = 0x3,
        Unknown_4 = 0x4,
        Unknown_5 = 0x5,
        Unknown_6 = 0x6,
        Unknown_7 = 0x7,
        Unknown_8 = 0x8,
        Unknown_9  = 0x9,
        Unknown_10 = 0xA,
        Unknown_11 = 0xB,
        Unknown_12 = 0xC,
        Unknown_13 = 0xD,
        Unknown_14 = 0xE,
        Unknown_15 = 0xF,
        Multi,
        None
    }

    public enum AttributeCode
    {
        Normal = 0x00,
        Dirt = 0x01,
        Wood = 0x02,
        Stone = 0x03,
        Grass = 0x04,
        Giant_Flower = 0x05,
        Lava = 0x06,
        Dirt_Packed = 0x07,
        Respawn_Generic = 0x08, // Unused
        Damage_Generic = 0x09,
        Carpet = 0x0A,
        Sand = 0x0B,
        Wood_Padded = 0x0C,
        Tree = 0x0D,
        Vine = 0x0E,
        Ice = 0x0F,
        Wood_Hollow = 0x10,
        Metal_Grate = 0x11,
        Water_Ocean = 0x12,
        Water = 0x13,
        Metal = 0x14,
        Respawn_Frozen = 0x15,
        Damage_Electricity = 0x16,
        Waterfall_Base = 0x17,
        Glass = 0x18,
        Cloth = 0x19,
        Books = 0x1A, // Only used in Mesa's house
        Unused_27 = 0x1B,
        Unused_28 = 0x1C,
        Unused_29 = 0x1D,
        Unused_30 = 0x1E,
        Unused_31 = 0x1F,
        Multi,
        None
    }

    public enum GroundCode
    {
        Normal = 0x00,
        Unknown_1 = 0x01,
        Unknown_2 = 0x02,
        Force_Ledge_Hang = 0x03,
        Respawn_Generic = 0x04,
        Unknown_5 = 0x05,
        Unknown_6 = 0x06,
        Unknown_7 = 0x07,
        Slope = 0x08,
        Unknown_9 = 0x09,
        Unknown_10 = 0x0A,
        Unknown_11 = 0x0B,
        Unknown_12 = 0x0C,
        Unknown_13 = 0x0D,
        Unknown_14 = 0x0E,
        Unknown_15 = 0x0F,
        Unknown_16 = 0x10,
        Unknown_17 = 0x11,
        Unknown_18 = 0x12,
        Unknown_19 = 0x13,
        Unknown_20 = 0x14,
        Unknown_21 = 0x15,
        Unknown_22 = 0x16,
        Unknown_23 = 0x17,
        Unknown_24 = 0x18,
        Unknown_25 = 0x19,
        Unknown_26 = 0x1A,
        Unknown_27 = 0x1B,
        Unknown_28 = 0x1C,
        Unknown_29 = 0x1D,
        Unknown_30 = 0x1E,
        Unknown_31 = 0x1F,
        Multi,
        None
    }
}
