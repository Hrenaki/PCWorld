using Console.Configuration;
using Core;
using Core.Configuration;

namespace Console
{
   public class Program
   {
      public static void Main(string[] args)
      {
         IConfiguration configuration = new ConfigurationBuilder().AddModule<ConsoleConfigurationModule>().Build();
         ConsoleViewModel viewModel = new ConsoleViewModel(configuration);
         ConsoleEngine engine = new ConsoleEngine(viewModel);
         
         CancellationTokenSource cs = new CancellationTokenSource();
         CancellationToken token = cs.Token;
         
         Task task = new Task(() => engine.Run(token), token);
         
         task.Start();
         
         task.Wait();
      }
   }
}