using System;
using System.Collections.Generic;
using System.IO;
using Core.Tools;

namespace Core.Game
{
    public class LevelInfo
    {
        public Vector StartPosition { get; }
        public Vector StartVelocity { get; }
        public double StartFuel { get; }
        public string Physics { get; }
        public string Landscape { get; }

        public LevelInfo(Vector startPosition, Vector startVelocity, double startFuel, string physics, string landscape)
        {
            StartPosition = startPosition;
            StartVelocity = startVelocity;
            StartFuel = startFuel;
            Physics = physics;
            Landscape = landscape;
        }

        private static Vector ReadVector(string str)
        {
            var tokens = str.Split();
            return Vector.Create(double.Parse(tokens[0]), double.Parse(tokens[1]));
        }

        private string[] ToStringArray()
        {
            return new[] { StartPosition.ToString(), StartVelocity.ToString(), StartFuel.ToString(), Physics, Landscape };
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
            return CreateFromText(text);
        }
    }
}
