using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientRecordsDesktopApp.Services {
    public static class WindowManagementHelper {
        public static void CloseWindowById(Guid windowId) {
            var window = Application.Current?.Windows.FirstOrDefault(win => win.Id == windowId);
            if (window == null)
                throw new InvalidOperationException("No active window found.");

            App.Current?.CloseWindow(window);
        }
    }
}
