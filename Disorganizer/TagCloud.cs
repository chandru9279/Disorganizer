using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using zasz.me.Disorganizer.Service;

namespace zasz.me.Disorganizer
{
    public partial class TagCloud : Form
    {
        private Color bg = Color.White;
        private Color fg = Color.Black;
        private FontsService service;

        public TagCloud()
        {
            InitializeComponent();
        }

        private static string RootPath
        {
            get
            {
                var path = Assembly.GetExecutingAssembly().CodeBase.Replace("file:///", "");
                return path.Substring(0, path.IndexOf("bin/Debug/"));
            }
        }

        private void TagCloudLoad(object sender, EventArgs e)
        {
            service = new FontsService();
            service.LoadFonts(RootPath + @"\Fonts\");
            FontsCombo.Items.AddRange(service.AvailableFonts.Keys.ToArray());
            StrategyCombo.Items.AddRange(Enum.GetNames(typeof (TagDisplayStrategy)));
            BgfgStrategyCombo.Items.AddRange(Enum.GetNames(typeof (Theme)));
            FgStrategyCombo.Items.AddRange(Enum.GetNames(typeof (Style)));
        }

        private void GenerateClick(object sender, EventArgs e)
        {
            Cloud.Controls.Clear();
            var genCloudSysPath = RootPath + @"\Cloud.png";
            var tags = Words.Lines.Select(
                line => line.Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries))
                .Where(splits => splits.Length == 2)
                .ToDictionary(splits => splits[0], splits => int.Parse(splits[1]));
            var tagCloudService = new TagCloudService(tags, int.Parse(Width.Text), int.Parse(Height.Text))
                                      {
                                          MaximumFontSize = float.Parse(MaxFontSize.Text),
                                          MinimumFontSize = float.Parse(MinFontSize.Text),
                                      };
            if (!String.IsNullOrEmpty(Angle.Text)) tagCloudService.Angle = int.Parse(Angle.Text);
            if (!String.IsNullOrEmpty(Margin.Text)) tagCloudService.Margin = int.Parse(Margin.Text);
            if (null != FontsCombo.SelectedItem)
                tagCloudService.SelectedFont = service.AvailableFonts[FontsCombo.SelectedItem.ToString()];
            if (null != StrategyCombo.SelectedItem)
                tagCloudService.DisplayChoice = DisplayStrategy.Get(
                    (TagDisplayStrategy) Enum.Parse(typeof (TagDisplayStrategy), StrategyCombo.SelectedItem.ToString()));
            var bgfgScheme = null != BgfgStrategyCombo.SelectedItem
                                 ? (Theme) Enum.Parse(typeof (Theme), BgfgStrategyCombo.SelectedItem.ToString())
                                 : Theme.LightBgDarkFg;
            var fgScheme = null != FgStrategyCombo.SelectedItem
                               ? (Style) Enum.Parse(typeof (Style), FgStrategyCombo.SelectedItem.ToString())
                               : Style.Varied;
            tagCloudService.ColorChoice = ColorStrategy.Get(bgfgScheme, fgScheme, bg, fg);
            tagCloudService.VerticalTextRight = VerticalTextRight.Checked;
            tagCloudService.ShowWordBoundaries = ShowBoundaries.Checked;
            tagCloudService.Crop = Cropper.Checked;
            Dictionary<string, RectangleF> borders;
            var bitmap = tagCloudService.Construct(out borders);
            Skipped.Text = string.Join("; ", tagCloudService.WordsSkipped.Select(x => x.Key));
            bitmap.Save(genCloudSysPath, ImageFormat.Png);
            Cloud.Image = bitmap;
            borders.Values.ToList().ForEach(x => Cloud.Controls.Add(GetBorder(x)));
        }

        private static Control GetBorder(RectangleF borders)
        {
            var it = Rectangle.Round(borders);
            var border = new Label
                             {
                                 Top = it.Top,
                                 Left = it.Left,
                                 Width = it.Width,
                                 Height = it.Height,
                                 BackColor = Color.FromArgb(0, Color.White)
                             };
            border.MouseEnter += OnEnter;
            border.MouseLeave += OnLeave;
            return border;
        }

        private static void OnEnter(object sender, EventArgs e)
        {
            ((Label) sender).BorderStyle = BorderStyle.FixedSingle;
        }

        private static void OnLeave(object sender, EventArgs e)
        {
            ((Label) sender).BorderStyle = BorderStyle.None;
        }

        private void SetBgClick(object sender, EventArgs e)
        {
            if (ColorPick.ShowDialog() != DialogResult.OK) return;
            bg = ColorPick.Color;
            BackG.BackColor = bg;
        }

        private void SetFgClick(object sender, EventArgs e)
        {
            if (ColorPick.ShowDialog() != DialogResult.OK) return;
            fg = ColorPick.Color;
            ForeG.BackColor = fg;
        }

        private void TagCloudFormClosing(object sender, FormClosingEventArgs e)
        {
            service.Dispose();
        }
    }
}