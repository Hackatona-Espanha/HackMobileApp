using Hack4Edu.Models;
using Plugin.Maui.Audio;

namespace Hack4Edu.Views;

public partial class AssigmentView : ContentPage
{
    private readonly IAudioManager _audioManager;
    private bool isPlaying = false;
    private IAudioPlayer _player;
    public AssigmentView(RemainingAssignmentModel model, IAudioManager audioManager)
    {
        InitializeComponent();
        BindingContext = model;
        this._audioManager = audioManager;

    }

    private void AudioButton_Clicked(object sender, EventArgs e)
    {
        var model = (AIGeneratedworkModel)AssignmentCarousel.CurrentItem;
        var audio = model.AudioStream;

        if (audio == null)
        {
            return;
        }

        if (isPlaying)
        {
            _player.Stop();
            _player = null;
            isPlaying = false;
            return;
        }

        _player = _audioManager.CreatePlayer(audio);
        _player.Play();
        isPlaying = true;
    }
}