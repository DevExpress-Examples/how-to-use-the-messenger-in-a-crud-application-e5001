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


