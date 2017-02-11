using System;
using System.Text;
using System.Windows;

namespace NumericalIntegrationGUI {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow () {
            InitializeComponent();
        }

        private void Calculate_Click (object sender, RoutedEventArgs e) {
            StringBuilder SB = new StringBuilder();
            SB.Clear();

            double Answer, Lower = Double.Parse(LowerLimit.Text), Upper = Double.Parse(UpperLimit.Text), N = Double.Parse(NumberOfRectangles.Text);

            if (RuleSelectionBox.SelectedIndex.Equals(0)) {
                Answer = NumericalIntegration.Midpoint(Lower, Upper, N);
                SB.AppendFormat("{0} | {1}% Error", Math.Round(Answer, 10), Math.Round(NumericalIntegration.PercentError(Answer, NumericalIntegration.IntegralF(Lower, Upper)), 3));
                ResultText.Text = SB.ToString();
            } else if (RuleSelectionBox.SelectedIndex.Equals(1)) {
                Answer = NumericalIntegration.Trapezoid(Lower, Upper, N);
                SB.AppendFormat("{0} | {1}% Error", Math.Round(Answer, 10), Math.Round(NumericalIntegration.PercentError(Answer, NumericalIntegration.IntegralF(Lower, Upper)), 3));
                ResultText.Text = SB.ToString();
            } else if (RuleSelectionBox.SelectedIndex.Equals(2)) {
                Answer = NumericalIntegration.Simpson(Lower, Upper, N);
                SB.AppendFormat("{0} | {1}% Error", Math.Round(Answer, 10), Math.Round(NumericalIntegration.PercentError(Answer, NumericalIntegration.IntegralF(Lower, Upper)), 3));
                ResultText.Text = SB.ToString();
            } else if (RuleSelectionBox.SelectedIndex.Equals(3)){
                ResultText.Text = NumericalIntegration.IntegralF(Lower, Upper).ToString();
            } else {
                ResultText.Text = "Please select a rule";
            }
        }
    }

    class NumericalIntegration {

        private static double F (double X) {
            return 8 * Math.Pow(X, 2) - Math.Pow(X, 5);
        }

        public static double IntegralF (double LowerBound, double UpperBound) {
            return ((8.0 / 3.0) * Math.Pow(UpperBound, 3.0) - (Math.Pow(UpperBound, 6.0) / 6.0)) - ((8.0 / 3.0) * Math.Pow(LowerBound, 3.0) - (Math.Pow(LowerBound, 6.0) / 6.0));
        }

        public static double PercentError (double Experimental, double Actual) {
            return ((Experimental - Actual) / Actual) * 100;
        }

        public static double Midpoint (double LowerBound, double UpperBound, double N) {
            double DeltaX = (UpperBound - LowerBound) / N, LastX = LowerBound, Sum = 0;

            for (double i = LowerBound; i <= UpperBound; i += DeltaX) {
                Sum += F((LastX + i) / 2);
                LastX = i;
            }
            return (DeltaX) * Sum;
        }

        public static double Trapezoid (double LowerBound, double UpperBound, double N) {
            double DeltaX = (UpperBound - LowerBound) / N, Sum = 0;

            for (double i = LowerBound; i <= UpperBound; i += DeltaX) {
                Sum += 2 * F(i);
            }
            return (DeltaX / 2) * Sum;
        }

        public static double Simpson (double LowerBound, double UpperBound, double N) {
            return (2 * Midpoint(LowerBound, UpperBound, N) + Trapezoid(LowerBound, UpperBound, N)) / 3;
        }

    }
}