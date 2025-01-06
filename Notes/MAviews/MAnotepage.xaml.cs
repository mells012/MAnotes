namespace Notes.Views;

[QueryProperty(nameof(MAitemId), nameof(MAitemId))]
public partial class MAnotePage : ContentPage
{
    public string MAitemId
    {
        set { MAloadNote(value); }
    }

    public MAnotePage()
    {
        InitializeComponent();

        string appDataPath = FileSystem.AppDataDirectory;
        string randomFileName = $"{Path.GetRandomFileName()}.notes.txt";

        MAloadNote(Path.Combine(appDataPath, randomFileName));
    }

    private async void MAsaveButton_Clicked(object sender, EventArgs e)
    {
        if (BindingContext is Models.MAnote note)
            File.WriteAllText(note.Filename, TextEditor.Text);

        await Shell.Current.GoToAsync("..");
    }

    private async void MAdeleteButton_Clicked(object sender, EventArgs e)
    {
        if (BindingContext is Models.MAnote note)
        {
            // Delete the file.
            if (File.Exists(note.Filename))
                File.Delete(note.Filename);
        }

        await Shell.Current.GoToAsync("..");
    }

    private void MAloadNote(string fileName)
    {
        Models.MAnote noteModel = new Models.MAnote();
        noteModel.Filename = fileName;

        if (File.Exists(fileName))
        {
            noteModel.Date = File.GetCreationTime(fileName);
            noteModel.Text = File.ReadAllText(fileName);
        }

        BindingContext = noteModel;
    }

}