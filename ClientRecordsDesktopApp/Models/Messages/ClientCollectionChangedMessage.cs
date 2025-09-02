using CommunityToolkit.Mvvm.Messaging.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientRecordsDesktopApp.Models.Messages {
    internal class ClientCollectionChangedMessage : ValueChangedMessage<bool> {
        public ClientCollectionChangedMessage(bool valor) : base(valor) {
        }
    }
}
