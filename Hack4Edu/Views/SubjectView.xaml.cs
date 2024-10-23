using Hack4Edu.Models;

namespace Hack4Edu.Views;

public partial class SubjectView : ContentPage
{
    public SubjectView(SubjectsModel model)
    {
        InitializeComponent();
        BindingContext = model;
    }

    private async void RemainingAssignment_Tapped(object sender, TappedEventArgs e)
    {
        var grid = sender as Grid;
        var item = grid?.BindingContext as RemainingAssignmentModel;

        if (item != null)
        {
            // Navegar para a nova página  
            await Navigation.PushAsync(new AssigmentView(item));
        }
    }
}