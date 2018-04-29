using System.IO;
using Core.Tools;

namespace Core
{
    public class LevelInfo
    {
        public Vector StartPosition { get; set; }
        public Vector StartVelocity { get; set; }
        public double StartFuel { get; set; }
        public string Physics { get; set; }

        public LevelInfo(Vector startPosition, Vector startVelocity, double startFuel, string physics)
        {
            StartPosition = startPosition;
            StartVelocity = startVelocity;
            StartFuel = startFuel;
            Physics = physics;
        }

        public LevelInfo(string path)
        {
            var lines = File.ReadAllLines(path);
            try
            {
                StartPosition = ReadVector(lines[0].Split());
                StartVelocity = ReadVector(lines[1].Split());
                StartFuel = double.Parse(lines[2]);
                Physics = lines[3];
            }
            catch
            {
                throw new InvalidDataException("Invalid file format.");
            }
        }

        private static Vector ReadVector(string[] line)
        {
            return Vector.Create(double.Parse(line[0]), double.Parse(line[1]));
        }

        private string[] ToStringArray()
        {
            return new string[] { StartPosition.ToString(), StartVelocity.ToString(), StartFuel.ToString(), Physics };
        }

        public void WriteLevelInFile(LevelInfo levelInfo, string fileName)
        {
            File.WriteAllLines(fileName, levelInfo.ToStringArray());
        }
    }
}
