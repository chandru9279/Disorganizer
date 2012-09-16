using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;

namespace zasz.me.Disorganizer.Service
{
    public class TagCloudService
    {
        private readonly Func<float, float> _Decrement;
        private readonly Action<string> _Die = Msg => { throw new Exception(Msg); };

        private readonly int _Height;

        private readonly int _HighestWeight;
        private readonly Func<float, float> _Increment;
        private readonly int _LowestWeight;
        private readonly RectangleF _MainArea;

        private readonly PointF _SpiralEndSentinel;
        private readonly Dictionary<string, int> _TagsSorted;
        private readonly int _Width;
        internal PointF _Center;
        internal PointF _CurrentCorner;
        internal int _CurrentEdgeSize;
        private Func<float, float> _EdgeDirection;
        private float _FontHeightSpan;
        internal int _MaxEdgeSize;
        internal List<RectangleF> _Occupied;

        private bool _ServiceObjectNew = true;
        internal bool _SleepingEdge;
        private int _SpiralRoom;
        private int _WeightSpan;

        public TagCloudService(Dictionary<string, int> Tags, int Width, int Height)
        {
            _Increment = It => It + _SpiralRoom;
            _Decrement = It => It - _SpiralRoom;
            if (null == Tags || 0 == Tags.Count)
                _Die("Argument Exception, No Tags to disorganize");
            if (Width < 30 || Height < 30)
                _Die("Way too low Width or Height for the cloud to be useful");
            _Width = Width;
            _Height = Height;
            _MainArea = new RectangleF(0, 0, Width, Height);
            _MaxEdgeSize = Width >= Height ? Width : Height;
            /* Sentinel is a definitely out of bounds point that the spiral normally
             * should never reach. */
            _SpiralEndSentinel = new PointF(_MaxEdgeSize + 10, _MaxEdgeSize + 10);
            var Sorted = from Tag in Tags
                         orderby Tag.Value descending
                         select new {Tag.Key, Tag.Value};
            _TagsSorted = Sorted.ToDictionary(x => x.Key, x => x.Value);
            _LowestWeight = _TagsSorted.Last().Value;
            _HighestWeight = _TagsSorted.First().Value;
            _Occupied = new List<RectangleF>(_TagsSorted.Count + 4);
            WordsSkipped = new Dictionary<string, int>();
            ApplyDefaults();
        }

        /// <summary>
        ///   Default is Times New Roman
        /// </summary>
        public FontFamily SelectedFont { get; set; }

        /// <summary>
        ///   Default is false, Enable to start seeing Word boundaries used for
        ///   collision detection.
        /// </summary>
        public bool ShowWordBoundaries { get; set; }

        /// <summary>
        ///   Set this to true, if vertical must needs to appear with RHS as floor
        ///   Default is LHS is the floor and RHS is ceiling of the Text.
        /// </summary>
        public bool VerticalTextRight { get; set; }

        /// <summary>
        ///   Size of the smallest string in the TagCloud
        /// </summary>
        public float MinimumFontSize { get; set; }

        /// <summary>
        ///   Size of the largest string in the TagCloud
        /// </summary>
        public float MaximumFontSize { get; set; }

        /// <summary>
        ///   Use <code>DisplayStrategy.Get()</code> to get a Display Strategy
        ///   Default is RandomHorizontalOrVertical.
        /// </summary>
        public DisplayStrategy DisplayChoice { get; set; }

        /// <summary>
        ///   Use <code>ColorStrategy.Get()</code> to get a Color Strategy
        ///   Default is white background and random darker foreground colors.
        /// </summary>
        public ColorStrategy ColorChoice { get; set; }

        /// <summary>
        ///   A rotate transform will be applied on the whole image based on this 
        ///   Angle in degrees. Which means the Boundaries are not usable for hover animations
        ///   in CSS/HTML.
        /// </summary>
        public int Angle { get; set; }

