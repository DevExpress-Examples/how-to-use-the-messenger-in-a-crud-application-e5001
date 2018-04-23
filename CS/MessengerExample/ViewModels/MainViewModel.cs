using System;
using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;

namespace MessengerExample.ViewModels {
    [POCOViewModel]
    public class MainViewModel : ViewModelBase {
        protected virtual IDocumentManagerService DocumentManagerService { get { return null; } }
        public void ShowDocument(string documentName) {
            var doc = DocumentManagerService.FindDocumentById(documentName);
            if(doc == null) {
                doc = DocumentManagerService.CreateDocument(documentName, null, this);
                doc.Id = documentName;
            }
            doc.Show();
        }

        public static MainViewModel Create() {
            var factory = ViewModelSource.Factory(() => new MainViewModel());
            return factory();
        }
        protected MainViewModel() {
        }
    }
}