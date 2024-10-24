using Hack4Edu.Common;
using Hack4Edu.Models;
using Hack4Edu.Views;
using Microsoft.Maui.Handlers;
using System.Collections.ObjectModel;

namespace Hack4Edu
{
    public partial class MainPage : ContentPage
    {
        public LocalizationResourceManager LocalizationResourceManager => LocalizationResourceManager.Instance;
        public ObservableCollection<SubjectsModel> Subjects { get; set; }
        public ObservableCollection<RemainingAssignmentModel> RemainingAssignments { get; set; }

        public MainPage()
        {
            InitializeComponent();
            ModifySearchBar();
            InitializeSubjects();
            BindingContext = this;
        }

        private void InitializeSubjects()
        {
            Subjects = new ObservableCollection<SubjectsModel>
            {
                new SubjectsModel { Name = LocalizationResourceManager["MathematicsText"].ToString(), Image = "matematica.png"},

                new SubjectsModel { Name = LocalizationResourceManager["HistoryText"].ToString(), Image = "history.png",
                RemainingAssignments = new List<RemainingAssignmentModel>
                {
                    new RemainingAssignmentModel {Id = 1, IsRead = false, AssignmentName = LocalizationResourceManager["HistAssignment1"].ToString(), AssignmentImage = "historyassign1.png"}
                }},

                new SubjectsModel { Name = LocalizationResourceManager["GeographyText"].ToString(), Image = "geography.png",
                RemainingAssignments = new List<RemainingAssignmentModel>
                {
                    new RemainingAssignmentModel {Id = 2, IsRead = false, AssignmentName = LocalizationResourceManager["GeoAssignment1"].ToString(), AssignmentImage = "geoassign1.png"}
                }},
                new SubjectsModel { Name = LocalizationResourceManager["ArtText"].ToString(), Image = "art.png" },
                new SubjectsModel { Name = LocalizationResourceManager["MusicText"].ToString(), Image = "music.png" }
            };

            RemainingAssignments = new ObservableCollection<RemainingAssignmentModel>(Subjects.Where(s => s.RemainingAssignments != null)
              .SelectMany(s => s.RemainingAssignments)
             .Where(a => !a.IsRead));
        }

        private async void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
        {
            var grid = sender as Grid;
            var item = grid?.BindingContext as SubjectsModel;

            if (item != null)
            {
                // Navegar para a nova página  
                await Navigation.PushAsync(new SubjectView(item));
            }
        }

        private async void RemainingAssigmentsGrid_Tapped(object sender, TappedEventArgs e)
        {
            var grid = sender as Grid;
            var item = grid?.BindingContext as RemainingAssignmentModel;

            if (item != null)
            {
                // Navegar para a nova página  
                await Navigation.PushAsync(new AssigmentView(item));
            }
        }

        private void ModifySearchBar()
        {
            SearchBarHandler.Mapper.AppendToMapping("CustomSearchIconColor", (handler, view) =>
            {


#if ANDROID
                var context = handler.PlatformView.Context;
                var searchIconId = context?.Resources?.GetIdentifier("search_mag_icon", "id", context.PackageName);
                if(searchIconId != 0)
                {
                    var searchIcon = handler.PlatformView.FindViewById<Android.Widget.ImageView>(searchIconId ?? 0);
                    searchIcon?.SetColorFilter(Android.Graphics.Color.Rgb(172,157,185), Android.Graphics.PorterDuff.Mode.SrcIn);
                }
#endif
            });
        }

    }
}
