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

            //TODO: Make methods able to parse strings to code (System.Linq.Expressions)
            //Can configure methods here
            Func<Decimal, Decimal> f = (X) => 8 * Integral.Power(X, 2) - Integral.Power(X, 5);
            Func<Decimal, Decimal, Decimal> If = (L, U) => (((Decimal)8 / 3) * Integral.Power(U, 3) - (Integral.Power(U, 6) / 6)) - (((Decimal)8 / 3) * Integral.Power(L, 3) - (Integral.Power(L, 6) / 6));

            Integral integral = null;

            try {

                Decimal Lower = Decimal.Parse(LowerLimit.Text), Upper = Decimal.Parse(UpperLimit.Text), N = Decimal.Parse(NumberOfRectangles.Text);

                if (Lower > Upper) throw new InvalidOperationException("Lower bound > Upper Bound");

                switch (RuleSelectionBox.SelectedIndex) {

                    case 0:
                        integral = new Midpoint(f, Lower, Upper, N);
                        break;
                    case 1:
                        integral = new Trapezoid(f, Lower, Upper, N);
                        break;
                    case 2:
                        integral = new Simpson(f, Lower, Upper, N);
                        break;
                    case 3:
                        ResultText.Text = Math.Round(If(Lower, Upper), 20).ToString();
                        return;
                    default:
                        throw new Exception();
                }

                ResultText.Text = integral.ToString();

            } catch (Exception E) {
                ResultText.Text = E.Message.Equals("Lower bound > Upper Bound") ? E.Message : "Error: Bad Input";
            }
        }

    }

    public abstract class Integral {

        protected Decimal LowerLimit;
        protected Decimal UpperLimit;
        protected Decimal Number;
        protected Decimal Result;
        protected Decimal Actual;
        protected Decimal Error;
        protected Func<Decimal, Decimal> FofX;
        //TODO implement IntegralF
        protected Func<Decimal, Decimal> IntF;

        public Integral (Func<Decimal, Decimal> X, Decimal L, Decimal U, Decimal N) {
            this.FofX = X;
            LowerLimit = L;
            UpperLimit = U;
            Number = N;
            Result = Calculate();
            Actual = IntegralF();
            Error = PercentError();
        }

        public Integral (Integral I) : this(I.FofX, I.LowerLimit, I.UpperLimit, I.Number) { }

        abstract protected Decimal Calculate ();

        public override string ToString () {
            return String.Format("{0} | {1}% Error", Math.Round(Result, 8), Math.Round(Error, 4));
        }

        public static Decimal operator * (Decimal D, Integral X) {
            return X.Result * D;
        }

        public static Decimal operator + (Decimal D, Integral X) {
            return X.Result + D;
        }

        public static Decimal Power (Decimal Base, int P) {
            if (P == 0) return 1;
            if (Base == 0) return 0;
            return P % 2 == 0 ? Power(Base * Base, P / 2) : Base * Power(Base * Base, P / 2);
        }

        protected Decimal F (Decimal X) {
            return this.FofX(X);
        }

        public Decimal IntegralF () {
            return (((Decimal)8 / 3) * Power(UpperLimit, 3) - (Power(UpperLimit, 6) / 6)) - (((Decimal)8 / 3) * Power(LowerLimit, 3) - (Power(LowerLimit, 6) / 6));
        }

        protected Decimal PercentError () {
            return (Math.Abs(Result - Actual) / Actual) * 100;
        }

    }

    public class Midpoint : Integral {

        public Midpoint (Func<Decimal, Decimal> X, Decimal L, Decimal U, Decimal N) : base(X, L, U, N) { }

        public Midpoint (Integral I) : base(I) { }

        protected override Decimal Calculate () {
            Decimal DeltaX = (UpperLimit - LowerLimit) / Number, LastX = LowerLimit, Sum = 0;

            for (Decimal i = LowerLimit; i <= UpperLimit; i += DeltaX) {
                Sum += F((LastX + i) / 2);
                LastX = i;
            }
            return (DeltaX) * Sum;
        }

    }

    public class Trapezoid : Integral {

        public Trapezoid (Func<Decimal, Decimal> X, Decimal L, Decimal U, Decimal N) : base(X, L, U, N) { }

        public Trapezoid (Integral I) : base(I) { }

        protected override Decimal Calculate () {
            Decimal DeltaX = (UpperLimit - LowerLimit) / Number, Sum = 0;

            for (Decimal i = LowerLimit; i <= UpperLimit; i += DeltaX) {
                Sum += 2 * F(i);
            }
            return (DeltaX / 2) * Sum;

        }

    }

    public class Simpson : Integral {

        public Simpson (Func<Decimal, Decimal> X, Decimal L, Decimal U, Decimal N) : base(X, L, U, N) { }

        public Simpson (Integral I) : base(I) { }

        protected override Decimal Calculate () {

            return (2 * new Midpoint(this) + new Trapezoid(this)) / 3;

        }

    }

}