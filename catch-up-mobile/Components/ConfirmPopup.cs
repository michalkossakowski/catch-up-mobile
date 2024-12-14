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
                BackgroundColor = Colors.White,
                Spacing = 15,
                Children =
            {
                new Label
                {
                    Text = message,
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center,
                    FontSize = 18,
                    TextColor = Colors.Black
                },
                new HorizontalStackLayout
                {
                    Spacing = 10,
                    HorizontalOptions = LayoutOptions.Center,
                    Children =
                    {
                        new Button
                        {
                            Text = "Yes",
                            Command = new Command(() =>
                            {
                                onConfirmation(true);
                                Close();
                            })
                        },
                        new Button
                        {
                            Text = "No",
                            Command = new Command(() =>
                            {
                                onConfirmation(false);
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
