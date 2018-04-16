using DataAnnotationAttributes.Model;
using DevExpress.Xpf.Mvvm;
using System;
using System.Linq;
using System.Windows.Input;

namespace DXSample {
    public class DialogViewModel : ViewModelBase {
        protected ContactContext Context;

        Contact entity;
        public Contact Entity {
            get {
                return entity;
            }
            set {
                entity = value;
                RaisePropertyChanged(() => Entity);
            }
        }
        public ICommand SaveCommand { get; private set; }

        public DialogViewModel() {
            SaveCommand = new DelegateCommand(Save);
        }

        protected override void OnParameterChanged(object parameter) {
            base.OnParameterChanged(parameter);
            if (!(parameter is int))
                return;
            Context = new ContactContext();
            Entity = Context.Contacts.Where(x => x.Id == (int)parameter).FirstOrDefault();
        }
        public void Save() {
            Context.SaveChanges();
            Messenger.Default.Send(new EntityChanged<Contact>(Entity.Id));
        }
    }
}