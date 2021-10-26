using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WPF_JSON
{
    /// <summary>
    /// Interaction logic for DetailedWindow.xaml
    /// </summary>
    public partial class DetailedWindow : Window
    {
        public DetailedWindow()
        {
            InitializeComponent();
        }

        public void SetupWindow(Car vroom)
        {
            this.Title = vroom.CarMake;
            txtColor.Text = vroom.CarColor;
            txtMake.Text = vroom.CarMake;
            txtMileage.Text = Convert.ToString(vroom.Mileage);
            txtYear.Text = Convert.ToString(vroom.Year);
            ImgBox.Source = new BitmapImage(new Uri(vroom.Picture));
        }
    }
}
