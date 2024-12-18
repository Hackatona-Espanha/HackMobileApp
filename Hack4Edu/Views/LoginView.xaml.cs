using Hack4Edu.Common;
using Hack4Edu.Models;
using Plugin.Maui.Audio;
using System.Text.Json;

namespace Hack4Edu.Views;

public partial class LoginView : ContentPage
{
    private readonly IAudioManager _audioManager;
    private string dbPath;
    public LocalizationResourceManager LocalizationResourceManager => LocalizationResourceManager.Instance;

    public LoginView(IAudioManager audioManager)
    {
        InitializeComponent();

        string appDataDirectory = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        dbPath = Path.Combine(appDataDirectory, "Users.json");
        this._audioManager = audioManager;
    }

    private async void CreateAccount_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new CreateAccountView(_audioManager));
    }

    private async void LoginButton_Clicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(EmailEntry.Text) || string.IsNullOrWhiteSpace(PasswordEntry.Text))
        {
            string? errorMessageTitile = LocalizationResourceManager["CreateAccountErrorTitleText"].ToString();
            string? errorMessageText = LocalizationResourceManager["CreateAccountErrorBodyText"].ToString();
            string? errorMessageButton = LocalizationResourceManager["CreateAccountErrorButtonText"].ToString();

            await DisplayAlert(errorMessageTitile, errorMessageText, errorMessageButton);
            return;
        }

        if (File.Exists(dbPath))
        {
            var existingJson = File.ReadAllText(dbPath);
            var users = JsonSerializer.Deserialize<List<UserModel>>(existingJson) ?? [];

            var loginUser = users.FirstOrDefault(user => user.Email == EmailEntry.Text && user.Password == PasswordEntry.Text);

            if (loginUser != null)
            {
                await Navigation.PushAsync(new MainPage(_audioManager));
            }
            else
            {
                // Usu�rio n�o encontrado, mostrar um alerta  
                string? errorMessageTitile = LocalizationResourceManager["LoginErrorTitleText"].ToString();
                string? errorMessageText = LocalizationResourceManager["LoginErrorBodyText"].ToString();
                string? errorMessageButton = LocalizationResourceManager["LoginErrorButtonText"].ToString();
                await DisplayAlert(errorMessageTitile, errorMessageText, errorMessageButton);
            }
        }
    }
}