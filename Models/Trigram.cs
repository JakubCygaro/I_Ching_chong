using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace I_Ching.Models
{
    public class Trigram
    {
        public enum TNature
        {
            Heaven,
            Wind,
            Fire,
            Mountain,
            Lake,
            Water,
            Thunder,
            Earth
        }
        public enum Line
        {
            Full,
            FullDot,
            Broken,
            BrokenDot
        }
        public TNature Nature { get; init; }

        private readonly Line[] _lines = new Line[3];
        public bool CanChange { get; init; } = false;
        public Line this[int i]
        {
            get
            {
                if (i > 2 || i< 0)
                    throw new IndexOutOfRangeException();
                return _lines[i];
            }
        }

        public Trigram(Line line1, Line line2, Line line3)
        {
            _lines[0] = line1;
            _lines[1] = line2;
            _lines[2] = line3;
            Nature = GetNature(GetCode(_lines));
            foreach(Line line in _lines)
                if(line == Line.FullDot || line == Line.BrokenDot)
                    CanChange = true;
        }
        private Trigram(Line[] lines):this(lines[0], lines[1], lines[2]) { CanChange = false; }
        public static Trigram GetRandom()
        {
            return new Trigram(GetRandomLine(), GetRandomLine(), 
                GetRandomLine());
        }
        public static Line GetRandomLine()
        {
            Random random = new Random();
            var sum = 0;
            for (int i = 0; i < 3; i++)
            {
                if (random.Next(2) == 1)
                    sum++;
            }
            switch (sum)
            {
                case 0:
                    return Line.FullDot;
                case 1:
                    return Line.Broken;
                case 2:
                    return Line.Full;
                case 3:
                    return Line.BrokenDot;
            }
            throw new Exception("Trigram.GetRandomLine fucked up");
        }
        private static TNature GetNature(string code) =>
            code switch
            {
                "111" => TNature.Heaven,
                "110" => TNature.Wind,
                "101" => TNature.Fire,
                "100" => TNature.Mountain,
                "011" => TNature.Lake,
                "010" => TNature.Water,
                "001" => TNature.Thunder,
                "000" => TNature.Earth,
                _ => TNature.Heaven,
            };
        private static string GetCode(Line[] lines)
        {
            if (lines.Length == 0)
                throw new ArgumentException();

            StringBuilder sb = new StringBuilder();
            for(int i = lines.Length-1; i >= 0; i--)
            {
                if (lines[i] == Line.FullDot || lines[i] == Line.Full)
                {
                    sb.Append('1');
                }
                else
                {
                    sb.Append('0');
                }
            }
            return sb.ToString() ?? string.Empty;
        }
        public static Trigram Change(Trigram t)
        {
            var lines = t._lines;

            for(int i = 0; i < lines.Length; i++)
            {
                if (lines[i] == Line.FullDot)
                    lines[i] = Line.Broken;
                if (lines[i] == Line.BrokenDot)
                    lines[i] = Line.Full;
            }
            return new Trigram(lines);
        }
    }
}
