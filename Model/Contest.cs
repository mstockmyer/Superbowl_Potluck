using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SB_MAUI.Model
{
    public class ContestEntry : ObservableViewModelBase
    {
        /// <summary>
        /// Name of the Team
        /// </summary>
        private string _Name;
        public string Name 
        {
            get => _Name;
            set => SetProperty(ref _Name, value);
        }

        /// <summary>
        /// Distance between coin A and coin B
        /// </summary>
        private double _A_to_B;
        public double A_to_B
        {
            get => _A_to_B;
            set
            {
                SetProperty(ref _A_to_B, value);
                RaisePropertyChanged("Area");
            }
        }


        /// <summary>
        /// Distance between coin B and coin C
        /// </summary>
        private double _B_to_C;
        public double B_to_C
        {
            get => _B_to_C;
            set
            {
                SetProperty(ref _B_to_C, value);
                RaisePropertyChanged("Area");
                
            }
        }


        /// <summary>
        /// Distance between coin C and coin A
        /// </summary>
        private double _C_to_A;
        public double C_to_A
        {
            get => _C_to_A;
            set
            {
                SetProperty(ref _C_to_A, value);
                RaisePropertyChanged("Area");
            }
        }

        public double Area
        {
            get => GetArea();
        }

        /// <summary>
        /// Calculate the are of the Triangle based on th A-B-C edge lengths
        /// in this object using Heron's Formula:
        /// https://www.mathsisfun.com/geometry/herons-formula.html
        /// </summary>
        /// <returns>Area of the triangle</returns>
        public double GetArea()
        {
            try
            {
                double area = 0;

                double SemiPerimeter = (A_to_B + B_to_C + C_to_A) / 2.0;

                double temp = SemiPerimeter * (SemiPerimeter - A_to_B) * (SemiPerimeter - B_to_C) * (SemiPerimeter - C_to_A);

                area = Math.Sqrt(temp);

                // Round to three decimal places. . . that should be good enough.
                return Math.Round(area,3);

            }
            catch
            {
                return 0;
            }


        }



    }


    public abstract class ObservableViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged(string propertyName)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        /// <summary>
        /// Set a property and raise a property changed event if it has changed
        /// </summary>
        protected bool SetProperty<T>(ref T property, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(property, value))
            {
                return false;
            }

            property = value;
            RaisePropertyChanged(propertyName);
            return true;
        }
    }
}
