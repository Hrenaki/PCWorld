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

using Core;
using Core.Configuration;
using Core.UserZone;
using Desktop.Configuration;

namespace Desktop
{
   /// <summary>
   /// Interaction logic for MainWindow.xaml
   /// </summary>
   public partial class MainWindow : Window
   {
      private IConfiguration configuration;

      public MainWindow()
      {
         InitializeComponent();

         configuration = new ConfigurationBuilder().AddModule<DesktopConfigurationModule>().Build();
      }
   }
}
