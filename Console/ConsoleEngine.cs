using Core.Configuration;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SystemConsole = System.Console;

namespace Console
{
   internal class ConsoleEngine
   {
      private ConsoleViewModel viewModel;

      public ConsoleEngine(ConsoleViewModel viewModel)
      {
         this.viewModel = viewModel;
      }

      public void Run(CancellationToken token)
      {
         int command;
         object parameter = new object();
         IReadOnlyList<ICommand> commands = viewModel.Commands;
         commands[0].Execute(parameter);

         while (true)
         {
            if(token.IsCancellationRequested)
            {
               viewModel.Close();
               return;
            }

            if (viewModel.Closed)
               return;

            commands = viewModel.Commands;

            SystemConsole.Write("Enter command: ");
            if(!int.TryParse(SystemConsole.ReadLine(), out command))
            {
               SystemConsole.WriteLine("Command should be a number");
               continue;
            }

            if(command < 1 || command > commands.Count)
            {
               SystemConsole.WriteLine("Wrong command");
               continue;
            }

            commands[command - 1].Execute(parameter);
         }
      }
   }
}
