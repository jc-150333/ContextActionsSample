using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace ContextActionsSample
{
	public partial class App : Application
	{
		public App ()
		{
			MainPage = new MyPage();
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}

    class MyPage : ContentPage
    {
        private ObservableCollection<string> _ar = new ObservableCollection<string>(Enumerable.Range(0, 50).Select(n => "item-" + n));

        public MyPage()
        {
            var listView = new ListView
            {
                //ItemsSource = Enumerable.Range(0, 50).Select(n => "item-" + n),
                ItemsSource = _ar,
                ItemTemplate = new DataTemplate(() => new MyCell(this)),
            };
            Padding = new Thickness(0, Device.OnPlatform(20, 0, 0), 0, 0);
            Content = listView;
        }

        public async void Action(MenuItem item)
        {
            var text = item.CommandParameter.ToString();
            if(item.Text == "Add")
            {
                _ar.Insert(_ar.IndexOf(text) + 1, text + "-Add");
            }
            else if(item.Text == "Delete")
            {
                _ar.RemoveAt(_ar.IndexOf(text));
            }
        }
    }

    class MyCell : ViewCell
    {
        public MyCell(MyPage myPage)
        {
            var label = new Label
            {
                VerticalOptions = LayoutOptions.CenterAndExpand,
            };
            label.SetBinding(Label.TextProperty, new Binding("."));

            var actionDelete = new MenuItem
            {
                Text = "Delete",
                Command = new Command(p => myPage.DisplayAlert("Delete", p.ToString(), "OK")),
                IsDestructive = true,
            };

            actionDelete.SetBinding(MenuItem.CommandParameterProperty, new Binding("."));
            actionDelete.Clicked += (s, a) => myPage.Action((MenuItem)s);
            ContextActions.Add(actionDelete);

            var actionAdd = new MenuItem
            {
                Text = "Add",
            };

            actionAdd.SetBinding(MenuItem.CommandParameterProperty, new Binding("."));

            //actionAdd.Clicked += async (s, e) =>
            //{
            //    var itemMenu = ((MenuItem)s);
            //    await myPage.DisplayAlert(itemMenu.Text, (string)itemMenu.CommandParameter, "OK");
            //};

            actionAdd.Clicked += (s, a) => myPage.Action((MenuItem)s);
            ContextActions.Add(actionAdd);

            View = new StackLayout
            {
                Padding = 10,
                Children = { label }
            };
        }
    }
}
