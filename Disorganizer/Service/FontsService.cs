using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;

namespace zasz.me.Disorganizer.Service
{
    public class FontsService : IDisposable
    {
        private readonly Action<string> die = message => { throw new Exception(message); };

        private PrivateFontCollection fonts;


        /// <summary>
        /// A list of private fonts used by this service. It is loaded from the
        /// path given while construction. It expects .ttf files (TrueType Font)
        /// </summary>
        public Dictionary<string, FontFamily> AvailableFonts { get; private set; }

        #region IDisposable Members

        public void Dispose()
        {
            fonts.Dispose();
        }

        #endregion

        public void LoadFonts(string fontsFolderPath)
        {
            if (string.IsNullOrEmpty(fontsFolderPath)) die("Null Fonts Path");
            var files = Directory.GetFiles(fontsFolderPath);
            var fontFiles = (from aFile in files
                             where aFile.EndsWith(".ttf")
                             select aFile).ToList();
            if (!fontFiles.Any()) die("No Fonts Found");
            fonts = new PrivateFontCollection();
            fontFiles.ForEach(f => fonts.AddFontFile(f));
            AvailableFonts = fonts.Families.ToDictionary(x => x.Name);
        }
    }
}