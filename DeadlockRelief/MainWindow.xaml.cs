using System.Windows;

namespace DeadlockRelief;

public partial class MainWindow : Window
{
    private const int DisabledTimeMS = 1000;

    public MainWindow() => InitializeComponent();

    private void button1_Click(object sender, RoutedEventArgs e)
    {
        //This results in a deadlock since the current context, UI, is waiting on the result.
        //The method is called, awaiting a delay, returning the context to here waiting for the result.
        //Since we are on the same context the statement that gets returned is never passed back because the UI is paused here waiting on it.
        var result = DeadlockRelief().Result;
        button1.Content = "Complete";
    }

    private void button2_Click(object sender, RoutedEventArgs e)
    {
        //This is a hack to off load the context to a new context to capture the result.
        //NOTE: The current context is still paused until the result is complete making this non-asynchronous.
        //The UI is now waiting on the result of another context.
        //The thing to note is the inner result is now taking place on a different context.
        //If the inner result is on a different context and is expected to make changes to a thread safe type, such as UI components, then this will not work.
        var result = Task.Run(() => DeadlockRelief().Result).Result;
        button2.Content = "Complete";
    }

    private async Task<bool> DeadlockRelief()
    {
        await Task.Delay(DisabledTimeMS);
        return true;
    }
}