using System;
using System.Text;
using System.Windows;
using System.Xml;
//using System.Linq.Expressions;
//using System.Linq.Dynamic;
using System.ComponentModel;
using System.Collections.Generic;

namespace NumericalIntegrationGUI {

    public partial class MainWindow : Window {

        public MainWindow () {
            InitializeComponent();
        }

        //Can configure methods here
        public Func<Decimal, Decimal, Decimal> If = (L, U) => (((Decimal)8 / 3) * Integral.Power(U, 3) - (Integral.Power(U, 6) / 6)) - (((Decimal)8 / 3) * Integral.Power(L, 3) - (Integral.Power(L, 6) / 6));
        public Func<Decimal, Decimal> f = (X) => 8 * Integral.Power(X, 2) - Integral.Power(X, 5);

        public List<String[]> CalcList = new List<String[]>();

        private void Calculate_Click (object sender, RoutedEventArgs e) {

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
                        break;
                    default:
                        throw new Exception();
                }

                ResultText.Text = RuleSelectionBox.SelectedIndex == 3 ? Math.Round(If(Lower, Upper), 20).ToString() : integral.ToString();

                CalcList.Add(RuleSelectionBox.SelectedIndex == 3 ? new String[] { "Integral", If(Lower, Upper).ToString() } : integral.ToXML());

            } catch (Exception E) {
                ResultText.Text = E.Message.Equals("Lower bound > Upper Bound") ? E.Message : "Error: Bad Input";
            }
        }

        private void Convert_Button_Click (object sender, RoutedEventArgs e) {

            //TODO: Make methods able to parse strings to code (System.Linq.Expressions)
            String Function = InputFunction.Text;

            InputFunction.Text = "Not yet implemented";

        }

        private void OnClose (object sender, CancelEventArgs e) {

            XmlWriterSettings Settings = new XmlWriterSettings();
            Settings.Indent = true;
            Settings.NewLineOnAttributes = true;

            using (XmlWriter XW = XmlWriter.Create("RawData.xml", Settings)) {

                XW.WriteStartDocument();
                XW.WriteStartElement("RawData");

                String[] Terms = { "Lower", "Upper", "Number", "Result", "Error" };

                foreach (String[] Arr in CalcList) {

                    XW.WriteStartElement(Arr[0]);
                    if (Arr[0].Equals("Integral")) {
                        XW.WriteAttributeString("Result", Arr[1]);
                        XW.WriteEndElement();

                    } else {
                        for (int i = 1; i < 6; ++i) {
                            XW.WriteAttributeString(Terms[i - 1], Arr[i]);
                        }
                        XW.WriteEndElement();
                    }
                }
                XW.WriteEndElement();
                XW.WriteEndDocument();
                XW.Flush();
                XW.Close();
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
        //TODO: implement IntegralF
        protected Func<Decimal, Decimal> IntF;
        protected String Rule;

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

        public String[] ToXML () {
            return new string[] { Rule, LowerLimit.ToString(), UpperLimit.ToString(), Number.ToString(), Result.ToString(), Error.ToString() };
        }

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

        public Midpoint (Func<Decimal, Decimal> X, Decimal L, Decimal U, Decimal N) : base(X, L, U, N) { Rule = "Midpoint"; }

        public Midpoint (Integral I) : base(I) { Rule = "Midpoint"; }

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

        public Trapezoid (Func<Decimal, Decimal> X, Decimal L, Decimal U, Decimal N) : base(X, L, U, N) { Rule = "Trapezoid"; }

        public Trapezoid (Integral I) : base(I) { Rule = "Trapezoid"; }

        protected override Decimal Calculate () {
            Decimal DeltaX = (UpperLimit - LowerLimit) / Number, Sum = 0;

            for (Decimal i = 0; i <= Number; ++i) {
                Sum += i == 0 || i == Number ? i == 0 ? F(LowerLimit) : F(UpperLimit) : 2 * F(LowerLimit + i * DeltaX);
            }

            return .5m * DeltaX * Sum;
        }

    }

    public class Simpson : Integral {

        public Simpson (Func<Decimal, Decimal> X, Decimal L, Decimal U, Decimal N) : base(X, L, U, N) { Rule = "Simpson"; }

        public Simpson (Integral I) : base(I) { Rule = "Simpson"; }

        protected override Decimal Calculate () {

            return (2 * new Midpoint(this) + new Trapezoid(this)) / 3;

        }

    }

}