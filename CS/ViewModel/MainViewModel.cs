using DataAnnotationAttributes.Model;
using DevExpress.Xpf.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace DXSample {
    public class MainViewModel : ViewModelBase {
        protected ContactContext Context;

        public IList<Contact> Entities { get; set; }
        public Contact SelectedEntity { get; set; }
        public ICommand EditCommand { get; private set; }

        protected IDocumentManagerService DocumentService { get { return GetService<IDocumentManagerService>(); } }

        public MainViewModel() {
            Context = new ContactContext();
            Entities = new ObservableCollection<Contact>(Context.Contacts);
            EditCommand = new DelegateCommand(Edit, CanEdit);
            Messenger.Default.Register<EntityChanged<Contact>>(this, OnRefresh);
        }
        public void Edit() {
            var document = DocumentService.Documents.Where(x => Equals(x.Id, SelectedEntity.Id)).FirstOrDefault();
            if (document == null) {
                document = DocumentService.CreateDocument("EditEntityDialog", SelectedEntity.Id, this);
                document.Id = SelectedEntity.Id;
            }
            document.Show();
        }
        public bool CanEdit() {
            return SelectedEntity != null;
        }
        protected void OnRefresh(EntityChanged<Contact> message) {
            var entity = Entities.First(x => x.Id == (int)message.EntityKey);
            Context.Entry(entity).Reload();
            Entities[Entities.IndexOf(entity)] = entity;
        }
    }
    public class EntityChanged<T> {
        public EntityChanged(object entityKey) {
            EntityKey = entityKey;
        }
        public object EntityKey { get; private set; }
    }
}