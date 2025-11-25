using CetTodoApp.Data;

namespace CetTodoApp;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();


        FakeDb.AddToDo("Test1", DateTime.Now.AddDays(-1)); 
        FakeDb.AddToDo("Test2", DateTime.Now.AddDays(1));
        FakeDb.AddToDo("Test3", DateTime.Now);


        DueDate.Date = DateTime.Today;

        RefreshListView();
    }


    private async void AddButton_OnClicked(object? sender, EventArgs e)
    {
        var titleText = Title.Text?.Trim();


        if (string.IsNullOrWhiteSpace(titleText))
        {
            await DisplayAlert("Validation", "Please enter a title for the todo item.", "OK");
            return;
        }


        var selectedDate = DueDate.Date;
        if (selectedDate < DateTime.Today)
        {
            await DisplayAlert("Validation", "Due date cannot be in the past.", "OK");
            return;
        }

        FakeDb.AddToDo(titleText, selectedDate);


        Title.Text = string.Empty;
        DueDate.Date = DateTime.Today;

        RefreshListView();
    }

    private void RefreshListView()
    {
        TasksListView.ItemsSource = null;


        TasksListView.ItemsSource = FakeDb.Data
            .Where(x => !x.IsComplete ||
                        (x.IsComplete && x.DueDate > DateTime.Now.AddDays(-1)))
            .ToList();
    }

    private void TasksListView_OnItemSelected(object? sender, SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem is not TodoItem item)
            return;

        FakeDb.ChageCompletionStatus(item);
        RefreshListView();


        TasksListView.SelectedItem = null;
    }
}
