using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TempratureChangeEventExample;

namespace TempratureChangeEventExample
{
    public class TemperatureChangedEventArgs : EventArgs
    {
        public double OldTemperature { get; set; }
        public double NewTemperature { get; set; }
        public double Diffence { get; set; }

        public TemperatureChangedEventArgs(double OldTemperature, double NewTemperature)
        {
            this.OldTemperature = OldTemperature;
            this.NewTemperature = NewTemperature;
            this.Diffence = NewTemperature - OldTemperature;
        }
    }
    public class Thermostat
    {
        public event EventHandler<TemperatureChangedEventArgs> TempratureChanged;
        private double OldTemperature;
        private double CurrentTemperature;
        public void SetTemperature(double NewTemperature)
        {
            if (NewTemperature != CurrentTemperature)
            {
                OldTemperature = CurrentTemperature;
                CurrentTemperature = NewTemperature;
                OnTempratureChanged(OldTemperature, CurrentTemperature);
            }

        }
        void OnTempratureChanged(double OldTemperature, double CurrentTemperature)
        {
            OnTempratureChanged(new TemperatureChangedEventArgs(OldTemperature, CurrentTemperature));
        }
        void OnTempratureChanged(TemperatureChangedEventArgs temperatureChangedEventArgs)
        {
            TempratureChanged?.Invoke(this, temperatureChangedEventArgs);
        }

    }

    public class Display
    {
        public void Subscribe(Thermostat thermostat)
        {
            thermostat.TempratureChanged += HandleTempratureChange;
        }
        public void HandleTempratureChange(object sender, TemperatureChangedEventArgs e)
        {
            Console.WriteLine("\n\nTemprature changed:");
            Console.WriteLine($"Temprature Changed from {e.OldTemperature}°C");
            Console.WriteLine($"Temprature Changed to {e.NewTemperature}°C");
            Console.WriteLine($"Temprature Diffence is {e.Diffence}°C");
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Thermostat thermostat = new Thermostat();
            Display display = new Display();

            display.Subscribe(thermostat);

            thermostat.SetTemperature(25);
            thermostat.SetTemperature(30);
            thermostat.SetTemperature(30);
            thermostat.SetTemperature(30);
            thermostat.SetTemperature(30);
            thermostat.SetTemperature(30);
            thermostat.SetTemperature(30);

            Console.ReadLine();
        }
    }
}
