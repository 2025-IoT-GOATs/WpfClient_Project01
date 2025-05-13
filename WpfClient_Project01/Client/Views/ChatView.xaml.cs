using Client.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Client.Views
{
    /// <summary>
    /// ChatView.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ChatView : UserControl
    {
        public ChatView()
        {
            InitializeComponent();
            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            TxtInput.Focus();

            if (DataContext is ChatViewModel vm)
            {
                // CollectionChanged 구독
                vm.Logs.CollectionChanged += OnLogsChanged;
            }
        }

        private void OnLogsChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (LsbChat.Items.Count > 0)
            {
                LsbChat.ScrollIntoView(LsbChat.Items[LsbChat.Items.Count - 1]);
            }
        }
        private void TxtInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (DataContext is LoginViewModel vm && vm.ConnectCommand.CanExecute(null))
                {
                    vm.ConnectCommand.Execute(null);
                }
            }
        }

    }
}
