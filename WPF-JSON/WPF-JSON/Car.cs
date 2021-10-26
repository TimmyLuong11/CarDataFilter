using System;
using System.Collections.Generic;
using System.Text;

namespace WPF_JSON
{
    public class Car
    {
        public string VIN { get; set; }
        public int Year { get; set; }
        public string CarMake { get; set; }
        public string CarColor { get; set; }
        public int Mileage { get; set; }
        public string Picture { get; set; }

        public override string ToString()
        {
            return $"{VIN}";
        }
    }
}
