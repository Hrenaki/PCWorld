using Microsoft.AspNetCore.Authorization;

namespace Web.Settings
{
   internal class AuthPolicy
   {
      public string Name { get; private set; }
      public Action<AuthorizationPolicyBuilder> Action { get; private set; }

      public AuthPolicy(string name, Action<AuthorizationPolicyBuilder> action)
      {
         Name = name;
         Action = action;
      }
   }

   internal static class AuthPolicies
   {
      public static readonly AuthPolicy AllUsersPolicy = 
                                        new AuthPolicy("allUsers", policy => policy.RequireAuthenticatedUser());
      public static readonly AuthPolicy AdminOnlyPolicy = 
                                        new AuthPolicy("adminOnly", policy => policy.RequireRole("admin"));
   }
}