        /// <summary>
        ///   Default is false. Set this to true to crop out blank background.
        /// </summary>
        public bool Crop { get; set; }

        /// <summary>
        ///   Default is 30px.
        /// </summary>
        public float Margin { get; set; }

        /// <summary>
        ///   Default is 0. The higher the number, the more roomy
        ///   the tag cloud is, and more performant the service is.
        ///   Don't go over 20 for good results
        /// </summary>
        public int SpiralRoom
        {
            get { return _SpiralRoom/2 - 1; }
            set { _SpiralRoom = 2*value + 1; }
        }

        /// <summary>
        ///   Words that were not rendered because of non-availability
        ///   of free area to render them. If count is anything other than 0
        ///   use a bigger bitmap as input with more area.
        /// </summary>
        public Dictionary<string, int> WordsSkipped { get; set; }

        private void ApplyDefaults()
        {
            SelectedFont = new FontFamily("Times New Roman");
            MinimumFontSize = 1f;
            MaximumFontSize = 5f;
            Angle = 0;
            DisplayChoice = DisplayStrategy.Get(TagDisplayStrategy.RandomHorizontalOrVertical);
            ColorChoice = ColorStrategy.Get(Theme.LightBgDarkFg, Style.RandomVaried,
                                            Color.White, Color.Black);
            VerticalTextRight = false;
            ShowWordBoundaries = false;
            Margin = 30f;
            SpiralRoom = 0;

            /* Adding 4 Rectangles on the border to make sure that words dont go outside the border.
             * Words going outside the border will collide on these and hence be placed elsewhere.
             */
            _Occupied.Add(new RectangleF(0, -1, _Width, 1));
            _Occupied.Add(new RectangleF(-1, 0, 1, _Height));
            _Occupied.Add(new RectangleF(0, _Height, _Width, 1));
            _Occupied.Add(new RectangleF(_Width, 0, 1, _Height));
        }

        public Bitmap Construct(out Dictionary<string, RectangleF> Borders)
        {
            if (_ServiceObjectNew)
                _ServiceObjectNew = false;
            else
                _Die("This object has been used. Dispose this, create and use a new Service object.");
            var TheCloudBitmap = new Bitmap(_Width, _Height);
            var GImage = Graphics.FromImage(TheCloudBitmap);
            GImage.TextRenderingHint = TextRenderingHint.AntiAlias;
            _Center = new PointF(TheCloudBitmap.Width/2f, TheCloudBitmap.Height/2f);
            if (Angle != 0) GImage.Rotate(_Center, Angle);
            _WeightSpan = _HighestWeight - _LowestWeight;
            if (MaximumFontSize < MinimumFontSize)
                _Die("MaximumFontSize is less than MinimumFontSize");
            _FontHeightSpan = MaximumFontSize - MinimumFontSize;
            GImage.Clear(ColorChoice.GetBackGroundColor());

            foreach (var Tag in _TagsSorted)
            {
                var FontToApply = new Font(SelectedFont, CalculateFontSize(Tag.Value));
                var StringBounds = GImage.MeasureString(Tag.Key, FontToApply);
                var Format = DisplayChoice.GetFormat();
                var IsVertical = Format.FormatFlags.HasFlag(StringFormatFlags.DirectionVertical);
                if (IsVertical)
                {
                    var StringWidth = StringBounds.Width;
                    StringBounds.Width = StringBounds.Height;
                    StringBounds.Height = StringWidth;
                }
                var TopLeft = CalculateWhere(StringBounds);
                /* Strategy chosen display format, failed to be placed */
                if (TopLeft.Equals(_SpiralEndSentinel))
                {
                    WordsSkipped.Add(Tag.Key, Tag.Value);
                    continue;
                }
                var TextCenter = IsVertical & VerticalTextRight
                                     ? new PointF(TopLeft.X + (StringBounds.Width/2f),
                                                  TopLeft.Y + (StringBounds.Height/2f))
                                     : TopLeft;
                var CurrentBrush = new SolidBrush(ColorChoice.GetCurrentColor());
                if (IsVertical & VerticalTextRight) GImage.Rotate(TextCenter, -180);
                GImage.DrawString(Tag.Key, FontToApply, CurrentBrush, TopLeft, Format);
                if (IsVertical & VerticalTextRight) GImage.Rotate(TextCenter, 180);
                if (ShowWordBoundaries)
                    GImage.DrawRectangle(new Pen(CurrentBrush), TopLeft.X, TopLeft.Y, StringBounds.Width,
                                         StringBounds.Height);
                _Occupied.Add(new RectangleF(TopLeft, StringBounds));
            }
            GImage.Dispose();
            _Occupied.RemoveRange(0, 4);
            if (Crop)
                TheCloudBitmap = CropAndTranslate(TheCloudBitmap);
            Borders = _Occupied
                .Zip(_TagsSorted.Keys.Where(Word => !WordsSkipped.ContainsKey(Word)), (Rect, Tag) => new {Rect, Tag})
                .ToDictionary(x => x.Tag, x => x.Rect);
            return TheCloudBitmap;
        }

