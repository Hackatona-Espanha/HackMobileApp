using Hack4Edu.Models;

namespace Hack4Edu.Views;

public partial class AssigmentView : ContentPage
{
    public AssigmentView(RemainingAssignmentModel model)
    {
        InitializeComponent();
        BindingContext = model;
    }
}