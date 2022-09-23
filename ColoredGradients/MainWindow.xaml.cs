using System.Threading;
using System.Windows;

namespace ColoredGradients;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        Thread thread = new(() => StartDrawing());
        thread.SetApartmentState(ApartmentState.STA);

        thread.Start();
    }

    private void StartDrawing()
    {
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
}