using System.Windows;

namespace ColoredGradients;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        Drawer.SetDrawingType(Drawer.DrawingType.RandomMultipliedColor);
        Drawer.StartDrawing(Field, GradientGenerator.GradientType.FromOneCorner);
    }

    private void ExitButtonClick(object sender, RoutedEventArgs e)
    {
        Application.Current.Shutdown();
    }

    private void Grid_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        DragMove();
    }

    private void RedrawButtonClick(object sender, RoutedEventArgs e)
    {
        Drawer.Redraw();
    }
}