using System.Collections.ObjectModel;

namespace Notes.Models;

internal class MAallnotes
{
    public ObservableCollection<MAnote> Notes { get; set; } = new ObservableCollection<MAnote>();

    public MAallnotes() =>
        MAloadNotes();

    public void MAloadNotes()
    {
        Notes.Clear();

        // Get the folder where the notes are stored.
        string appDataPath = FileSystem.AppDataDirectory;

        // Use Linq extensions to load the *.notes.txt files.
        IEnumerable<MAnote> notes = Directory

                                    // Select the file names from the directory
                                    .EnumerateFiles(appDataPath, "*.notes.txt")

                                    // Each file name is used to create a new Note
                                    .Select(filename => new MAnote()
                                    {
                                        Filename = filename,
                                        Text = File.ReadAllText(filename),
                                        Date = File.GetLastWriteTime(filename)
                                    })

                                    // With the final collection of notes, order them by date
                                    .OrderBy(note => note.Date);

        // Add each note into the ObservableCollection
        foreach (MAnote note in notes)
            Notes.Add(note);
    }
}