        private PointF CalculateWhere(SizeF Measure)
        {
            _CurrentEdgeSize = _SpiralRoom;
            _SleepingEdge = true;
            _CurrentCorner = _Center;

            var CurrentPoint = _Center;
            while (TryPoint(CurrentPoint, Measure) == false)
                CurrentPoint = GetNextPointInEdge(CurrentPoint);
            return CurrentPoint;
        }

        internal bool TryPoint(PointF TrialPoint, SizeF Rectangle)
        {
            if (TrialPoint.Equals(_SpiralEndSentinel)) return true;
            var TrailRectangle = new RectangleF(TrialPoint, Rectangle);
            return !_Occupied.Any(x => x.IntersectsWith(TrailRectangle));
        }

        /*
         * This method gives points that crawls along an edge of the spiral, described below.
         */

        internal PointF GetNextPointInEdge(PointF Current)
        {
            do
            {
                if (Current.Equals(_CurrentCorner))
                {
                    _CurrentCorner = GetSpiralNext(_CurrentCorner);
                    if (_CurrentCorner.Equals(_SpiralEndSentinel)) return _SpiralEndSentinel;
                }
                Current = Current.X == _CurrentCorner.X
                              ? new PointF(Current.X, _EdgeDirection(Current.Y))
                              : new PointF(_EdgeDirection(Current.X), Current.Y);
            } while (!_MainArea.Contains(Current));
            return Current;
        }

        /* Imagine a grid of 5x5 points, and 0,0 and 4,4 are the topright and bottomleft respectively.
         * You can move in a spiral by navigating as follows:
         * 1. Inc GivenPoint's X by 1 and return it.
         * 2. Inc GivenPoint's Y by 1 and return it.
         * 3. Dec GivenPoint's X by 2 and return it.
         * 4. Dec GivenPoint's Y by 2 and return it.
         * 5. Inc GivenPoint's X by 3 and return it.
         * 6. Inc GivenPoint's Y by 3 and return it.
         * 7. Dec GivenPoint's X by 4 and return it.
         * 8. Dec GivenPoint's Y by 4 and return it.
         * 
         * I'm calling the values 1,2,3,4 etc as _EdgeSize. Any joining of points in a graph is an edge.
         * To find out if we need to increment or decrement I'm using the condition _EdgeSize is even or not.
         * To increment EdgeSize, using a boolean _SleepingEdge to count upto 2 steps. I'll blog about this later
         * at chandruon.net!
         * 
         *       0  1  2  3  4
         *   0   X  X  X  X  X     .-------->
         *   1   X  X  X  X  X     | .-----.  
         *   2   X  X  X  X  X     | | --. |    
         *   3   X  X  X  X  X     | '---' |    
         *   4   X  X  X  X  X     '-------'    
         *   
         * 
         * Depth of Recursion is meant to be at most ONE in this method, 
         * and only when outlying edges are to be skipped.
         * 
         */

