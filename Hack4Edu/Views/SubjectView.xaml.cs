using Hack4Edu.Models;
using Plugin.Maui.Audio;

namespace Hack4Edu.Views;

public partial class SubjectView : ContentPage
{
    private readonly IAudioManager _audioManager;

    public SubjectView(SubjectsModel model, IAudioManager audioManager)
    {
        InitializeComponent();
        BindingContext = model;
        this._audioManager = audioManager;
    }

    private async void RemainingAssignment_Tapped(object sender, TappedEventArgs e)
    {
        var grid = sender as Grid;
        var item = grid?.BindingContext as RemainingAssignmentModel;

        if (item != null)
        {
            // Navegar para a nova página  
            await Navigation.PushAsync(new AssigmentView(item, _audioManager));
        }
    }
}