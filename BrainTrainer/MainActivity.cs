using System;
using System.Collections.Generic;
using Android.App;
using Android.Widget;
using Android.OS;
using Android.Views;

namespace BrainTrainer
{
    [Activity(Label = "BrainTrainer", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private Button _buttonStart;
        private Button _button0;
        private Button _button1;
        private Button _button2;
        private Button _button3;
        private TextView _textViewSum;

        private List<int> _answers=new List<int>(4);
        private int _locationOfCorrectAnswer;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);
            initFields();
        }

        private void initFields()
        {
            _textViewSum = FindViewById<TextView>(Resource.Id.textViewSum);
            _button0 = FindViewById<Button>(Resource.Id.button0);
            _button0.Click+=ButtonOnClick;
            _button1 = FindViewById<Button>(Resource.Id.button1);
            _button1.Click += ButtonOnClick;
            _button2 = FindViewById<Button>(Resource.Id.button2);
            _button2.Click += ButtonOnClick;
            _button3 = FindViewById<Button>(Resource.Id.button3);
            _button3.Click += ButtonOnClick;
            Random rand=new Random();
            int a = rand.Next(21);
            int b = rand.Next(21);
            int incorrectAnswer;
            _textViewSum.Text=a+"+"+b;
            _locationOfCorrectAnswer = rand.Next(0, 4);
            for (int i = 0; i < 4; i++)
            {
                if(i==_locationOfCorrectAnswer)
                    _answers.Add(a+b);
                else
                {
                    incorrectAnswer = rand.Next(41);
                    while (incorrectAnswer==(a+b))
                    {
                        incorrectAnswer = rand.Next(41);
                    }
                    _answers.Add(incorrectAnswer);
                }
            }
            _button0.Text = _answers[0].ToString();
            _button1.Text = _answers[1].ToString();
            _button2.Text = _answers[2].ToString();
            _button3.Text = _answers[3].ToString();

            _buttonStart = FindViewById<Button>(Resource.Id.buttonStart);
            _buttonStart.Click += (sender, args) =>
            {
                
            };
        }

        private void ButtonOnClick(object sender, EventArgs eventArgs)
        {
            Button button = (Button) sender;
            string buttonTag = button.Tag.ToString();
        }
    }
}

