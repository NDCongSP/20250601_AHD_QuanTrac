using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

public class AlarmModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    private int id;
    private string tagName; // ✅ thêm biến backing field
    private string status;
    private string position;
    private DateTime createAt;

    public int Id
    {
        get => id;
        set { id = value; OnPropertyChanged(); }
    }

    public string TagName
    {
        get => tagName;
        set { tagName = value; OnPropertyChanged(); }
    }

    public string Status
    {
        get => status;
        set { status = value; OnPropertyChanged(); }
    }

    public string Position
    {
        get => position;
        set { position = value; OnPropertyChanged(); }
    }

    public DateTime CreateAt
    {
        get => createAt;
        set { createAt = value; OnPropertyChanged(); }
    }

    protected void OnPropertyChanged([CallerMemberName] string name = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}