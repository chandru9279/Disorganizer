using System;
using System.Collections.Generic;
using System.Drawing;

namespace zasz.me.Disorganizer.Service
{
    public abstract class DisplayStrategy
    {
        public static readonly StringFormat HorizontalFormat;
        public static readonly StringFormat VerticalFormat;
        protected static readonly Random Seed;
        private static readonly Dictionary<TagDisplayStrategy, DisplayStrategy> _Set;

        static DisplayStrategy()
        {
            VerticalFormat = new StringFormat();
            HorizontalFormat = new StringFormat();
            VerticalFormat.FormatFlags = StringFormatFlags.DirectionVertical;
            Seed = new Random(DateTime.Now.Second);

            _Set = new Dictionary<TagDisplayStrategy, DisplayStrategy>(6)
                       {
                           {TagDisplayStrategy.EqualHorizontalAndVertical, new EqualHorizontalAndVertical()},
                           {TagDisplayStrategy.AllHorizontal, new AllHorizontal()},
                           {TagDisplayStrategy.AllVertical, new AllVertical()},
                           {TagDisplayStrategy.RandomHorizontalOrVertical, new RandomHorizontalOrVertical()},
                           {TagDisplayStrategy.MoreHorizontalThanVertical, new RandomHorizontalOrVertical(0.25)},
                           {TagDisplayStrategy.MoreVerticalThanHorizontal, new RandomHorizontalOrVertical(0.75)}
                       };
        }

        public static DisplayStrategy Get(TagDisplayStrategy DisplayStrategy)
        {
            return _Set[DisplayStrategy];
        }

        public abstract StringFormat GetFormat();
    }

    internal class AllHorizontal : DisplayStrategy
    {
        public override StringFormat GetFormat()
        {
            return HorizontalFormat;
        }
    }

    internal class AllVertical : DisplayStrategy
    {
        public override StringFormat GetFormat()
        {
            return VerticalFormat;
        }
    }

    internal class RandomHorizontalOrVertical : DisplayStrategy
    {
        private readonly double _Split;

        public RandomHorizontalOrVertical(double Split = 0.5)
        {
            _Split = Split;
        }

        public override StringFormat GetFormat()
        {
            return Seed.NextDouble() > _Split ? HorizontalFormat : VerticalFormat;
        }
    }

    internal class EqualHorizontalAndVertical : DisplayStrategy
    {
        private bool _CurrentState;

        public EqualHorizontalAndVertical()
        {
            _CurrentState = Seed.NextDouble() > 0.5;
        }

        public override StringFormat GetFormat()
        {
            _CurrentState = !_CurrentState;
            return _CurrentState ? HorizontalFormat : VerticalFormat;
        }
    }

    public enum TagDisplayStrategy
    {
        EqualHorizontalAndVertical,
        AllHorizontal,
        AllVertical,
        RandomHorizontalOrVertical,
        MoreHorizontalThanVertical,
        MoreVerticalThanHorizontal
    }
}