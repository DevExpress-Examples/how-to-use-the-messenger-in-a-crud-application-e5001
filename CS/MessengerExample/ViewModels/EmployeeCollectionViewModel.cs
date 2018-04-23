using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;
using MessengerExample.Common;
using MessengerExample.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Windows;

namespace MessengerExample.ViewModels {
    [POCOViewModel]
    public class EmployeeCollectionViewModel : IDocumentViewModel {
        public object Title { get { return "Employees"; } }
        public virtual IList<Employee> Employees { get; protected set; }
        [ServiceProperty(SearchMode = ServiceSearchMode.PreferParents)]
        protected virtual IMessageBoxService MessageBoxService { get { return null; } }
        [ServiceProperty(SearchMode = ServiceSearchMode.PreferParents)]
        protected virtual IDocumentManagerService DocumentManagerService { get { return null; } }
        EmployeeContext EmployeeContext; 

        public void Add() {
            IDocument document = DocumentManagerService.CreateDocument("EmployeeView", new EmployeeViewModelInfo(EmployeeViewModelType.Add), this);
            document.Show();
        }
        public void Edit(Employee row) {
            int rowId = row.ID;
            IDocument document = FindDocument(rowId);
            if(document == null) {
                document = DocumentManagerService.CreateDocument("EmployeeView", new EmployeeViewModelInfo(rowId), this);
                document.Id = rowId;
            }
            document.Show();
        }
        public bool CanEdit(Employee row) {
            return row != null;
        }
        public void Delete(Employee row) {
            if(MessageBoxService.Show("Delete Employee?", "Confirmation", MessageBoxButton.YesNo) != MessageBoxResult.Yes)
                return;
            CloseDocument(row.ID);
            EmployeeContext.Employees.Remove(row);
            EmployeeContext.SaveChanges();
        }
        public bool CanDelete(Employee row) {
            return row != null;
        }

        public static EmployeeCollectionViewModel Create() {
            var factory = ViewModelSource.Factory(() => new EmployeeCollectionViewModel());
            return factory();
        }
        protected EmployeeCollectionViewModel() {
            Messenger.Default.Register<EntityMessage<Employee>>(this, OnMessage);
            if(!ViewModelBase.IsInDesignMode)
                Initialize();
        }
        void Initialize() {
            EmployeeContext = new EmployeeContext();
            EmployeeContext.Employees.Load();
            Employees = EmployeeContext.Employees.Local;
        }
        void CloseDocument(int id) {
            IDocument doc = FindDocument(id);
            if(doc != null)
                doc.Close();
        }
        IDocument FindDocument(int rowId) {
            foreach(var doc in DocumentManagerService.Documents)
                if(rowId.Equals(doc.Id))
                    return doc;
            return null;
        }
        void OnMessage(EntityMessage<Employee> message) {
            Employee employee = null;
            switch(message.MessageType) {
                case EntityMessageType.Added:
                    EmployeeContext.Employees.Find(message.Entity.ID);
                    break;
                case EntityMessageType.Changed:
                    employee = EmployeeContext.Employees.Find(message.Entity.ID);
                    EmployeeContext.Entry<Employee>(employee).Reload();
                    employee.Refresh();
                    break;
                case EntityMessageType.Deleted:
                    employee = EmployeeContext.Employees.Find(message.Entity.ID);
                    EmployeeContext.Entry<Employee>(employee).Reload();
                    CloseDocument(message.Entity.ID);
                    break;
                default:
                    throw new NotImplementedException();
            }
        }
        bool IDocumentViewModel.Close() { return false; }
    }
}
