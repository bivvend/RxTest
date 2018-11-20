using System;
using System.Collections.Generic;
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

namespace RxTests
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        RxTestsViewModel viewModel { get; set; }
        RxTestModel model { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            model = new RxTestModel();
            viewModel = new RxTestsViewModel(model);
            viewModel.Title = "RxTests";

            this.DataContext = viewModel;
        }
    }
}
