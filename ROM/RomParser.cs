using System.Text;

namespace YaNES.ROM
{
    public class RomParser
    {
        private const int PrgRomPageSize = 1024 * 16; // 16 kB
        private const int ChrRomPageSize = 1024 * 8; // 8 kB

        // TODO : check if this code is cross-platform
        public static ROM FromFile(string fileName)
        {
            if (!File.Exists(fileName))
                throw new FileNotFoundException();

            using var stream = File.Open(fileName, FileMode.Open);
            using var reader = new BinaryReader(stream, Encoding.ASCII, false);

            // stream position - 0

            var magicHeader = new byte[4] { 0x4E, 0x45, 0x53, 0x1A };

            for (int i = 0; i < magicHeader.Length; i++)
            {
                if (reader.ReadByte() != magicHeader[i])
                {
                    throw new Exception($"File {fileName} is not in iNES file format");
                }
            }

            // stream position - 4

            var romBanksCount = reader.ReadByte();
            var vromBanksCount = reader.ReadByte();
            var skipTrainer = (reader.ReadByte() & 0b100) != 0;

            // stream position - 7

            stream.Seek(16 - 7, SeekOrigin.Current);

            if (skipTrainer)
                stream.Seek(512, SeekOrigin.Current);

            var prgRom = reader.ReadBytes(romBanksCount * PrgRomPageSize);
            var chrRom = reader.ReadBytes(vromBanksCount * ChrRomPageSize);

            return new ROM(prgRom, chrRom);
        }
    }
}
