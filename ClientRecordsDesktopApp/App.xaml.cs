using Microsoft.Maui.Devices;

namespace ClientRecordsDesktopApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell()) {
                Title = "Client Records Desktop App",
                Width = DeviceDisplay.MainDisplayInfo.Width / DeviceDisplay.MainDisplayInfo.Density,
                Height = DeviceDisplay.MainDisplayInfo.Height / DeviceDisplay.MainDisplayInfo.Density,
                X = 0,
                Y = 0
            };
        }
    }
}