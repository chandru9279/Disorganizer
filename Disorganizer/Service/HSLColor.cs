using System;
using System.Drawing;

namespace zasz.me.Disorganizer.Service
{
    /// <summary>
    /// Color class shown in Rich Newman's Post http://richnewman.wordpress.com/hslcolor-class/
    /// with small edits. Private data members below are on scale 0-1. They are scaled for use 
    /// externally based on scale.
    /// </summary>
    public class HslColor
    {
        private readonly double _Scale;
        private double _Hue = 1.0;
        private double _Luminosity = 1.0;
        private double _Saturation = 1.0;

        public HslColor()
        {
            _Scale = 1.0;
        }

        public HslColor(Color Color, double Scale = 1.0)
            : this(Color.R, Color.G, Color.B, Scale)
        {
            _Scale = Scale;
        }

        public HslColor(int Red, int Green, int Blue, double Scale = 1.0)
        {
            var HslColor = (HslColor) Color.FromArgb(Red, Green, Blue);
            _Hue = HslColor._Hue;
            _Saturation = HslColor._Saturation;
            _Luminosity = HslColor._Luminosity;
            _Scale = Scale;
        }

        public HslColor(double Hue, double Saturation, double Luminosity, double Scale = 1.0)
        {
            _Hue = Hue;
            _Scale = Scale;
            _Saturation = Saturation;
            _Luminosity = Luminosity;
        }

        public double Hue
        {
            get { return _Hue*_Scale; }
            set { _Hue = CheckRange(value/_Scale); }
        }

        public double Saturation
        {
            get { return _Saturation*_Scale; }
            set { _Saturation = CheckRange(value/_Scale); }
        }

        public double Luminosity
        {
            get { return _Luminosity*_Scale; }
            set { _Luminosity = CheckRange(value/_Scale); }
        }

        private static double CheckRange(double Value)
        {
            if (Value < 0.0)
                Value = 0.0;
            else if (Value > 1.0)
                Value = 1.0;
            return Value;
        }

        public override string ToString()
        {
            return String.Format("H: {0:#0.##} S: {1:#0.##} L: {2:#0.##}", Hue, Saturation, Luminosity);
        }

        public string ToRgbString()
        {
            var RgbColor = (Color) this;
            return String.Format("R: {0:#0.##} G: {1:#0.##} B: {2:#0.##}", RgbColor.R, RgbColor.G, RgbColor.B);
        }

        #region Casts to/from System.Drawing.Color

        public static explicit operator Color(HslColor ColorHsl)
        {
            double R = 0, G = 0, B = 0;
            if (ColorHsl._Luminosity != 0)
            {
                if (ColorHsl._Saturation == 0)
                    R = G = B = ColorHsl._Luminosity;
                else
                {
                    var Temp2 = GetTemp2(ColorHsl);
                    var Temp1 = 2.0*ColorHsl._Luminosity - Temp2;

                    R = GetColorComponent(Temp1, Temp2, ColorHsl._Hue + 1.0/3.0);
                    G = GetColorComponent(Temp1, Temp2, ColorHsl._Hue);
                    B = GetColorComponent(Temp1, Temp2, ColorHsl._Hue - 1.0/3.0);
                }
            }
            return Color.FromArgb((int) (255*R), (int) (255*G), (int) (255*B));
        }

        private static double GetColorComponent(double Temp1, double Temp2, double Temp3)
        {
            Temp3 = MoveIntoRange(Temp3);
            if (Temp3 < 1.0/6.0)
                return Temp1 + (Temp2 - Temp1)*6.0*Temp3;
            if (Temp3 < 0.5)
                return Temp2;
            if (Temp3 < 2.0/3.0)
                return Temp1 + ((Temp2 - Temp1)*((2.0/3.0) - Temp3)*6.0);
            return Temp1;
        }

        private static double MoveIntoRange(double Temp3)
        {
            if (Temp3 < 0.0)
                Temp3 += 1.0;
            else if (Temp3 > 1.0)
                Temp3 -= 1.0;
            return Temp3;
        }

        private static double GetTemp2(HslColor ColorHsl)
        {
            double Temp2;
            if (ColorHsl._Luminosity < 0.5)
                Temp2 = ColorHsl._Luminosity*(1.0 + ColorHsl._Saturation);
            else
                Temp2 = ColorHsl._Luminosity + ColorHsl._Saturation - (ColorHsl._Luminosity*ColorHsl._Saturation);
            return Temp2;
        }

        public static explicit operator HslColor(Color RgbColor)
        {
            var HslColor = new HslColor
                               {
                                   _Hue = RgbColor.GetHue()/360f,
                                   _Luminosity = RgbColor.GetBrightness(),
                                   _Saturation = RgbColor.GetSaturation()
                               };
            return HslColor;
        }

        #endregion
    }
}