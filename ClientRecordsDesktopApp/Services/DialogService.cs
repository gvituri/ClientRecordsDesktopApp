using ClientRecordsDesktopApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientRecordsDesktopApp.Services {
    public class DialogService : IDialogService {
        private Page GetCurrentPage(Guid windowId) {
            var window = Application.Current?.Windows.FirstOrDefault(win => win.Id == windowId);
            if (window == null)
                throw new InvalidOperationException("No active window found.");

            return window.Page;
        }

        public async Task<bool> ShowConfirmationAsync(Guid windowId, string title, string message, string accept = "Yes", string cancel = "No") {
            var page = GetCurrentPage(windowId);
            return await page.DisplayAlert(title, message, accept, cancel);
        }

        public async Task ShowMessageAsync(Guid windowId, string title, string message, string cancel = "OK") {
            var page = GetCurrentPage(windowId);
            await page.DisplayAlert(title, message, cancel);
        }
    }
}
