using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace time_report_api.Models
{
    public class Policies
    {
        public const string projectLeader = "ProjectLeader";
        public const string user = "User";

        public static AuthorizationPolicy ProjectLeaderPolicy()
        {
            return new AuthorizationPolicyBuilder().RequireAuthenticatedUser().RequireRole(projectLeader).Build();
        }
        public static AuthorizationPolicy UserPolicy()
        {
            return new AuthorizationPolicyBuilder().RequireAuthenticatedUser().RequireRole(user).Build();
        }
    }
}
