using DataAnnotationAttributes.Model;
using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace DXSample {
    public class MainViewModel {
        protected ContactContext Context;

        public virtual IList<Contact> Entities { get; set; }
        public virtual Contact SelectedEntity { get; set; }

        protected IDocumentManagerService DocumentService { get { return this.GetService<IDocumentManagerService>(); } }

        public MainViewModel() {
            Context = new ContactContext();
            Entities = new ObservableCollection<Contact>(Context.Contacts);
            Messenger.Default.Register<EntityChanged<Contact>>(this, OnRefresh);
        }
        public void Edit() {
            var document = DocumentService.Documents.Where(x => Equals(x.Id, SelectedEntity.Id)).FirstOrDefault();
            if (document == null) {
                document = DocumentService.CreateDocument(null, SelectedEntity.Id, null);
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