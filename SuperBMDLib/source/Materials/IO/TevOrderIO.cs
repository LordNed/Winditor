using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperBMDLib.Materials;
using GameFormatReader.Common;

namespace SuperBMDLib.Materials.IO
{
    public static class TevOrderIO
    {
        public static List<TevOrder> Load(EndianBinaryReader reader, int offset, int size)
        {
            List<TevOrder> orders = new List<TevOrder>();
            int count = size / 4;

            for (int i = 0; i < count; i++)
                orders.Add(new TevOrder(reader));

            return orders;
        }

        public static void Write(EndianBinaryWriter writer, List<TevOrder> orders)
        {
            foreach (TevOrder order in orders)
                order.Write(writer);
        }
    }
}
