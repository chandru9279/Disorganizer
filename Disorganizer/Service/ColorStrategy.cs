using System;
using System.Collections.Generic;
using System.Drawing;

namespace zasz.me.Disorganizer.Service
{
    public abstract class ColorStrategy
    {
        private static readonly Dictionary<Style, Func<Color, HslColor, Theme, ColorStrategy>> _Set;

        protected readonly HslColor _Foreground;
        protected Color _Background;
        protected Random _Seed;

        static ColorStrategy()
        {
            _Set = new Dictionary<Style, Func<Color, HslColor, Theme, ColorStrategy>>(3);
            _Set.Add(Style.Fixed, (BgHsl, FgHsl, TheTheme) => new FixedForeground(BgHsl, FgHsl));
            _Set.Add(Style.Varied, (BgHsl, FgHsl, TheTheme) => new VariedForeground(BgHsl, FgHsl, TheTheme));
            _Set.Add(Style.RandomVaried, (BgHsl, FgHsl, TheTheme) => new RandomVaried(BgHsl, FgHsl, TheTheme));
            _Set.Add(Style.Random, (BgHsl, FgHsl, TheTheme) => new RandomForeground(BgHsl, FgHsl));
            _Set.Add(Style.Grayscale, (BgHsl, FgHsl, TheTheme) => new Grayscale(BgHsl, FgHsl, TheTheme));
        }

        protected ColorStrategy(Color Background, HslColor Foreground)
        {
            _Background = Background;
            _Foreground = Foreground;
            _Seed = new Random(DateTime.Now.Second);
        }

        public static ColorStrategy Get(Theme TheTheme, Style TheStyle,
                                        Color Background, Color Foreground)
        {
            var FgHsl = (TheTheme == Theme.LightBgDarkFg)
                            ? Foreground.Darken()
                            : Foreground.Lighten();
            if (Background.A != 255) /* Not interfering with any transparent color */
                return _Set[TheStyle](Background, FgHsl, TheTheme);
            var BgHsl = (TheTheme == Theme.LightBgDarkFg)
                            ? Background.Lighten()
                            : Background.Darken();
            return _Set[TheStyle]((Color) BgHsl, FgHsl, TheTheme);
        }

        public Color GetBackGroundColor()
        {
            return _Background;
        }

        public abstract Color GetCurrentColor();
    }

    internal class FixedForeground : ColorStrategy
    {
        public FixedForeground(Color Background, HslColor Foreground)
            : base(Background, Foreground)
        {
        }

        public override Color GetCurrentColor()
        {
            return (Color) _Foreground;
        }
    }

    internal class RandomForeground : FixedForeground
    {
        public RandomForeground(Color Background, HslColor Foreground)
            : base(Background, Foreground)
        {
        }

        public override Color GetCurrentColor()
        {
            _Foreground.Hue = _Seed.NextDouble();
            return (Color) _Foreground;
        }
    }

    internal class VariedForeground : ColorStrategy
    {
        protected readonly double _Range;

        public VariedForeground(Color Background, HslColor Foreground, Theme TheTheme)
            : base(Background, Foreground)
        {
            /* Dark foreground needed, so Luminosity is reduced to somewhere between 0 & 0.5
             * Saturation is full so the color comes out, removing all blackness/greyness
             * For Light foreground Luminosity is kept between 0.5 & 1
             */
            _Foreground.Saturation = 1.0;
            _Range = (TheTheme == Theme.LightBgDarkFg) ? 0d : 0.5;
        }

        public override Color GetCurrentColor()
        {
            _Foreground.Luminosity = (_Seed.NextDouble()*0.5) + _Range;
            return (Color) _Foreground;
        }
    }

    internal class RandomVaried : VariedForeground
    {
        public RandomVaried(Color Background, HslColor Foreground, Theme TheTheme)
            : base(Background, Foreground, TheTheme)
        {
        }

        public override Color GetCurrentColor()
        {
            _Foreground.Hue = _Seed.NextDouble();
            return base.GetCurrentColor();
        }
    }

    internal class Grayscale : VariedForeground
    {
        public Grayscale(Color Background, HslColor Foreground, Theme TheTheme)
            : base(Background, Foreground, TheTheme)
        {
            /* Saturation is 0 - Meaning no color specified by hue can be seen at all. 
             * So luminance is now reduced to showing grayscale */
            _Foreground.Saturation = 0.0;
            _Background = Color.FromArgb(Background.A, TheTheme == Theme.LightBgDarkFg ? Color.White : Color.Black);
        }
    }

    public enum Theme
    {
        DarkBgLightFg,
        LightBgDarkFg
    }

    public enum Style
    {
        Fixed,
        Random,
        Varied,
        RandomVaried,
        Grayscale
    }

    public static class ColorExtension
    {
        public static HslColor Lighten(this Color Given)
        {
            var ColorHsl = (HslColor) Given;
            if (ColorHsl.Luminosity < 0.5) ColorHsl.Luminosity = 0.75;
            return ColorHsl;
        }

        public static HslColor Darken(this Color Given)
        {
            var ColorHsl = (HslColor) Given;
            if (ColorHsl.Luminosity > 0.5) ColorHsl.Luminosity = 0.25;
            return ColorHsl;
        }
    }
}