using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
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

namespace WPF_JSON
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Car> cars = new List<Car>();
        private List<Car> filterCar;
        public MainWindow()
        {
            InitializeComponent();

            using (var client = new HttpClient())
            {
                string json = client.GetStringAsync("https://timmyluong11.github.io/JamJamTestJSON/RandCar3.json").Result;
                List<Car> c = JsonConvert.DeserializeObject<List<Car>>(json);

                foreach (var item in c)
                {
                    cars.Add(item);
                }
            }
            PopulateComboBox();
        }

        private void PopulateComboBox()
        {
            ComboBoxMake.Items.Add("All");
            ComboBoxMake.SelectedIndex = 0;
            foreach (var item in cars)
            {
                if (!ComboBoxMake.Items.Contains(item.CarMake))
                {
                    ComboBoxMake.Items.Add(item.CarMake);
                }
            }

            ComboBoxColor.Items.Add("All");
            ComboBoxColor.SelectedIndex = 0;
            foreach (var item in cars)
            {
                if (!ComboBoxColor.Items.Contains(item.CarColor))
                {
                    ComboBoxColor.Items.Add(item.CarColor);
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            filterCar = FilterMakeComboBox(cars);
            filterCar = FilterColorComboBox(filterCar);
            filterCar = FilterYear(cars);
            filterCar = FilterMileage(filterCar);
            PopulateListBox(filterCar);
            lableCount.Content = $"Total Car Count: {filterCar.Count}";
            if (filterCar.Count == 0)
            {
                MessageBox.Show("No car found. Please try agian!");
            }
        }

        private List<Car> FilterMakeComboBox(List<Car> vehicle)
        {
            string make = ComboBoxMake.Text;
            List<Car> cv = new List<Car>();
            foreach (var item in vehicle)
            {
                if (make == "all")
                {
                    cv.Add(item);
                }
                else if (item.CarMake.Contains(make))
                {
                    cv.Add(item);
                }
            }
            return cv;
        }

        private List<Car> FilterColorComboBox(List<Car> vehicle)
        {
            string color = ComboBoxColor.Text;
            List<Car> cv = new List<Car>();
            foreach (var item in vehicle)
            {
                if (color == "all")
                {
                    cv.Add(item);
                }
                else if (item.CarColor.Contains(color))
                {
                    cv.Add(item);
                }
            }
            return cv;
        }

        private List<Car> FilterYear(List<Car> vehicle)
        {
            int year = 0;
            string condition = TextBoxYearConditions.Text.Trim();
            List<Car> cv = new List<Car>();

            if (string.IsNullOrEmpty(TextBoxYear.Text))
            {
                MessageBox.Show("You did not enter in anything. Please try again!");
            }
            else if (int.TryParse(TextBoxYear.Text,out year) == false)
            {
                MessageBox.Show("You did not enter in a number. Please try again!");
            }

            foreach (var item in vehicle)
            {
                if (condition == "=")
                {
                    if (year == item.Year)
                    {
                        cv.Add(item);
                    }
                }
                else if (condition == "<=")
                {
                    if (year <= item.Year)
                    {
                        cv.Add(item);
                    }
                }
                else if (condition == ">=")
                {
                    if (year >= item.Year)
                    {
                        cv.Add(item);
                    }
                }
                else
                {
                    MessageBox.Show("You did not enter in a condition");
                    break;
                }
            }
            return cv;
        }

        private List<Car> FilterMileage(List<Car> vehicle)
        {
            int mileage = 0;
            string condition = TextBoxMileageCondition.Text.Trim();
            List<Car> cv = new List<Car>();

            if (string.IsNullOrEmpty(TextBoxMileage.Text))
            {
                MessageBox.Show("You did not enter in anything. Please try again!");
            }
            else if (int.TryParse(TextBoxMileage.Text, out mileage) == false)
            {
                MessageBox.Show("You did not enter in a number. Please try again!");
            }

            foreach (var item in vehicle)
            {
                if (condition == "=")
                {
                    if (mileage == item.Year)
                    {
                        cv.Add(item);
                    }
                }
                else if (condition == "<=")
                {
                    if (mileage <= item.Year)
                    {
                        cv.Add(item);
                    }
                }
                else if (condition == ">=")
                {
                    if (mileage >= item.Year)
                    {
                        cv.Add(item);
                    }
                }
                else
                {
                    MessageBox.Show("You did not enter in a condition");
                    break;
                }
            }
            return cv;
        }

        private void PopulateListBox(List<Car> vehicle)
        {
            ListBoxCar.Items.Clear();
            foreach (var item in vehicle)
            {
                ListBoxCar.Items.Add(item);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string jSON = JsonConvert.SerializeObject(filterCar, Formatting.Indented);
            File.WriteAllText("Car.json", jSON);
            MessageBox.Show("Success");
        }

        private void ListBoxCar_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Car vroom = (Car)ListBoxCar.SelectedItem;
            DetailedWindow win = new DetailedWindow();
            win.SetupWindow(vroom);
            win.ShowDialog();
        }
    }
}
