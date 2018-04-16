using DataAnnotationAttributes.Model;
using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using System;
using System.Linq;

namespace DXSample {
    public class DialogViewModel : ISupportParameter {
        protected ContactContext Context;

        public virtual Contact Entity { get; set; }
        public virtual object Parameter { get; set; }

        protected ICurrentWindowService CurrentWindowService { get { return this.GetService<ICurrentWindowService>(); } }

        protected virtual void OnParameterChanged() {
            if (!(Parameter is int))
                return;
            Context = new ContactContext();
            Entity = Context.Contacts.Where(x => x.Id == (int)Parameter).FirstOrDefault();
        }
        public void Save() {
            Context.SaveChanges();
            Messenger.Default.Send(new EntityChanged<Contact>(Entity.Id));
        }
        public void SaveAndClose() {
            Save();
            CurrentWindowService.Close();
        }
    }
}