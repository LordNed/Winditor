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
        Unused1 = 0x1B,
        Unused2 = 0x1C,
        Unused3 = 0x1D,
        Unused4 = 0x1E,
        Unused5 = 0x1F,
        Multi,
        None
    }

    public enum WallCode
    {
        Normal = 0x0,
        Climbable_Generic = 0x1,
        Wall = 0x2,
        Grabable = 0x3,
        Climbable_Ladder = 0x4,
        Code6 = 0x5,
        Code7 = 0x6,
        Code8 = 0x7,
        Code9 = 0x8,
        Code10 = 0x9,
        Code11 = 0xA,
        Code12 = 0xB,
        Code13 = 0xC,
        Code14 = 0xD,
        Code15 = 0xE,
        Code16 = 0xF,
        Multi,
        None
    }

    public enum SpecialCode
    {
        Normal = 0x0,
        Force_Slide1 = 0x1,
        Force_Slide2 = 0x2,
        No_Sidle = 0x3,
        Code5 = 0x4,
        Code6 = 0x5,
        Code7 = 0x6,
        Code8 = 0x7,
        Code9 = 0x8,
        Code10 = 0x9,
        Code11 = 0xA,
        Code12 = 0xB,
        Code13 = 0xC,
        Code14 = 0xD,
        Code15 = 0xE,
        Code16 = 0xF,
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
        Normal3 = 0x05,
        Lava = 0x06,
        Dirt_Packed = 0x07,
        Respawn_Generic = 0x08,
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
        Water2 = 0x17,
        Glass = 0x18,
        Cloth = 0x19,
        Unused1 = 0x1A,
        Unused2 = 0x1B,
        Unused3 = 0x1C,
        Unused4 = 0x1D,
        Unused5 = 0x1E,
        Unused6 = 0x1F,
        Multi,
        None
    }

    public enum GroundCode
    {
        Normal = 0x00,
        Code2 = 0x01,
        Code3 = 0x02,
        Force_Ledge_Hang = 0x3,
        Respawn_Generic = 0x04,
        Code5 = 0x05,
        Code6 = 0x06,
        Code7 = 0x07,
        Slope = 0x08,
        Code9 = 0x09,
        Code10 = 0x0A,
        Code11 = 0x0B,
        Code12 = 0x0C,
        Code13 = 0x0D,
        Code14 = 0x0E,
        Code15 = 0x0F,
        Code16 = 0x10,
        Code17 = 0x11,
        Code18 = 0x12,
        Code19 = 0x13,
        Code20 = 0x14,
        Code21 = 0x15,
        Code22 = 0x16,
        Code23 = 0x17,
        Code24 = 0x18,
        Code25 = 0x19,
        Code26 = 0x1A,
        Code27 = 0x1B,
        Code28 = 0x1C,
        Code29 = 0x1D,
        Code30 = 0x1E,
        Code31 = 0x1F,
        Multi,
        None
    }
}
