﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace ContextActionsSample
{
	public partial class MyPage2 : ContentPage
    {
        //0～50の文字列
        //private ObservableCollection<string> _ar = new ObservableCollection<string>(Enumerable.Range(0, 50).Select(n => "item-" + n));

        //private ObservableCollection<int> _ar = new ObservableCollection<int>(Enumerable.Range(0, 50).Select(n => n));
        
        //private ObservableCollection<UserModel> _ar = new ObservableCollection<UserModel>(UserModel.selectUser());

        private ObservableCollection<UserModel> _ar;

        public MyPage2()
        {
            UserModel.insertUser("鈴木");

            if (UserModel.selectUser() != null)
            {
                _ar = new ObservableCollection<UserModel>(UserModel.selectUser());
            }

            var listView = new ListView
            {
                //ItemsSource = Enumerable.Range(0, 50).Select(n => "item-" + n),
                ItemsSource = _ar,
                //ItemTemplate = new DataTemplate(() => new MyCell2(this)),
            };

            //文字入力
            var entry = new Entry
            {
                HorizontalOptions = LayoutOptions.FillAndExpand
            };
            //追加
            var buttonAdd = new Button
            {
                WidthRequest = 60,
                TextColor = Color.Black,
                Text = "Add"
            };
            buttonAdd.Clicked += (s, a) =>
            {//追加ボタンの処理
                if (!String.IsNullOrEmpty(entry.Text))
                {
                    //UserModel.insertUser(entry.Text);

                    _ar.Add(new UserModel { Name = entry.Text });

                    //id++;

                    //Application.Current.MainPage = new MainPage4();

                    //entry.Text = "";
                }
            };

            Content = new StackLayout
            {
                Padding = new Thickness(0, Device.OnPlatform(20, 0, 0), 0, 0),
                Children =
                    {
                        new StackLayout
                        {
                            BackgroundColor = Color.HotPink,
                            Padding = 5,
                            Orientation = StackOrientation.Horizontal,
                            Children = {entry,buttonAdd}//Entryコントロールとボタンコントロールを配置
                        },
                        listView//その下にリストボックス
                    }

            };
            //Padding = new Thickness(0, Device.OnPlatform(20, 0, 0), 0, 0);
            //Content = listView;

        }

        public async void Action(MenuItem item)
        {
            var text = item.CommandParameter;
            //if (item.Text == "Add")
            //{
            //    _ar.Insert(_ar.IndexOf(text) + 1, text + "-Add");
            //}
            if (item.Text == "Delete")
            {
                _ar.RemoveAt(_ar.IndexOf(text));
            }
        }
    }

    class MyCell2 : ViewCell
    {
        public MyCell2(MyPage2 myPage)
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