        internal PointF GetSpiralNext(PointF PreviousCorner)
        {
            float X = PreviousCorner.X, Y = PreviousCorner.Y;
            var EdgeSizeEven = (_CurrentEdgeSize & 1) == 0;

            if (_SleepingEdge)
            {
                X = EdgeSizeEven ? PreviousCorner.X - _CurrentEdgeSize : PreviousCorner.X + _CurrentEdgeSize;
                _SleepingEdge = false;
                /* Next edge will be standing. Sleeping = Parallal to X-Axis; Standing = Parallal to Y-Axis */
            }
            else
            {
                Y = EdgeSizeEven ? PreviousCorner.Y - _CurrentEdgeSize : PreviousCorner.Y + _CurrentEdgeSize;
                _CurrentEdgeSize += _SpiralRoom;
                _SleepingEdge = true;
            }

            _EdgeDirection = EdgeSizeEven ? _Decrement : _Increment;

            /* If the spiral widens to a point where its arms are longer than the Height & Width, 
             * it's time to end the spiral and give up placing the word. There is no 'point'
             * (no pun intended) in going for wider spirals, as you are out of bounds now.
             * Our spiral is an Archimedean Right spiral, made up of Line segments @ 
             * right-angles to each other.
             */
            return _CurrentEdgeSize > _MaxEdgeSize ? _SpiralEndSentinel : new PointF(X, Y);
        }

        // Range Mapping
        private float CalculateFontSize(int Weight)
        {
            // Strange case where all tags have equal weights
            if (_WeightSpan == 0) return (MinimumFontSize + MaximumFontSize)/2f;
            // Convert the Weight into a 0-1 range (float)
            var WeightScaled = (Weight - _LowestWeight)/(float) _WeightSpan;
            // Convert the 0-1 range into a value in the Font range.
            return MinimumFontSize + (WeightScaled*_FontHeightSpan);
        }

        /// <summary>
        ///   Uses the list of occupied areas to
        ///   crop the Bitmap and translates the list of occupied areas
        ///   keeping them consistant with the new cropped bitmap
        /// </summary>
        /// <param name = "CloudToCrop">The bitmap of the cloud to crop</param>
        /// <returns>The cropped version of the bitmap</returns>
        private Bitmap CropAndTranslate(Bitmap CloudToCrop)
        {
            var NewTop = _Occupied.Select(x => x.Top).Min() - Margin;
            var NewLeft = _Occupied.Select(x => x.Left).Min() - Margin;

            var Bottom = _Occupied.Select(x => x.Bottom).Max() + Margin;
            var Right = _Occupied.Select(x => x.Right).Max() + Margin;

            if (NewTop < 0) NewTop = 0;
            if (NewLeft < 0) NewLeft = 0;

            if (Bottom > _Height) Bottom = _Height;
            if (Right > _Width) Right = _Width;

            var PopulatedArea = new RectangleF(NewLeft, NewTop, Right - NewLeft, Bottom - NewTop);
            _Occupied =
                _Occupied.Select(It => new RectangleF(It.X - NewLeft, It.Y - NewTop, It.Width, It.Height)).ToList();
            return CloudToCrop.Clone(PopulatedArea, CloudToCrop.PixelFormat);
        }
    }

    public static class TagExtensions
    {
        public static void Rotate(this Graphics GImage, PointF About, int ByAngle)
        {
            GImage.TranslateTransform(About.X, About.Y);
            GImage.RotateTransform(ByAngle);
            GImage.TranslateTransform(-About.X, -About.Y);
        }

        /* Plan to use this to change strategy chosen format when it doesn't fit */

        public static StringFormat Other(this StringFormat Format)
        {
            return ReferenceEquals(Format, DisplayStrategy.HorizontalFormat)
                       ? DisplayStrategy.VerticalFormat
                       : DisplayStrategy.HorizontalFormat;
        }
    }
}