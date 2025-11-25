namespace CetTodoApp.Data;

public static class FakeDb
{
    public static List<TodoItem> Data = new List<TodoItem>();

    public static void AddToDo(TodoItem item)
    {
        Data.Add(item);
    }

    public static void AddToDo(string title, DateTime dueDate)
    {
        var item = new TodoItem
        {
            Title = title,
            DueDate = dueDate
        };

        Data.Add(item);
    }

    public static void ChageCompletionStatus(TodoItem item)
    {
        if (item == null)
            return;

        item.IsComplete = !item.IsComplete;
    }
}
