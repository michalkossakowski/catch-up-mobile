using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Views;
using Microsoft.AspNetCore.Components;

namespace catch_up_mobile.Components.Camera;

public partial class CameraPage : ContentPage
{
    [Parameter]
    public EventCallback<Stream?> OnSendFile { get; set; }
    private Stream stream;

    public CameraPage()
	{
		InitializeComponent();
        UploadButton.IsEnabled = false;
        UploadButton.BackgroundColor = Color.FromRgba("#0e442a");
        UploadButton.TextColor = Color.FromRgba("#a1a8a4");
    }

    private async void GoBack(object sender, EventArgs e)
    {
        await OnSendFile.InvokeAsync(null);
        await App.Current.MainPage.Navigation.PopModalAsync();
    }

    private async void CameraView_MediaCaptured(object sender, MediaCapturedEventArgs e)
    {
        var memoryStream = new MemoryStream();
        e.Media.CopyTo(memoryStream);
        memoryStream.Position = 0;
        stream = memoryStream; 

        Dispatcher.Dispatch(() =>
        {
            CapturedImage.Source = ImageSource.FromStream(() => new MemoryStream(memoryStream.ToArray()));
        });
    }

    private async void CaptureImage(object sender, EventArgs e)
    {
        await CameraView.CaptureImage(CancellationToken.None);
        UploadButton.IsEnabled = true;
        UploadButton.BackgroundColor = Color.FromRgba("#198754");
        UploadButton.TextColor = Color.FromRgba("#ffffff");
    }
    private async void UploadImage(object sender, EventArgs e)
    {
        await OnSendFile.InvokeAsync(stream);
        await App.Current.MainPage.Navigation.PopModalAsync();
    }
}