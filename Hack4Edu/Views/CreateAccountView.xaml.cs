using Hack4Edu.Common;
using Hack4Edu.Models;
using System.Text.Json;

namespace Hack4Edu.Views;

public partial class CreateAccountView : ContentPage
{

    private string dbPath;
    public LocalizationResourceManager LocalizationResourceManager => LocalizationResourceManager.Instance;

    public CreateAccountView()
    {
        InitializeComponent();

        string appDataDirectory = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        dbPath = Path.Combine(appDataDirectory, "Users.json");


        string directoryPath = Path.GetDirectoryName(dbPath);
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }
    }

    private async void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        await Navigation.PushAsync(new LoginView());
    }

    private async void CreateAcountButton_Clicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(NameEntry.Text) || string.IsNullOrWhiteSpace(EmailEntry.Text) || string.IsNullOrWhiteSpace(PasswordEntry.Text))
        {
            string? errorMessageTitile = LocalizationResourceManager["CreateAccountErrorTitleText"].ToString();
            string? errorMessageText = LocalizationResourceManager["CreateAccountErrorBodyText"].ToString();
            string? errorMessageButton = LocalizationResourceManager["CreateAccountErrorButtonText"].ToString();

            await DisplayAlert(errorMessageTitile, errorMessageText, errorMessageButton);
            return;
        }


        var newUser = new UserModel
        {
            Name = NameEntry.Text,
            Email = EmailEntry.Text,
            Password = PasswordEntry.Text
        };

        List<UserModel> users;
        if (File.Exists(dbPath))
        {
            var existingJson = File.ReadAllText(dbPath);
            users = JsonSerializer.Deserialize<List<UserModel>>(existingJson) ?? [];
        }
        else
        {
            users = new List<UserModel>();
        }

        users.Add(newUser);

        var updatedJson = JsonSerializer.Serialize(users);
        File.WriteAllText(dbPath, updatedJson);

        await Navigation.PushAsync(new LoginView());
    }
}