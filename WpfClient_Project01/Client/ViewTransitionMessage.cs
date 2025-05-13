// ViewTransitionMessage.cs
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;

using System.Windows.Controls;

namespace Client
{
    public class ViewTransitionMessage
    {
        public ObservableObject ViewModel { get; }
        public UserControl View { get; }

        public ViewTransitionMessage(ObservableObject viewModel, UserControl view)
        {
            ViewModel = viewModel;
            View = view;
        }
    }

    public class InitializeMessengerMessage { }
}
