using System;
using System.Threading.Tasks;
using Android.App;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Xama.JTPorts.AnimatedCircleLoadingView;

namespace SampleApp
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private AnimatedCircleLoadingView animatedCircleLoadingView;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            animatedCircleLoadingView = FindViewById<AnimatedCircleLoadingView>(Resource.Id.circle_loading_view);
            animatedCircleLoadingView.MainColor = Resource.Color.colorPrimary;
            animatedCircleLoadingView.SecondaryColor = Resource.Color.colorAccent;
            animatedCircleLoadingView.TextColor = Resource.Color.colorPrimaryDark;
            animatedCircleLoadingView.CheckMarkTintColor = Color.White;
            animatedCircleLoadingView.TitleText = "Loading";
            animatedCircleLoadingView.StartDeterminate();

            Task.Run(async () => {
                await Task.Delay(5000);
                RunOnUiThread(() => {
                    animatedCircleLoadingView.SetPercent(40);
                });
                await Task.Delay(5000);
                RunOnUiThread(() => {
                    animatedCircleLoadingView.SetPercent(60);
                });
                await Task.Delay(5000);
                RunOnUiThread(() => {
                    animatedCircleLoadingView.SetPercent(70);
                });
                await Task.Delay(5000);
                RunOnUiThread(() => {
                    animatedCircleLoadingView.SetPercent(100);
                });
            });

            Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            FloatingActionButton fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
            fab.Click += FabOnClick;
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            if (id == Resource.Id.action_settings)
            {
                return true;
            }

            return base.OnOptionsItemSelected(item);
        }

        private void FabOnClick(object sender, EventArgs eventArgs)
        {
            animatedCircleLoadingView.StopOk();
            View view = (View) sender;
            Snackbar.Make(view, "Replace with your own action", Snackbar.LengthLong)
                .SetAction("Action", (Android.Views.View.IOnClickListener)null).Show();
        }
	}
}

