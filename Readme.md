<!-- default file list -->
*Files to look at*:

* [EntityMessage.cs](./CS/MessengerExample/Common/EntityMessage.cs) (VB: [EntityMessage.vb](./VB/MessengerExample/Common/EntityMessage.vb))
* [MainWindow.xaml](./CS/MessengerExample/MainWindow.xaml) (VB: [MainWindow.xaml.vb](./VB/MessengerExample/MainWindow.xaml.vb))
* [MainWindow.xaml.cs](./CS/MessengerExample/MainWindow.xaml.cs) (VB: [MainWindow.xaml.vb](./VB/MessengerExample/MainWindow.xaml.vb))
* [Employee.cs](./CS/MessengerExample/Model/Employee.cs) (VB: [EmployeeContext.vb](./VB/MessengerExample/Model/EmployeeContext.vb))
* [EmployeeContext.cs](./CS/MessengerExample/Model/EmployeeContext.cs) (VB: [EmployeeContext.vb](./VB/MessengerExample/Model/EmployeeContext.vb))
* [EmployeeContextInitializer.cs](./CS/MessengerExample/Model/EmployeeContextInitializer.cs) (VB: [EmployeeContextInitializer.vb](./VB/MessengerExample/Model/EmployeeContextInitializer.vb))
* [EmployeeCollectionViewModel.cs](./CS/MessengerExample/ViewModels/EmployeeCollectionViewModel.cs) (VB: [EmployeeCollectionViewModel.vb](./VB/MessengerExample/ViewModels/EmployeeCollectionViewModel.vb))
* [EmployeeViewModel.cs](./CS/MessengerExample/ViewModels/EmployeeViewModel.cs) (VB: [EmployeeViewModel.vb](./VB/MessengerExample/ViewModels/EmployeeViewModel.vb))
* [MainViewModel.cs](./CS/MessengerExample/ViewModels/MainViewModel.cs) (VB: [MainViewModel.vb](./VB/MessengerExample/ViewModels/MainViewModel.vb))
* [EmployeeCollectionView.xaml](./CS/MessengerExample/Views/EmployeeCollectionView.xaml) (VB: [EmployeeCollectionView.xaml.vb](./VB/MessengerExample/Views/EmployeeCollectionView.xaml.vb))
* [EmployeeCollectionView.xaml.cs](./CS/MessengerExample/Views/EmployeeCollectionView.xaml.cs) (VB: [EmployeeCollectionView.xaml.vb](./VB/MessengerExample/Views/EmployeeCollectionView.xaml.vb))
* [EmployeeView.xaml](./CS/MessengerExample/Views/EmployeeView.xaml) (VB: [EmployeeView.xaml.vb](./VB/MessengerExample/Views/EmployeeView.xaml.vb))
* [EmployeeView.xaml.cs](./CS/MessengerExample/Views/EmployeeView.xaml.cs) (VB: [EmployeeView.xaml.vb](./VB/MessengerExample/Views/EmployeeView.xaml.vb))
* [MainView.xaml](./CS/MessengerExample/Views/MainView.xaml) (VB: [MainView.xaml.vb](./VB/MessengerExample/Views/MainView.xaml.vb))
* [MainView.xaml.cs](./CS/MessengerExample/Views/MainView.xaml.cs) (VB: [MainView.xaml.vb](./VB/MessengerExample/Views/MainView.xaml.vb))
<!-- default file list end -->
# How to use the Messenger in a CRUD application


<p>In this example, we demonstrated how to notify a view model about changes when editing is performed by an independent View Model. This is implemented using the <a href="https://documentation.devexpress.com/WPF/CustomDocument17474.aspx">Messenger</a> class:<br><br>- MainViewModel is subscribed to messages of the EntityChanged<Contact> type:</p>

```cs
Messenger.Default.Register<EntityChanged<Contact>>(this, OnRefresh);
```

<p><br>- DialogViewModel sends a message width the Id of the modified entity.</p>

```cs
Messenger.Default.Send(new EntityChanged<Contact>(Entity.Id));
```



<br/>


