using DevExpress.Mvvm;

namespace MessengerExample.Common {
    public enum EntityMessageType {
        Added, Deleted, Changed
    }
    public class EntityMessage<TEntity> {
        public TEntity Entity { get; private set; }
        public EntityMessageType MessageType { get; private set; }
        public EntityMessage(TEntity entity, EntityMessageType messageType) {
            this.Entity = entity;
            this.MessageType = messageType;
        }
    }
}
