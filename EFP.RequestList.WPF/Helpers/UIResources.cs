using System.Windows;

namespace EFP.RequestList.WPF.Helpers
{
    public static partial class UIResources
    {
        public static void InvokeByMain(Action action) => Application.Current.Dispatcher.Invoke(action);
    }
}
