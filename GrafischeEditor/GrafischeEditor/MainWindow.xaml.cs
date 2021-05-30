using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GrafischeEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }







        bool selectMode = false; // Keeps track of which Tool is selected, default is select

        bool mouseDown = false; // Set to true when mouse is held down
        Point mouseDownPos; // The position of the mouse when clicked down

        SolidColorBrush redBrush = new SolidColorBrush(Color.FromRgb(255,0,0));
        SolidColorBrush yellowBrush = new SolidColorBrush(Color.FromRgb(255,255,0));
        SolidColorBrush blueBrush = new SolidColorBrush(Color.FromRgb(0,0,255));
        SolidColorBrush greenBrush = new SolidColorBrush(Color.FromRgb(0,255,0));

        Shape selectedSelectionArea;

        string selectedColor;

        private enum selectedShape { 
        None, Ellipse, Rectangle
        }
        private selectedShape Shape1 = selectedShape.None;

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e){

            if (!selectMode)
            {
                // Capture and track the mouse
                mouseDown = true;
                mouseDownPos = e.GetPosition(theGrid);
                theGrid.CaptureMouse();
                selectedSelectionArea = null;

                switch (Shape1)
                {
                    case selectedShape.Rectangle:
                        selectedSelectionArea = selectionBox;
                        break;
                    case selectedShape.Ellipse:
                        selectedSelectionArea = selectionEllipse;
                        break;
                    default:
                        return;
                }
                drawSelectionArea(selectedSelectionArea, (mouseDownPos.X, mouseDownPos.Y));
            }
        }



        private void drawSelectionArea(Shape selectionArea, (double X, double Y) mouseDownPos) { 
            // Place drag selection box
            Canvas.SetLeft(selectionArea, mouseDownPos.X);
            Canvas.SetTop(selectionArea, mouseDownPos.Y);
            selectionArea.Width = 0;
            selectionArea.Height = 0;
            selectionArea.Fill = getSelectedColorBrush(selectedColor);

            // Make selection box visible
            selectionArea.Visibility = Visibility.Visible;
        }

        private void Grid_MouseUp(object sender, MouseButtonEventArgs e) {

            if (!selectMode)
            {
                // Release mouse capture and stop tracking
                mouseDown = false;
                theGrid.ReleaseMouseCapture();

                // Hide selection box again
                selectedSelectionArea.Visibility = Visibility.Collapsed;
                selectedSelectionArea = null;

                Point mouseUpPos = e.GetPosition(theGrid);

                // TODO Check what is selected

                double height;
                double width;

                double endPointX = mouseDownPos.X;
                double endPointY = mouseDownPos.Y;

                if (mouseDownPos.X < mouseUpPos.X)
                {
                    width = mouseUpPos.X - mouseDownPos.X;
                }
                else
                {
                    width = mouseDownPos.X - mouseUpPos.X;
                    endPointX = mouseUpPos.X;
                }

                if (mouseDownPos.Y < mouseUpPos.Y)
                {
                    height = mouseUpPos.Y - mouseDownPos.Y;
                }
                else
                {
                    height = mouseDownPos.Y - mouseUpPos.Y;
                    endPointY = mouseUpPos.Y;
                }

                drawShape(Shape1, getSelectedColorBrush(selectedColor), height, width, endPointX, endPointY);
            }
        }



        private void drawShape(selectedShape shape, SolidColorBrush colorBrush, double height, double width, double endPointX, double endPointY) {

            Shape renderShape = null;

            switch (shape)
            {
                case selectedShape.Rectangle:
                    renderShape = new Rectangle()
                    {
                        Height = height,
                        Width = width,
                        Visibility = Visibility.Visible,
                        Fill = getSelectedColorBrush(selectedColor),
                        Stroke = getSelectedColorBrush(selectedColor),
 
                    };
                    
                    break;
                case selectedShape.Ellipse:
                    renderShape = new Ellipse()
                    {
                        Height = height,
                        Width = width,
                        Visibility = Visibility.Visible,
                        Fill = getSelectedColorBrush(selectedColor),
                        Stroke = getSelectedColorBrush(selectedColor)
                    };
                    break;
                case selectedShape.None:
                    Console.WriteLine("Selecting area...");
                    break;
                default:
                    return;            
            }

        Console.WriteLine("Drawing " + Shape1 + " with size " + height + "x" + width + " and color " + selectedColor);


            if (renderShape != null)
            {
                string name = selectedColor + Shape1.ToString();
                // Update size and color
                renderShape.Name = name;
                renderShape.Height = height;
                renderShape.Width = width;
                renderShape.Visibility = Visibility.Visible;
                renderShape.Fill = getSelectedColorBrush(selectedColor);
                renderShape.Stroke = getSelectedColorBrush(selectedColor);

                // Add mousebutton event handlers for dragging/resizing
                renderShape.MouseDown += new MouseButtonEventHandler(shape_MouseDown);
                renderShape.MouseUp += new MouseButtonEventHandler(shape_MouseUp);
                renderShape.MouseMove += new MouseEventHandler(shape_MouseMove);

                // Set position on canvas
                Canvas.SetLeft(renderShape, endPointX);
                Canvas.SetTop(renderShape, endPointY);

                //add to canvas
                canvas.Children.Add(renderShape);

                // Reset renderShape
                renderShape = null;
            }
            else { Console.WriteLine("No shape selected"); }

        }


        protected bool isDragging;
        private Point clickPosition;
        private TranslateTransform originTT;


        private void shape_MouseMove(object sender, MouseEventArgs e)
        {
            Shape selectedshape = sender as Shape;
            if (selectMode && isDragging && selectedshape != null) {
                Point currentPos = e.GetPosition(canvas);
                var transform = selectedshape.RenderTransform as TranslateTransform ?? new TranslateTransform();
                transform.X = originTT.X + (currentPos.X - clickPosition.X);
                transform.Y = originTT.Y + (currentPos.Y - clickPosition.Y);
                selectedshape.RenderTransform = new TranslateTransform(transform.X, transform.Y);  
                Console.WriteLine("Dragging...");
            }
        }

        private void shape_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (selectMode && isDragging)
                isDragging = false;
                Shape selectedshape = sender as Shape;
                selectedshape.ReleaseMouseCapture();
        
                Console.WriteLine("Dropping...");
        }

        private void shape_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (selectMode) {
                Shape selectedshape = sender as Shape;
                Console.WriteLine(selectedshape.Name);
                
                originTT = selectedshape.RenderTransform as TranslateTransform ?? new TranslateTransform();
                isDragging = true;
                clickPosition = e.GetPosition(canvas);
                selectedshape.CaptureMouse();
            }
        }

        private void Grid_MouseMove(object sender, MouseEventArgs e){
            if (mouseDown && selectedSelectionArea != null)
            {
                // When the mouse is held down, reposition the drag selection box.

                Point mousePos = e.GetPosition(theGrid);

                if (mouseDownPos.X < mousePos.X)
                {
                    Canvas.SetLeft(selectedSelectionArea, mouseDownPos.X);
                    selectedSelectionArea.Width = mousePos.X - mouseDownPos.X;
                }
                else
                {
                    Canvas.SetLeft(selectedSelectionArea, mousePos.X);
                    selectedSelectionArea.Width = mouseDownPos.X - mousePos.X;
                }

                if (mouseDownPos.Y < mousePos.Y)
                {
                    Canvas.SetTop(selectedSelectionArea, mouseDownPos.Y);
                    selectedSelectionArea.Height = mousePos.Y - mouseDownPos.Y;
                }
                else
                {
                    Canvas.SetTop(selectedSelectionArea, mousePos.Y);
                    selectedSelectionArea.Height = mouseDownPos.Y - mousePos.Y;
                }
            }
        }



        private void selectEllipse_Click(object sender, RoutedEventArgs e)
        {
            Shape1 = selectedShape.Ellipse;

            updateToolsAndShapes();

            selectEllipse.Background = new SolidColorBrush(Color.FromRgb(119, 136, 153));
        }

        private void selectRectangle_Click(object sender, RoutedEventArgs e)
        {
            Shape1 = selectedShape.Rectangle;

            updateToolsAndShapes();

            selectRectangle.Background = new SolidColorBrush(Color.FromRgb(119, 136, 153));
        }

        private void updateToolsAndShapes() {
            selectEllipse.Background = new SolidColorBrush(Color.FromRgb(211, 211, 211));
            selectRectangle.Background = new SolidColorBrush(Color.FromRgb(211, 211, 211));
            selectSelector.Background = new SolidColorBrush(Color.FromRgb(211, 211, 211));
            selectMode = false;
        }

        private SolidColorBrush getSelectedColorBrush(string selectedColor)
        {
            switch (selectedColor) {
                case "red":
                    return redBrush;
                case "yellow":
                    return yellowBrush;
                case "blue":
                    return blueBrush;
                case "green":
                    return greenBrush;
                default:
                    return redBrush;
            }
        }

        private void updateButtonBorders(Button button) {
            Red.BorderThickness = new Thickness(0,0,0,0);
            Yellow.BorderThickness = new Thickness(0, 0, 0, 0);
            Blue.BorderThickness = new Thickness(0, 0, 0, 0);
            Green.BorderThickness = new Thickness(0, 0, 0, 0);

            button.BorderThickness = new Thickness(1, 1, 1, 1);
        }

        private selectedShape getSelectedShape() {
            return Shape1;
        }

        private void Red_Click(object sender, RoutedEventArgs e)
        {
            selectedColor = "red";
            Button button = sender as Button;
            updateButtonBorders(button);
        }

        private void Yellow_Click(object sender, RoutedEventArgs e)
        {
            selectedColor = "yellow";
            Button button = sender as Button;
            updateButtonBorders(button);
        }

        private void Blue_Click(object sender, RoutedEventArgs e)
        {
            selectedColor = "blue";
            Button button = sender as Button;
            updateButtonBorders(button);
        }

        private void Green_Click(object sender, RoutedEventArgs e)
        {
            selectedColor = "green";
            Button button = sender as Button;
            updateButtonBorders(button);
        }

        private void selectSelector_Click(object sender, RoutedEventArgs e)
        {
            
            updateToolsAndShapes();
            selectSelector.Background = new SolidColorBrush(Color.FromRgb(119, 136, 153));
            Shape1 = selectedShape.None;
            selectMode = true;
        }

        private void selectClear_Click(object sender, RoutedEventArgs e)
        {
            canvas.Children.RemoveRange(2, (canvas.Children.Count - 2));
        }

        private void selectResizer_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
