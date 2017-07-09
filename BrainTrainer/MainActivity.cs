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

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);
            initFields();
        }

        private void initFields()
        {
            _buttonStart = FindViewById<Button>(Resource.Id.buttonStart);
            _buttonStart.Click += (sender, args) =>
            {
                
            };
        }
    }
}

