using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Calculator;

namespace SampleAndroidApp
{
    [Activity(Label = "Ray's Calculator", MainLauncher = true)]
    public class MainActivity : Activity
    {
        private enum Operation { Addition, Subtraction, Division, Multiplication };
        private enum LastKeyInput { Digit, Operator, Equal, DecimalPoint, Sign }

        decimal? digitMemory = null;
        decimal? totalMemory = null;

        Operation? operationMemory = null;

        LastKeyInput? lastKeyInput = LastKeyInput.Digit;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            Button button1 = (Button) FindViewById(Resource.Id.button1);
            Button button2 = (Button) FindViewById(Resource.Id.button2);
            Button button3 = (Button) FindViewById(Resource.Id.button3);
            Button button4 = (Button) FindViewById(Resource.Id.button4);
            Button button5 = (Button)FindViewById(Resource.Id.button5);
            Button button6 = (Button)FindViewById(Resource.Id.button6);
            Button button7 = (Button)FindViewById(Resource.Id.button7);
            Button button8 = (Button)FindViewById(Resource.Id.button8);
            Button button9 = (Button)FindViewById(Resource.Id.button9);
            Button button0 = (Button)FindViewById(Resource.Id.button0);
            Button buttonDot = (Button)FindViewById(Resource.Id.buttonDot);
            Button buttonNegative = (Button)FindViewById(Resource.Id.buttonNegative);

            EditText resultText = (EditText)FindViewById(Resource.Id.resultText);

            Button buttonAdd = (Button)FindViewById(Resource.Id.buttonAdd);
            Button buttonSubtract = (Button)FindViewById(Resource.Id.buttonSubtract);
            Button buttonMultiply = (Button)FindViewById(Resource.Id.buttonMultiply);
            Button buttonDivide = (Button)FindViewById(Resource.Id.buttonDivide);
            Button buttonEquals = (Button)FindViewById(Resource.Id.buttonEquals);
            Button buttonClear = (Button)FindViewById(Resource.Id.buttonClear);

            buttonNegative.Click += delegate
            {
                //handles if negative sign is the first input after calculation
                if (lastKeyInput != LastKeyInput.Digit && lastKeyInput != LastKeyInput.DecimalPoint && lastKeyInput != LastKeyInput.Sign)
                {
                    resultText.Text = "-";
                    lastKeyInput = LastKeyInput.Sign;
                    return;
                }
                //handles multiple negative sign
                if (!resultText.Text.Contains("-"))
                {
                    resultText.Text = "-" + digitMemory.ToString();
                    lastKeyInput = LastKeyInput.Sign;
                }

            };
            buttonDot.Click += delegate
            {
                //handles if decimal point is the first input after calculation
                if (lastKeyInput != LastKeyInput.Digit && lastKeyInput != LastKeyInput.DecimalPoint && lastKeyInput != LastKeyInput.Sign)
                {
                    resultText.Text = ".";
                    lastKeyInput = LastKeyInput.DecimalPoint;
                    return;
                }
                //handles multiple decimal point
                if (!resultText.Text.Contains("."))
                {
                    resultText.Text = digitMemory.ToString() + ".";
                    lastKeyInput = LastKeyInput.DecimalPoint;
                }

            };
            button1.Click += delegate
            {
                //Renders Text on Screen
                RenderCurrentValue(resultText.Text, "1");
                resultText.Text = digitMemory.ToString();

                //Perform calculation based on current operator
                Calculate();
                lastKeyInput = LastKeyInput.Digit;
            };

            button2.Click += delegate
            {
                RenderCurrentValue(resultText.Text, "2");
                resultText.Text = digitMemory.ToString();
                Calculate();
                lastKeyInput = LastKeyInput.Digit;
            };

            button3.Click += delegate
            {
                RenderCurrentValue(resultText.Text, "3");
                resultText.Text = digitMemory.ToString();
                Calculate();
                lastKeyInput = LastKeyInput.Digit;
            };

            button4.Click += delegate
            {
                RenderCurrentValue(resultText.Text, "4");
                resultText.Text = digitMemory.ToString();
                Calculate();
                lastKeyInput = LastKeyInput.Digit;
            };

            button5.Click += delegate
            {
                RenderCurrentValue(resultText.Text, "5");
                resultText.Text = digitMemory.ToString();
                Calculate();
                lastKeyInput = LastKeyInput.Digit;
            };

            button6.Click += delegate
            {
                RenderCurrentValue(resultText.Text, "6");
                resultText.Text = digitMemory.ToString();
                Calculate();
                lastKeyInput = LastKeyInput.Digit;
            };

