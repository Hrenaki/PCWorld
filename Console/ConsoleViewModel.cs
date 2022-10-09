using Core.Configuration;
using Core.OrderZone;
using Core.SearchZone;
using Core.UserZone;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SystemConsole = System.Console;

namespace Console
{
   internal class ConsoleViewModel
   {
      private User? currentUser;
      public User? CurrentUser
      {
         get => currentUser;
         set
         {
            if (value != currentUser)
               currentUser = value;
         }
      }

      private List<Product> searchedProducts = new List<Product>();
      public IReadOnlyList<Product> SearchedProducts => searchedProducts.AsReadOnly();

      private IConfiguration configuration;

      public bool Closed { get; private set; } = false;

      private RelayCommand showCommandListCommand;
      private RelayCommand signInCommand;
      private RelayCommand registerCommand;
      private RelayCommand accountInfoCommand;
      private RelayCommand searchCommand;
      private RelayCommand logOutCommand;
      private RelayCommand viewBasketCommand;
      private RelayCommand viewOrdersCommand;
      private RelayCommand addProductToBasket;
      private RelayCommand removeProductFromBasket;
      private RelayCommand clearCommand;
      private RelayCommand closeCommand;

      private const string CommandOutputPrefix = "--> ";
      private const string CommandInputPrefix = "<-- ";

      private const string ProductFormat = "<{0}> - Name: '{1}', Price: '{2}$', Category: '{3}'";
      private const string CommandFormat = "<{0}> - {1}";
      private const string BasketItemFormat = "<{0}> - Name: '{1}', UnitPrice: '{2}$', Quantity: '{3}'";
      private const string OrderFormat = "<{0}> - Hash: '{1}'";

      private List<ICommand> defaultInterface = new List<ICommand>();
      private List<ICommand> signedUserInterface = new List<ICommand>();
      private List<ICommand> closedInterface = new List<ICommand>();

      private List<ICommand> currentInterface;
      public IReadOnlyList<ICommand> Commands => currentInterface.AsReadOnly();

      public ConsoleViewModel(IConfiguration configuration)
      {
         this.configuration = configuration;

         InitCommands();
         InitInterfaces();

         currentInterface = defaultInterface;
      }

