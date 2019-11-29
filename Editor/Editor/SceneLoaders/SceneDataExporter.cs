using GameFormatReader.Common;
using System.Collections.Generic;
using System;
using System.IO;
using Newtonsoft.Json;
using OpenTK;

namespace WindEditor.a
{
    public class SceneDataExporter
    {
        public SceneDataExporter()
        {
        }

        public void ExportToStream(EndianBinaryWriter writer, WScene scene)
        {
            // Build a dictionary which lists unique FourCC's and a list of all relevant actors.
            var actorCategories = new Dictionary<FourCC, List<WDOMEntityNode>>();
            var actorsToSort = scene.GetChildrenOfType<WDOMEntityNode>();

            foreach (var node in actorsToSort)
            {
                if (!actorCategories.ContainsKey(node.FourCC))
                    actorCategories[node.FourCC] = new List<WDOMEntityNode>();

                actorCategories[node.FourCC].Add(node);
            }

            // Create a chunk header for each one.
            var chunkHeaders = new List<ChunkHeader>();
            foreach(var kvp in actorCategories)
            {
                ChunkHeader header = new ChunkHeader();
                header.FourCC = kvp.Key;
                header.ElementCount = kvp.Value.Count;

                chunkHeaders.Add(header);
            }

            long chunkStart = writer.BaseStream.Position;

            // Write the Header
            writer.Write(chunkHeaders.Count);
            for (int i = 0; i < chunkHeaders.Count; i++)
            {
                writer.Write((int)0); // Dummy Placeholder values for the Chunk Header.
                writer.Write((int)0);
                writer.Write((int)0);
            }

            // For each chunk, write the data for that chunk. Before writing the data, get the current offset and update the header.
            List<WDOMEntityNode>[] dictionaryData = new List<WDOMEntityNode>[actorCategories.Count];
            actorCategories.Values.CopyTo(dictionaryData, 0);

            for(int i = 0; i < chunkHeaders.Count; i++)
            {
                ChunkHeader header = chunkHeaders[i];
                chunkHeaders[i] = new ChunkHeader(header.FourCC, header.ElementCount, (int)(writer.BaseStream.Position - chunkStart));

                List<WDOMEntityNode> actors = dictionaryData[i];

                if (header.FourCC == FourCC.RTBL)
                {
                    SaveRoomTable(actors, writer);
                    continue;
                }

                foreach (var actor in actors)
                {
                    actor.PreSave();
                    actor.Serialize(writer);
                }
            }

            // Now that we've written every actor to file we can go back and re-write the headers now that we know their offsets.
            writer.BaseStream.Position = chunkStart + 0x4; // 0x4 is the offset to the Chunk Headers
            foreach (var header in chunkHeaders)
            {
                writer.WriteFixedString(FourCCConversion.GetStringFromEnum(header.FourCC), 4); // FourCC
                writer.Write(header.ElementCount); // Number of Entries
                writer.Write(header.ChunkOffset);   // Offset from start of file.
            }

            // Seek to the end of the file, and then pad us to 32-byte alignment.
            writer.BaseStream.Seek(0, SeekOrigin.End);

            int delta = WMath.Pad16Delta(writer.BaseStream.Position);
            for (int i = 0; i < delta; i++)
                writer.Write((byte)0xFF);
        }

        private void SaveRoomTable(List<WDOMEntityNode> entries, EndianBinaryWriter writer)
        {
            int base_offset = (int)writer.BaseStream.Position;

            foreach (var entry in entries)
            {
                writer.Write((int)0); // Placeholders for entry offsets
            }

            writer.Seek(base_offset, SeekOrigin.Begin);

            int first_entry_offset = (int)writer.BaseStream.Length;

            foreach (var entry in entries)
            {
                writer.Write((int)writer.BaseStream.Length);
                writer.Seek(0, SeekOrigin.End);

                entry.Serialize(writer);

                base_offset += 4;

                writer.Seek(base_offset, SeekOrigin.Begin);
            }

            writer.Seek(first_entry_offset, SeekOrigin.Begin);

            foreach (var entry in entries)
            {
                RoomTableEntryNode node = entry as RoomTableEntryNode;
                node.WriteLoadedRoomTable(writer);
            }

            writer.Seek(0, SeekOrigin.End);
        }
    }
}
