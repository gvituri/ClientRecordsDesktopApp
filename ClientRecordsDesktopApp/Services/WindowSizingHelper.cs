using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#if WINDOWS
using Microsoft.UI.Windowing;
using Microsoft.UI;
#endif

namespace ClientRecordsDesktopApp.Services {
    public static class WindowSizingHelper {
        public enum WindowSize {
            FullScreen,
            ThreeQuartersScreen,
            HalfScreen
        }

        public enum WindowPosition {
            CenterScreen,
            TopLeft,
            TopRight,
            BottomLeft,
            BottomRight
        }

        public static void SetWindowSizeAndPosition(Window window, WindowSize size, WindowPosition position) {
            var displayInfo = DeviceDisplay.MainDisplayInfo;
            double screenWidth = displayInfo.Width / displayInfo.Density;
            double screenHeight = displayInfo.Height / displayInfo.Density;
            switch (size) {
                case WindowSize.FullScreen:
                    window.Width = screenWidth;
                    window.Height = screenHeight;
                    break;
                case WindowSize.ThreeQuartersScreen:
                    window.Width = screenWidth * 0.75;
                    window.Height = screenHeight * 0.75;
                    break;
                case WindowSize.HalfScreen:
                    window.Width = screenWidth / 2;
                    window.Height = screenHeight / 2;
                    break;
            }
            switch (position) {
                case WindowPosition.CenterScreen:
                    window.X = (screenWidth - window.Width) / 2;
                    window.Y = (screenHeight - window.Height) / 2;
                    break;
                case WindowPosition.TopLeft:
                    window.X = 0;
                    window.Y = 0;
                    break;
                case WindowPosition.TopRight:
                    window.X = screenWidth - window.Width;
                    window.Y = 0;
                    break;
                case WindowPosition.BottomLeft:
                    window.X = 0;
                    window.Y = screenHeight - window.Height;
                    break;
                case WindowPosition.BottomRight:
                    window.X = screenWidth - window.Width;
                    window.Y = screenHeight - window.Height;
                    break;
            }
        }

        public static void MaximizeWindow(Window window) {
#if WINDOWS
            var nativeWindow = window.Handler.PlatformView;
            IntPtr windowHandle = WinRT.Interop.WindowNative.GetWindowHandle(nativeWindow);
            WindowId WindowId = Win32Interop.GetWindowIdFromWindow(windowHandle);
            AppWindow appWindow = AppWindow.GetFromWindowId(WindowId);

            var p = appWindow.Presenter as OverlappedPresenter;

            p!.Maximize();
#endif
        }
    }
}
