using System;
using System.Windows.Media;

namespace ColoredGradients;

internal static class ColorTool
{
    static readonly Lazy<Random> _random = new();

    internal static SolidColorBrush RandomColor => new(Color.FromRgb((byte)_random.Value.Next(0, 256),
                                                                     (byte)_random.Value.Next(0, 256),
                                                                     (byte)_random.Value.Next(0, 256)));

    internal static (double red, double green, double blue) RandomColorMulpitilers() => (_random.Value.NextDouble(),
                                                                                         _random.Value.NextDouble(),
                                                                                         _random.Value.NextDouble());

    internal static SolidColorBrush GetMultipliedColor(int shadeIndex, double redMultiplier, double greenMultiplier, double blueMultiplier)
    {
        shadeIndex *= 8;

        return new(Color.FromRgb((byte)(redMultiplier * shadeIndex),
                                 (byte)(greenMultiplier * shadeIndex),
                                 (byte)(blueMultiplier * shadeIndex)));
    }

    internal static SolidColorBrush GetGreyShadeColor(int shadeIndex)
    {
        shadeIndex *= 8;

        byte shade = (byte)shadeIndex;

        return new(Color.FromRgb(shade, shade, shade));
    }
}