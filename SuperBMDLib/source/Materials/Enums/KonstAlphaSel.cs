using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperBMDLib.Materials.Enums
{
    public enum KonstAlphaSel
    {
        KASel_1 = 0x00,     // Constant 1.0
        KASel_7_8 = 0x01,   // Constant 7/8
        KASel_3_4 = 0x02,   // Constant 3/4
        KASel_5_8 = 0x03,   // Constant 5/8
        KASel_1_2 = 0x04,   // Constant 1/2
        KASel_3_8 = 0x05,   // Constant 3/8
        KASel_1_4 = 0x06,   // Constant 1/4
        KASel_1_8 = 0x07,   // Constant 1/8
        KASel_K0_R = 0x10,  // K0[R] Register
        KASel_K1_R = 0x11,  // K1[R] Register
        KASel_K2_R = 0x12,  // K2[R] Register
        KASel_K3_R = 0x13,  // K3[R] Register
        KASel_K0_G = 0x14,  // K0[G] Register
        KASel_K1_G = 0x15,  // K1[G] Register
        KASel_K2_G = 0x16,  // K2[G] Register
        KASel_K3_G = 0x17,  // K3[G] Register
        KASel_K0_B = 0x18,  // K0[B] Register
        KASel_K1_B = 0x19,  // K1[B] Register
        KASel_K2_B = 0x1A,  // K2[B] Register
        KASel_K3_B = 0x1B,  // K3[B] Register
        KASel_K0_A = 0x1C,  // K0[A] Register
        KASel_K1_A = 0x1D,  // K1[A] Register
        KASel_K2_A = 0x1E,  // K2[A] Register
        KASel_K3_A = 0x1F   // K3[A] Register
    }
}
