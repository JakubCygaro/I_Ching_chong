using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using I_Ching.Models;
using System.Xml.Linq;

namespace I_Ching
{
    public class HexagramHandler
    {
        private static readonly string xmlPath = @"Text/Fortunes.xml";
        private readonly XDocument _fortunes;
        public Hexagram? Hexagram { get; private set; }

        public HexagramHandler(Hexagram? initialHex)
        {
            Hexagram = initialHex;
            if(!System.IO.File.Exists(xmlPath))
                throw new System.IO.FileNotFoundException("Could not load fortunes!", "Fortunes.xml");

            _fortunes = XDocument.Load(xmlPath);
        }
        public HexagramHandler() : this(null) { }

        public void MakeNew()
        {
            Hexagram = Models.Hexagram.GetRandom();
        }
        public void Change()
        {
            Hexagram?.Change();
        }
        public Fortune GetFortune()
        {
            if (Hexagram is null)
                throw new NullReferenceException("Hexagram was null");
#pragma warning disable CS8600,CS8602

            var fortune = (from f in _fortunes.Descendants().Elements("Fortune")
                          where (string)f.FirstAttribute == Hexagram.UpperTrigram.Nature.ToString()
                                                                                        .ToLower()
                          && (string)f.LastAttribute == Hexagram.LowerTrigram.Nature.ToString()
                                                                                    .ToLower()
                          select new
                          {
                              Name = f.Element("Name").Value,
                              Description = f.Element("Text").Value
                          }).First();
#pragma warning restore

            return new Fortune
            {
                Name = fortune.Name,
                Description = fortune.Description,
            };
        }
    }
}
