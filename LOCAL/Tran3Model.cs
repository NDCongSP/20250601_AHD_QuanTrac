using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

public class Tran3Model : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    private int id;
    private string device;
    private string status;
    private DateTime createAt;

    public int Id
    {
        get => id;
        set { id = value; OnPropertyChanged(); }
    }

    public string Device
    {
        get => device;
        set { device = value; OnPropertyChanged(); }
    }

    public string Status
    {
        get => status;
        set { status = value; OnPropertyChanged(); }
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