using System;
using System.Collections.Generic;
using System.Timers;
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
        private TextView _textViewPoints;
        private TextView _textViewTimer;
        private TextView _textViewResult;
        private GridLayout _gridLayoutAllAnswers;
       
        private int _locationOfCorrectAnswer;
        private int _numberOfQuestions = 0;
        private int _numberOfCorrectAnswers = 0;

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
            _textViewPoints = FindViewById<TextView>(Resource.Id.textViewPoints);
            _textViewTimer= FindViewById<TextView>(Resource.Id.textViewTimer);
            _textViewResult = FindViewById<TextView>(Resource.Id.textViewResult);
            _gridLayoutAllAnswers = FindViewById<GridLayout>(Resource.Id.gridLayoutAllAnswers);
            _button0 = FindViewById<Button>(Resource.Id.button0);
            _button0.Click+=ButtonOnClick;
            _button1 = FindViewById<Button>(Resource.Id.button1);
            _button1.Click += ButtonOnClick;
            _button2 = FindViewById<Button>(Resource.Id.button2);
            _button2.Click += ButtonOnClick;
            _button3 = FindViewById<Button>(Resource.Id.button3);
            _button3.Click += ButtonOnClick;
            

            _buttonStart = FindViewById<Button>(Resource.Id.buttonStart);
            _buttonStart.Click +=ButtonStartOnClick;

            
        }

        private void ButtonStartOnClick(object sender, EventArgs eventArgs)
        {
            _buttonStart.Visibility=ViewStates.Invisible;
            _gridLayoutAllAnswers.Visibility=ViewStates.Visible;
            _textViewResult.Visibility=ViewStates.Invisible;

            QuizCountDownTimer quizCountDownTimer=new QuizCountDownTimer(30000+100,1000,this);
            quizCountDownTimer.Start();
            updateQuestionAndAnswer();
        }

        private void ButtonOnClick(object sender, EventArgs eventArgs)
        {
            _numberOfQuestions++;
            Button button = (Button) sender;
            string buttonTag = button.Tag.ToString();
            if (buttonTag == _locationOfCorrectAnswer.ToString())
            {
                _numberOfCorrectAnswers++;
            }
            _textViewPoints.Text = _numberOfCorrectAnswers + "/" + _numberOfQuestions;

            updateQuestionAndAnswer();
        }

        private void updateQuestionAndAnswer()
        {
            Random rand = new Random();
            List<int> answers = new List<int>(4);
            int a = rand.Next(21);
            int b = rand.Next(21);
            _textViewSum.Text = a + "+" + b;
            _locationOfCorrectAnswer = rand.Next(0, 4);
            for (int i = 0; i < 4; i++)
            {
                if (i == _locationOfCorrectAnswer)
                    answers.Add(a + b);
                else
                {
                    var incorrectAnswer = rand.Next(41);
                    while (incorrectAnswer == (a + b))
                    {
                        incorrectAnswer = rand.Next(41);
                    }
                    answers.Add(incorrectAnswer);
                }
            }
            _button0.Text = answers[0].ToString();
            _button1.Text = answers[1].ToString();
            _button2.Text = answers[2].ToString();
            _button3.Text = answers[3].ToString();
        }

        public void SetTimerText(int remainingTime)
        {
            _textViewTimer.Text = remainingTime + "S";
        }

        public void TimeOver()
        {
            _buttonStart.Visibility = ViewStates.Visible;
            _gridLayoutAllAnswers.Visibility = ViewStates.Invisible;

            _textViewResult.Text = "You answered " + _numberOfCorrectAnswers + " out of " + _numberOfQuestions +
                                   " questions.";
            _textViewResult.Visibility=ViewStates.Visible;

        }
    }

    public class QuizCountDownTimer : CountDownTimer
    {
        private MainActivity _mainActivity;
        public QuizCountDownTimer(long millisInFuture, long countDownInterval) : base(millisInFuture, countDownInterval)
        {
        }
        public QuizCountDownTimer(long millisInFuture, long countDownInterval, MainActivity mainActivity) : base(millisInFuture, countDownInterval)
        {
            _mainActivity = mainActivity;
        }

        public override void OnFinish()
        {
            if (_mainActivity == null)
                return;

            _mainActivity.SetTimerText(0);
            _mainActivity.TimeOver();


        }

        public override void OnTick(long millisUntilFinished)
        {
            if (_mainActivity == null)
                return;
            _mainActivity.SetTimerText((int)(millisUntilFinished / 1000));
            

        }
    }
}

