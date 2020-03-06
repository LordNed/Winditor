using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperBMDLib.Materials.Enums
{
    public enum KonstColorSel
    {
        KCSel_1 = 0x00,     // Constant 1.0
        KCSel_7_8 = 0x01,   // Constant 7/8
        KCSel_3_4 = 0x02,   // Constant 3/4
        KCSel_5_8 = 0x03,   // Constant 5/8
        KCSel_1_2 = 0x04,   // Constant 1/2
        KCSel_3_8 = 0x05,   // Constant 3/8
        KCSel_1_4 = 0x06,   // Constant 1/4
        KCSel_1_8 = 0x07,   // Constant 1/8
        KCSel_K0 = 0x0C,    // K0[RGB] Register
        KCSel_K1 = 0x0D,    // K1[RGB] Register
        KCSel_K2 = 0x0E,    // K2[RGB] Register
        KCSel_K3 = 0x0F,    // K3[RGB] Register
        KCSel_K0_R = 0x10,  // K0[RRR] Register
        KCSel_K1_R = 0x11,  // K1[RRR] Register
        KCSel_K2_R = 0x12,  // K2[RRR] Register
        KCSel_K3_R = 0x13,  // K3[RRR] Register
        KCSel_K0_G = 0x14,  // K0[GGG] Register
        KCSel_K1_G = 0x15,  // K1[GGG] Register
        KCSel_K2_G = 0x16,  // K2[GGG] Register
        KCSel_K3_G = 0x17,  // K3[GGG] Register
        KCSel_K0_B = 0x18,  // K0[BBB] Register
        KCSel_K1_B = 0x19,  // K1[BBB] Register
        KCSel_K2_B = 0x1A,  // K2[BBB] Register
        KCSel_K3_B = 0x1B,  // K3[BBB] Register
        KCSel_K0_A = 0x1C,  // K0[AAA] Register
        KCSel_K1_A = 0x1D,  // K1[AAA] Register
        KCSel_K2_A = 0x1E,  // K2[AAA] Register
        KCSel_K3_A = 0x1F   // K3[AAA] Register
    }
}