            button7.Click += delegate
            {
                RenderCurrentValue(resultText.Text, "7");
                resultText.Text = digitMemory.ToString();
                Calculate();
                lastKeyInput = LastKeyInput.Digit;
            };

            button8.Click += delegate
            {
                RenderCurrentValue(resultText.Text, "8");
                resultText.Text = digitMemory.ToString();
                Calculate();
                lastKeyInput = LastKeyInput.Digit;
            };

            button9.Click += delegate
            {
                RenderCurrentValue(resultText.Text, "9");
                resultText.Text = digitMemory.ToString();
                Calculate();
                lastKeyInput = LastKeyInput.Digit;
            };

            button0.Click += delegate
            {
                RenderCurrentValue(resultText.Text, "0");
                resultText.Text = digitMemory.ToString();
                Calculate();
                lastKeyInput = LastKeyInput.Digit;
            };

            buttonClear.Click += delegate
            {
                resultText.Text = "";
                ResetMemory();
            };

            buttonAdd.Click += delegate
            {
                if (lastKeyInput == LastKeyInput.Digit || lastKeyInput == LastKeyInput.Operator)
                {
                    operationMemory = Operation.Addition;
                }
                resultText.Text = totalMemory.ToString();
                lastKeyInput = LastKeyInput.Operator;
            };

            buttonSubtract.Click += delegate
            {
                if (lastKeyInput == LastKeyInput.Digit || lastKeyInput == LastKeyInput.Operator)
                {
                    operationMemory = Operation.Subtraction;
                }
                resultText.Text = totalMemory.ToString();
                lastKeyInput = LastKeyInput.Operator;
            };

            buttonMultiply.Click += delegate
            {
                if (lastKeyInput == LastKeyInput.Digit || lastKeyInput == LastKeyInput.Operator)
                {
                    operationMemory = Operation.Multiplication;
                }
                resultText.Text = totalMemory.ToString();
                lastKeyInput = LastKeyInput.Operator;
            };

            buttonDivide.Click += delegate
            {
                if (lastKeyInput == LastKeyInput.Digit || lastKeyInput == LastKeyInput.Operator)
                {
                    operationMemory = Operation.Division;
                }
                resultText.Text = totalMemory.ToString();
                lastKeyInput = LastKeyInput.Operator;
            };

            buttonEquals.Click += delegate
            {
                lastKeyInput = LastKeyInput.Equal;
                resultText.Text = totalMemory.ToString();
                ResetMemory();
            };

        }

        private void Calculate()
        {
            if (operationMemory != null)
            {
                switch (operationMemory)
                {
                    case Operation.Addition:
                        if (totalMemory == null)
                        {
                            //Handles first entry
                            totalMemory = digitMemory;
                        }
                        else
                        {
                            totalMemory = totalMemory + digitMemory;
                        }
                        lastKeyInput = LastKeyInput.Operator;
                        break;

                    case Operation.Subtraction:
                        if (totalMemory == null)
                        {
                            //Handles first entry
                            totalMemory = digitMemory;
                        }
                        else
                        {
                            totalMemory = totalMemory - digitMemory;
                        }
                        lastKeyInput = LastKeyInput.Operator;
                        break;

                    case Operation.Multiplication:
                        if (totalMemory == null)
                        {
                            //Handles first entry
                            totalMemory = digitMemory;
                        }
                        else
                        {
                            totalMemory = totalMemory * digitMemory;
                        }
                        lastKeyInput = LastKeyInput.Operator;
                        break;

                    case Operation.Division:
                        if (totalMemory == null)
                        {
                            //Handles first entry
                            totalMemory = digitMemory;
                        }
                        else
                        {
                            totalMemory = totalMemory / digitMemory;
                        }
                        lastKeyInput = LastKeyInput.Operator;
                        break;
                }
            }
            else
            {

                totalMemory = digitMemory;

            }
        }

        private void RenderCurrentValue(string currentRenderedValue, string character)
        {
            //display multiple digits
            if (lastKeyInput == LastKeyInput.Digit || lastKeyInput == LastKeyInput.DecimalPoint || lastKeyInput == LastKeyInput.Sign)
            {
                digitMemory = decimal.Parse(currentRenderedValue + character);
            }
            else
            {
                digitMemory = decimal.Parse(character);
            }
        }

        private void ResetMemory()
        {
            totalMemory = null;
            digitMemory = null;
            operationMemory = null;
            lastKeyInput = LastKeyInput.Digit;
        }
    }

}