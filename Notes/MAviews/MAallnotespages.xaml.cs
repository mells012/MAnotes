namespace Notes.Views;

public partial class AllNotesPage : ContentPage
{
    public AllNotesPage()
    {
        InitializeComponent();

        BindingContext = new Models.MAallnotes();
    }

    protected override void OnAppearing()
    {
        ((Models.MAallnotes)BindingContext).MAloadNotes();
    }

    private async void MAadd_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(MAnotePage));
    }

    private async void MAnotesCollection_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.Count != 0)
        {
            // Get the note model
            var note = (Models.MAnote)e.CurrentSelection[0];

            // Should navigate to "MAnotePage?MAitemId=path\on\device\XYZ.notes.txt"
            await Shell.Current.GoToAsync($"{nameof(MAnotePage)}?{nameof(MAnotePage.MAitemId)}={note.Filename}");

            // Unselect the UI
            notesCollection.SelectedItem = null;
        }
    }
}