namespace catch_up_mobile.Components
{
    using CommunityToolkit.Maui.Views;
    using Microsoft.Maui.Controls;

    public class ConfirmPopup : Popup
    {
        public ConfirmPopup(string message, Action<bool> onConfirmation)
        {
            Content = new VerticalStackLayout
            {
                Padding = new Thickness(20),
                BackgroundColor = Color.FromRgba("#22272b"),
                Spacing = 15,
                Children =
            {
                new Label
                {
                    Text = message,
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center,
                    FontSize = 18,
                    TextColor = Colors.White
                },
                new HorizontalStackLayout
                {
                    Spacing = 10,
                    HorizontalOptions = LayoutOptions.Center,
                    Children =
                    {
                        new Button
                        {
                            WidthRequest = 100,
                            Text = "No",
                            BackgroundColor = Color.FromRgba("#dc3545"),
                            TextColor = Color.FromRgba("ffffff"),
                            Command = new Command(() =>
                            {
                                onConfirmation(false);
                                Close();
                            })
                        },
                        new Button
                        {
                            WidthRequest = 100,
                            Text = "Yes",
                            BackgroundColor = Color.FromRgba("#198754"),
                            TextColor = Color.FromRgba("ffffff"),
                            Command = new Command(() =>
                            {
                                onConfirmation(true);
                                Close();
                            })
                        }
                    }
                }
            }
            };
        }
    }

}
