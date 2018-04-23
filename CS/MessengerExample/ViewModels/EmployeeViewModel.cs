using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;
using MessengerExample.Common;
using MessengerExample.Model;
using System;
using System.Windows;

namespace MessengerExample.ViewModels {
    public enum EmployeeViewModelType { Add, Modify }
    public class EmployeeViewModelInfo {
        public EmployeeViewModelType Type { get; set; }
        public int RecordID { get; set; }
        public EmployeeViewModelInfo(EmployeeViewModelType type)
            : this(type, -1) {
        }
        public EmployeeViewModelInfo(int recordId)
            : this(EmployeeViewModelType.Modify, recordId) {
        }
        EmployeeViewModelInfo(EmployeeViewModelType type, int recordID) {
            Type = type;
            RecordID = recordID;
        }
    }
    [POCOViewModel]
    public class EmployeeViewModel : ISupportParameter, IDocumentViewModel {
        public virtual object Title { get; protected set; }
        public virtual Employee Employee { get; protected set; }
        public virtual object Parameter { get; set; }
        [ServiceProperty(SearchMode = ServiceSearchMode.PreferParents)]
        protected virtual IMessageBoxService MessageBoxService { get { return null; } }
        [ServiceProperty(SearchMode = ServiceSearchMode.PreferParents)]
        protected virtual IDocumentManagerService DocumentManagerService { get { return null; } }
        EmployeeContext EmployeeContext;

        public void Save() {
            bool isNew = EmployeeContext.IsNew(Employee);
            EmployeeContext.SaveChanges();
            if(isNew)
                Messenger.Default.Send(new EntityMessage<Employee>(Employee, EntityMessageType.Added));
            else
                Messenger.Default.Send(new EntityMessage<Employee>(Employee, EntityMessageType.Changed));
            UpdateTitle();
        }
        public bool CanSave() {
            if(Employee == null) return false;
            return EmployeeContext.IsNew(Employee) || EmployeeContext.NeedSave(Employee);
        }
        public void Close() {
            if(TryClose() && DocumentManagerService != null) {
                IDocument document = DocumentManagerService.FindDocument(this);
                if(document != null)
                    document.Close();
            };
        }
        public void Delete() {
            if(MessageBoxService.Show("Delete Employee?", "Confirmation", MessageBoxButton.YesNo) != MessageBoxResult.Yes)
                return;
            EmployeeContext.Employees.Remove(Employee);
            EmployeeContext.SaveChanges();
            Messenger.Default.Send(new EntityMessage<Employee>(Employee, EntityMessageType.Deleted));
            Employee = null;
            UpdateTitle();
        }
        public bool CanDelete() {
            if(Employee == null) return false;
            return !EmployeeContext.IsNew(Employee);
        }

        public static EmployeeViewModel Create() {
            var factory = ViewModelSource.Factory(() => new EmployeeViewModel());
            return factory();
        }
        protected EmployeeViewModel() {
            if(!ViewModelBase.IsInDesignMode)
                Initialize();
        }
        protected void OnParameterChanged() {
            if(ViewModelBase.IsInDesignMode) return;
            EmployeeViewModelInfo info = (EmployeeViewModelInfo)Parameter;
            if(info == null) throw new InvalidOperationException();
            if(info.Type == EmployeeViewModelType.Add) {
                Employee = EmployeeContext.Employees.Create();
                EmployeeContext.Employees.Add(Employee);
            } else {
                Employee = EmployeeContext.Employees.Find(info.RecordID);
            }
            UpdateTitle();
        }
        void Initialize() {
            EmployeeContext = new EmployeeContext();
        }
        void UpdateTitle() {
            if(Employee != null) {
                string entityId = EmployeeContext.IsNew(Employee) ? "(New)" : string.Format("{0} {1}", Employee.FirstName, Employee.LastName);
                Title = entityId;
            } else {
                Title = null;
            }
        }
        bool TryClose() {
            if(!EmployeeContext.NeedSave(Employee))
                return true;
            MessageBoxResult result = MessageBoxService.Show("Do you want to save changes?", "Confirmation", MessageBoxButton.YesNoCancel);
            if(result == MessageBoxResult.Yes) {
                Save();
                return true;
            }
            if(result == MessageBoxResult.No)
                return true;
            return false;
        }
        bool IDocumentViewModel.Close() {
            return TryClose();
        }
    }
}
