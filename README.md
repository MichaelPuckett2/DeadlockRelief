# DeadlockRelief
This is an example of a deadlock and an example, in WPF, of how to possibly hack past it, but not recommended for normal use cases.

Run the WPF app, look at the code behind in the MainWindow (MainWindow.xaml.cs) for the methods and some verbage on what's happening.

    //The trick is to offload the deadlock to another Task, if this can be accomplished.
    var result = SomethingThatDeadlocks().Result;

    //can be done like
    var result = Task.Run(() => SomethingThatDeadlocks().Result).Result;
