using Infrastructure.Tools;
using System.IO;

namespace Infrastructure
{
    class LevelInfo
    {
        public Vector StartPosition { get; set; }
        public Vector StartVelocity { get; set; }
        public double StartFuel { get; set; }

        public LevelInfo(string path)
        {
            var lines = File.ReadAllLines(path);
            try
            {
                var line = lines[0].Split();
                StartPosition = Vector.Create(double.Parse(line[0]), double.Parse(line[1]));
                line = lines[1].Split();
                StartVelocity = Vector.Create(double.Parse(line[0]), double.Parse(line[1]));
                StartFuel = double.Parse(lines[2]);
            }
            catch
            {
                throw new InvalidDataException("Invalid file format.");
            }
        }
    }
}