      private void InitCommands()
      {
         showCommandListCommand = new RelayCommand(obj =>
         {
            int i = 1;
            foreach (var command in currentInterface)
            {
               SystemConsole.WriteLine(string.Format(CommandFormat, i++, command.Name));
            }
         }, "Show commands");

         signInCommand = new RelayCommand(obj =>
         {
            var username = Input("Enter username: ");
            var password = Input("Enter password: ");

            var authService = configuration.Get<IUserAuthenticationService>();

            User? user;
            var result = authService.TrySignIn(username, password, out user);
            if (result.Successful)
            {
               CurrentUser = user;
               currentInterface = signedUserInterface;
            }

            OutputLine(result.Message);

            showCommandListCommand.Execute(null);
         }, "Sign in");

         registerCommand = new RelayCommand(obj =>
         {
            var username = Input("Enter username: ");
            var password = Input("Enter password: ");
            var email = Input("Enter email: ");

            var authService = configuration.Get<IUserAuthenticationService>();

            User? user;
            var result = authService.TryRegister(username, password, email, out user);
            if (result.Successful)
            {
               CurrentUser = user;
               currentInterface = signedUserInterface;
            }

            OutputLine(result.Message);
            showCommandListCommand.Execute(null);

         }, "Register");

         accountInfoCommand = new RelayCommand(obj =>
         {
            if (CurrentUser == null)
               return;

            OutputLine("Username: " + CurrentUser.Name);
            OutputLine("Email: " + CurrentUser.Email);

         }, "View account info");

         searchCommand = new RelayCommand(obj =>
         {
            var request = Input("Enter text: ");

            var productService = configuration.Get<IProductService>();
            var products = productService.GetProducts(new NameFilter(request).Or(new CategoryFilter(request)));

            int i = 0;
            foreach (var product in products)
            {
               OutputLine(string.Format(ProductFormat, ++i, product.Name, product.Price, product.CategoryInfo.Name));
            }

            searchedProducts.Clear();
            searchedProducts.AddRange(products);

         }, "Search products");

         logOutCommand = new RelayCommand(obj =>
         {
            if (CurrentUser == null)
               return;

            CurrentUser = null;

            currentInterface = defaultInterface;

            OutputLine("Logged out");

            if(obj == null)
               showCommandListCommand.Execute(null);

         }, "Log out");

         viewBasketCommand = new RelayCommand(obj =>
         {
            if (CurrentUser == null)
               return;

            var items = CurrentUser.Basket.Items;
            if(!items.Any())
            {
               OutputLine("No items");
               return;
            }

            int i = 0;
            foreach (var item in CurrentUser.Basket.Items)
            {
               OutputLine(string.Format(BasketItemFormat, ++i, item.ProductName, item.ProductPrice, item.Quantity));
            }

         }, "View basket");

         addProductToBasket = new RelayCommand(obj =>
         {
            if (CurrentUser == null)
               return;

            if(!searchedProducts.Any())
            {
               OutputLine("You need to search products first");
               return;
            }

            int productNumber = int.Parse(Input("Enter searched product number: "));
            if(productNumber < 1 || productNumber > searchedProducts.Count)
            {
               OutputLine("Product number is out of range");
               return;
            }

            int quantity = int.Parse(Input("Enter quantity: "));

            if(quantity <= 0)
            {
               OutputLine("Quantity must be positive");
               return;
            }

            CurrentUser.Basket.AddProduct(searchedProducts[productNumber - 1], quantity);

            OutputLine("Added");
         }, "Add product to basket");

         removeProductFromBasket = new RelayCommand(obj =>
         {
            if (CurrentUser == null)
               return;

            int productNumber = int.Parse(Input("Enter product number in basket: "));
            var basketItems = CurrentUser.Basket.Items;
            if (productNumber < 1 || productNumber > basketItems.Count)
            {
               OutputLine("Product number is out of range");
               return;
            }

            CurrentUser.Basket.RemoveProduct(basketItems[productNumber - 1]);

         }, "Remove product from basket");

         viewOrdersCommand = new RelayCommand(obj =>
         {
            if (CurrentUser == null)
               return;

            var orderService = configuration.Get<IOrderService>();
            var orders = orderService.GetAllUserOrders(CurrentUser);

            if(!orders.Any())
            {
               OutputLine("No orders");
               return;
            }

            int i = 0;
            foreach(var order in orders)
            {
               OutputLine(string.Format(OrderFormat, ++i, order.Hash));
            }

         }, "View orders");

         clearCommand = new RelayCommand(obj =>
         {
            Clear();
            showCommandListCommand.Execute(null);
         }, "Clear");

         closeCommand = new RelayCommand(obj =>
         {
            Close();
            OutputLine("Closed");
         }, "Close");
      }

      private void InitInterfaces()
      {
         InitDefaultInterface();
         InitSignedUserInterface();
      }

      private void InitDefaultInterface()
      {
         defaultInterface.Add(showCommandListCommand);
         defaultInterface.Add(signInCommand);
         defaultInterface.Add(registerCommand);
         defaultInterface.Add(searchCommand);
         defaultInterface.Add(clearCommand);
         defaultInterface.Add(closeCommand);
      }

      private void InitSignedUserInterface()
      {
         signedUserInterface.Add(showCommandListCommand);
         signedUserInterface.Add(accountInfoCommand);
         signedUserInterface.Add(viewBasketCommand);
         signedUserInterface.Add(addProductToBasket);
         signedUserInterface.Add(removeProductFromBasket);
         signedUserInterface.Add(viewOrdersCommand);
         signedUserInterface.Add(searchCommand);
         signedUserInterface.Add(clearCommand);
         signedUserInterface.Add(logOutCommand);
         signedUserInterface.Add(closeCommand);
      }

      private string Input(string preMessage)
      {
         SystemConsole.Write(CommandInputPrefix + preMessage);
         return SystemConsole.ReadLine();
      }

      private void OutputLine(string str)
      {
         SystemConsole.WriteLine(CommandOutputPrefix + str);
      }

      private void Clear()
      {
         SystemConsole.Clear();
      }

      public void Close()
      {
         if (Closed)
            throw new Exception("ViewModel is already closed");

         logOutCommand.Execute(false);

         Closed = true;
         currentInterface = closedInterface;
      }
   }
}