using Hack4Edu.Common;
using Hack4Edu.Models;
using Hack4Edu.Views;
using Plugin.Maui.Audio;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Reflection;
using System.Text.Json;

namespace Hack4Edu
{
    public partial class MainPage : ContentPage
    {
        private readonly IAudioManager _audioManager;
        public LocalizationResourceManager LocalizationResourceManager => LocalizationResourceManager.Instance;
        public ObservableCollection<SubjectsModel> Subjects { get; set; }
        public ObservableCollection<RemainingAssignmentModel> RemainingAssignments { get; set; }

        public MainPage(IAudioManager audioManager)
        {
            InitializeComponent();
            InitializeSubjects();
            BindingContext = this;
            this._audioManager = audioManager;
        }

        private async void InitializeSubjects()
        {
            Subjects = new ObservableCollection<SubjectsModel>
            {
                new SubjectsModel { Name = LocalizationResourceManager["MathematicsText"].ToString(), Image = "matematica.png"},

                new SubjectsModel { Name = LocalizationResourceManager["HistoryText"].ToString(), Image = "history.png",
                RemainingAssignments = new List<RemainingAssignmentModel>
                {
                    new RemainingAssignmentModel {Id = 1, IsRead = false, AssignmentName = LocalizationResourceManager["HistAssignment1"].ToString(), AssignmentImage = "historyassign1.png",
                    AIGeneratedWorks = await PopulateAssignments(1)}
                }},

                new SubjectsModel { Name = LocalizationResourceManager["GeographyText"].ToString(), Image = "geography.png",
                RemainingAssignments = new List<RemainingAssignmentModel>
                {
                    new RemainingAssignmentModel {Id = 2, IsRead = false, AssignmentName = LocalizationResourceManager["GeoAssignment1"].ToString(), AssignmentImage = "geoassign1.png",
                    AIGeneratedWorks = await PopulateAssignments(2)}
                }},
                new SubjectsModel { Name = LocalizationResourceManager["ArtText"].ToString(), Image = "art.png" },
                new SubjectsModel { Name = LocalizationResourceManager["MusicText"].ToString(), Image = "music.png" }
            };

            RemainingAssignments = new ObservableCollection<RemainingAssignmentModel>(Subjects.Where(s => s.RemainingAssignments != null)
              .SelectMany(s => s.RemainingAssignments)
             .Where(a => !a.IsRead));
        }

        private async Task<List<AIGeneratedworkModel>> PopulateAssignments(int id)
        {
            try
            {
                var cultureInfo = CultureInfo.CurrentCulture;
                var fileName = $"Hack4Edu.Database.Assignments.{id}_{cultureInfo}.json";

                var assembly = Assembly.GetExecutingAssembly();
                if (!assembly.GetManifestResourceNames().Contains(fileName))
                {
                    return [];
                }

                using var stream = assembly.GetManifestResourceStream(fileName);
                using var reader = new StreamReader(stream);
                var json = reader.ReadToEnd();

                var aiGeneratedWorks = JsonSerializer.Deserialize<List<AIGeneratedworkModel>>(json);

                aiGeneratedWorks = aiGeneratedWorks
                                        .Where(work => work != null && work.Text != null && work.ImageBase64 != null)
                                       .Select(work =>
                                       {
                                           work.ImageStream = new MemoryStream(Convert.FromBase64String(work.ImageBase64));
                                           if (!string.IsNullOrEmpty(work.AudioBase64))
                                           {
                                               work.ConvertAudioStream();
                                           }
                                           return work;
                                       }).ToList();

                return aiGeneratedWorks;
            }
            catch
            {
                return [];
            }

        }

        private async void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
        {
            var grid = sender as Grid;
            var item = grid?.BindingContext as SubjectsModel;

            if (item != null)
            {
                // Navegar para a nova página  
                await Navigation.PushAsync(new SubjectView(item, _audioManager));
            }
        }

        private async void RemainingAssigmentsGrid_Tapped(object sender, TappedEventArgs e)
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
}
