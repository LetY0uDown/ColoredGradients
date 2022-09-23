using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ColoredGradients;

internal static class Drawer
{
    internal const int CELL_SIZE = 50;
    internal const int GRID_SIZE = 16;

    internal static bool EndedDrawing { get; private set; }

    private const int DELAY = 0;

    private static Canvas _drawingField;

    private static DrawingType _drawingType;
    private static GradientGenerator.GradientType _gradientType;

    private static double _red = 0, _green = 0, _blue = 0;

    internal static void Redraw()
    {
        if (EndedDrawing)
        {
            _drawingField.Children.Clear();
            SetDrawingType(_drawingType, _red, _green, _blue);
            StartDrawing(_drawingField, _gradientType);
        }
    }

    internal static void SetDrawingType(DrawingType type, double red = 0, double green = 0, double blue = 0)
    {
        _drawingType = type;

        _red = red;
        _green = green;
        _blue = blue;

        if (type == DrawingType.RandomMultipliedColor)
            (_red, _green, _blue) = ColorTool.RandomColorMulpitilers();
    }

    internal static void StartDrawing(Canvas field, GradientGenerator.GradientType gradientType)
    {
        EndedDrawing = false;

        Application.Current.MainWindow.Height = CELL_SIZE * GRID_SIZE;
        Application.Current.MainWindow.Width = CELL_SIZE * GRID_SIZE;

        _gradientType = gradientType;

        _drawingField = field;

        Thread thread = new(() => DrawingThread());
        thread.SetApartmentState(ApartmentState.STA);

        thread.Start();
    }

    private static void DrawingThread()
    {
        for (int x = 0; x < GRID_SIZE; x++)
        {
            for (int y = 0; y < GRID_SIZE; y++)
            {
                var shadeIndex = GradientGenerator.GetShadeIndex(x, y, _gradientType);

                Application.Current.Dispatcher.Invoke(() => {
                    var color = GetColor(shadeIndex);
                    DrawRectangle(x * CELL_SIZE, y * CELL_SIZE, color);
                });

                Thread.Sleep(DELAY);
            }
        }

        EndedDrawing = true;
    }

    private static SolidColorBrush GetColor(int shadeIndex)
    {
        return _drawingType switch
        {
            DrawingType.RandomColor => ColorTool.RandomColor,
            DrawingType.GrayShades => ColorTool.GetGreyShadeColor(shadeIndex),
            DrawingType.MultipliedColors or DrawingType.RandomMultipliedColor => ColorTool.GetMultipliedColor(shadeIndex, _red, _green, _blue),
            _ => Brushes.White
        };
    }

    private static void DrawRectangle(int posX, int posY, SolidColorBrush brush)
    {
        Rectangle rectangle = new()
        {
            Width = CELL_SIZE,
            Height = CELL_SIZE,
            Fill = brush
        };
        
        _drawingField.Children.Add(rectangle);
       
        Canvas.SetLeft(rectangle, posX);
        Canvas.SetTop(rectangle, posY);
    }

    internal enum DrawingType
    {
        RandomColor,
        GrayShades,
        MultipliedColors,
        RandomMultipliedColor
    }
}