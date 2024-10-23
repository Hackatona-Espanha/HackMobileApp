using Hack4Edu.Models;
using System.Globalization;
using System.Reflection;
using System.Text.Json;

namespace Hack4Edu.Views;

public partial class AssigmentView : ContentPage
{
    public AssigmentView(RemainingAssignmentModel model)
    {
        InitializeComponent();

        PopulateAIGeneratedWorks(model);
        BindingContext = model;
    }

    private void PopulateAIGeneratedWorks(RemainingAssignmentModel model)
    {
        var cultureInfo = CultureInfo.CurrentCulture;
        var fileName = $"Hack4Edu.Database.Assignments.{model.Id}_{cultureInfo}.json";

        var assembly = Assembly.GetExecutingAssembly();
        if (!assembly.GetManifestResourceNames().Contains(fileName))
        {
            return;
        }

        using var stream = assembly.GetManifestResourceStream(fileName);
        using var reader = new StreamReader(stream);
        var json = reader.ReadToEnd();

        var aiGeneratedWorks = JsonSerializer.Deserialize<List<AIGeneratedworkModel>>(json);

        if (model.AIGeneratedWorks == null)
        {
            model.AIGeneratedWorks = new List<AIGeneratedworkModel>();
        }

        foreach (var work in aiGeneratedWorks)
        {
            if (work != null)
            {
                if (work.Text != null || work.ImageBase64 != null)
                {
                    model.AIGeneratedWorks.Add(work);
                }
            }
        }

        BindingContext = model;
        AssignmentCollection.BindingContext = model.AIGeneratedWorks;
    }
}