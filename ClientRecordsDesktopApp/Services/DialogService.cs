using ClientRecordsDesktopApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientRecordsDesktopApp.Services {
    public class DialogService : IDialogService {
        private Page GetCurrentPage() {
            var window = Application.Current?.Windows.FirstOrDefault();
            if (window == null)
                throw new InvalidOperationException("No active window found.");

            return window.Page;
        }

        public async Task<bool> ShowConfirmationAsync(string title, string message, string accept = "Yes", string cancel = "No") {
            var page = GetCurrentPage();
            return await page.DisplayAlert(title, message, accept, cancel);
        }

        public async Task ShowMessageAsync(string title, string message, string cancel = "OK") {
            var page = GetCurrentPage();
            await page.DisplayAlert(title, message, cancel);
        }
    }
}
