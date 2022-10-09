using Core;
using Core.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Console
{
   public interface ICommand
   {
      public string Name { get; init; }
      public void Execute(object parameter);
   }

   internal class RelayCommand : ICommand
   {
      private Action<object> execute;
      public string Name { get; init; }

      public RelayCommand(Action<object> execute, string name)
      {
         this.execute = execute;
         Name = name;
      }

      public void Execute(object parameter)
      {
         execute(parameter);
      }
   }
}