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
    }

    private async void Recapture(object sender, EventArgs e)
    {
        //await OnSendFile.InvokeAsync(null);
        //await App.Current.MainPage.Navigation.PopModalAsync();
        CaptureButton.IsVisible = true;
        AfterCaptureButtonGroup.IsVisible = false;
        CameraView.IsVisible = true;
        CapturedImage.IsVisible = false;
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
        CaptureButton.IsVisible = false;
        AfterCaptureButtonGroup.IsVisible = true;
        CameraView.IsVisible = false;
        CapturedImage.IsVisible = true;
    }
    private async void UploadImage(object sender, EventArgs e)
    {
        await OnSendFile.InvokeAsync(stream);
        //await App.Current.MainPage.Navigation.PopModalAsync();
        await Navigation.PopAsync();
    }
}