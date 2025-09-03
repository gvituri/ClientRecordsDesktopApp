using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientRecordsDesktopApp.Services.Interfaces {
    public interface IDialogService {
        Task<bool> ShowConfirmationAsync(Guid windowId, string title, string message, string accept = "Sim", string cancel = "Não");
        Task ShowMessageAsync(Guid windowId, string title, string message, string cancel = "OK");
    }
}
