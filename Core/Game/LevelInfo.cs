using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Core.Objects;
using Core.Physics;
using Core.Tools;
using System.Linq;

namespace Core.Game
{
    public class LevelInfo
    {
        public Vector StartPosition { get; }
        public Vector StartVelocity { get; }
        public double StartFuel { get; }
        public string PhysicsName { get; }
        public string LandscapeFile { get; }

        public LevelInfo(Vector startPosition, Vector startVelocity, double startFuel, string physicsName, string landscapeFile)
        {
            StartPosition = startPosition;
            StartVelocity = startVelocity;
            StartFuel = startFuel;
            PhysicsName = physicsName;
            LandscapeFile = landscapeFile;
        }

        private static Vector ReadVector(string str)
        {
            var tokens = str.Split();
            return Vector.Create(double.Parse(tokens[0]), double.Parse(tokens[1]));
        }

        public Level BuildLevel()
        {
            var landscape = Landscape.LoadFromImageFile(LandscapeFile,
                color => color.R + color.B + color.G < 100 ? LandscapeCell.Ground : LandscapeCell.Empty);

            var ship = new Ship(StartPosition, Core.Tools.Size.Create(30, 30), 1, 20)
            {
                Velocity = StartVelocity,
                Fuel = StartFuel
            };

            return Level.Create(landscape, Enumerable.Empty<IPhysObject>(), new PhysicsFactory().FromName(PhysicsName),
                ship);
        }

        private string[] ToStringArray()
        {
            return new[] { StartPosition.ToString(), StartVelocity.ToString(), StartFuel.ToString(), PhysicsName, LandscapeFile };
        }

        public void WriteLevelInFile(string fileName)
        {
            File.WriteAllLines(fileName, ToStringArray());
        }

        public static LevelInfo CreateFromText(IReadOnlyList<string> text)
        {
            return new LevelInfo(ReadVector(text[0]), ReadVector(text[1]), double.Parse(text[2]), text[3], text[4]);
        }

        public static LevelInfo CreateFromFile(string path)
        {
            var text = File.ReadAllLines(path);
            text[4] = GetPathToImageRelativeFrom(path, text[4]);
            
            return CreateFromText(text);
        }

        private static string GetPathToImageRelativeFrom(string path, string image)
        {
            var builder = new StringBuilder();

            var tokens = path.Split(new[] {'\\'}, StringSplitOptions.None);
            foreach (var token in tokens.Take(tokens.Length - 1))
                builder.Append($"{token}\\");

            builder.Append(image);

            return builder.ToString();
        }
    }
}
