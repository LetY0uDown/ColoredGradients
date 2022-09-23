namespace ColoredGradients;

internal static class GradientGenerator
{
    internal static int GetShadeIndex(int x, int y, GradientType gradientType)
    {
        switch (gradientType)
        {
            case GradientType.FromOneCorner: 
                return x + y;

            case GradientType.FromTwoCorners:
                var index = x + 1 + y;
                return index < Drawer.GRID_SIZE ? index
                                                : Drawer.GRID_SIZE - index + Drawer.GRID_SIZE;

            case GradientType.CornerShaped:
                return x + y < Drawer.GRID_SIZE ? Drawer.GRID_SIZE + 1 - (y + (Drawer.GRID_SIZE - y) - x)
                                                : x + (Drawer.GRID_SIZE - x) - y;

            default: return 0;
        }
    }

    internal enum GradientType
    {
        FromOneCorner,
        FromTwoCorners,
        CornerShaped
    }
}