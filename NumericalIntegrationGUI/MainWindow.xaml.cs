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

            try {

                Decimal Answer, Lower = Decimal.Parse(LowerLimit.Text), Upper = Decimal.Parse(UpperLimit.Text), N = Decimal.Parse(NumberOfRectangles.Text);

                if (Lower > Upper) throw new InvalidOperationException("Lower bound > Upper Bound");

                switch (RuleSelectionBox.SelectedIndex) {

                    case 0:
                        Answer = NumericalIntegration.Midpoint(Lower, Upper, N);
                        ResultText.Text = String.Format("{0} | {1}% Error", Math.Round(Answer, 8), Math.Round(NumericalIntegration.PercentError(Answer, NumericalIntegration.IntegralF(Lower, Upper)), 4));
                        break;
                    case 1:
                        Answer = NumericalIntegration.Trapezoid(Lower, Upper, N);
                        ResultText.Text = String.Format("{0} | {1}% Error", Math.Round(Answer, 8), Math.Round(NumericalIntegration.PercentError(Answer, NumericalIntegration.IntegralF(Lower, Upper)), 4));
                        break;
                    case 2:
                        Answer = NumericalIntegration.Simpson(Lower, Upper, N);
                        ResultText.Text = String.Format("{0} | {1}% Error", Math.Round(Answer, 8), Math.Round(NumericalIntegration.PercentError(Answer, NumericalIntegration.IntegralF(Lower, Upper)), 4));
                        break;
                    case 3:
                        ResultText.Text = Math.Round(NumericalIntegration.IntegralF(Lower, Upper), 24).ToString();
                        break;
                    default:
                        throw new Exception();
                }

            } catch (Exception E) {
                ResultText.Text = E.Message.Equals("Lower bound > Upper Bound") ? E.Message : "Error: Bad Input";
            }
        }

    }

    class NumericalIntegration {

        private static Decimal Power (Decimal Base, int P) {

            if (P.Equals(0)) return 1;
            if (Base.Equals(0)) return 0;

            if (P % 2 == 0) {
                return Power(Base * Base, P / 2);
            } else {
                return Base * Power(Base * Base, P / 2);
            }
        }

        private static Decimal F (Decimal X) {
            return 8 * Power(X, 2) - Power(X, 5);
        }

        public static Decimal IntegralF (Decimal LowerBound, Decimal UpperBound) {
            return (((Decimal)8 / 3) * Power(UpperBound, 3) - (Power(UpperBound, 6) / 6)) - (((Decimal)8 / 3) * Power(LowerBound, 3) - (Power(LowerBound, 6) / 6));
        }

        public static Decimal PercentError (Decimal Experimental, Decimal Actual) {
            return (Math.Abs(Experimental - Actual) / Actual) * 100;
        }

        public static Decimal Midpoint (Decimal LowerBound, Decimal UpperBound, Decimal N) {
            Decimal DeltaX = (UpperBound - LowerBound) / N, LastX = LowerBound, Sum = 0;

            for (Decimal i = LowerBound; i <= UpperBound; i += DeltaX) {
                Sum += F((LastX + i) / 2);
                LastX = i;
            }
            return (DeltaX) * Sum;
        }

        public static Decimal Trapezoid (Decimal LowerBound, Decimal UpperBound, Decimal N) {
            Decimal DeltaX = (UpperBound - LowerBound) / N, Sum = 0;

            for (Decimal i = LowerBound; i <= UpperBound; i += DeltaX) {
                Sum += 2 * F(i);
            }
            return (DeltaX / 2) * Sum;
        }

        public static Decimal Simpson (Decimal LowerBound, Decimal UpperBound, Decimal N) {
            return (2 * Midpoint(LowerBound, UpperBound, N) + Trapezoid(LowerBound, UpperBound, N)) / 3;
        }

    }
}