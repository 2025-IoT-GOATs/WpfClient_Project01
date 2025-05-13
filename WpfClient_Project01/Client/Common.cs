using MahApps.Metro.Controls.Dialogs;
using NLog;

namespace Client
{
    public static class Common
    {
        public static readonly Logger LOGGER = LogManager.GetCurrentClassLogger();

        public static IDialogCoordinator DialogCoordinator;
    }
}
