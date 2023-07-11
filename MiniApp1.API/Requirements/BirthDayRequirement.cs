using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace MiniApp1.API.Requirements
{
    public class BirthDayRequirement:IAuthorizationRequirement
    {
        public int Age { get; set; }
        public BirthDayRequirement(int age)
        {
            Age = age;
        }
    }

    public class BirthDayRequirementHandler : AuthorizationHandler<BirthDayRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, BirthDayRequirement requirement)
        {
            var birthDay = context.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.DateOfBirth);

            if (birthDay == null)
            {
                context.Fail();
                return Task.CompletedTask;
            }
            var today = DateTime.Today;
            var age = today.Year - Convert.ToDateTime(birthDay.Value).Year;

            if (age >= requirement.Age)
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }
            return Task.CompletedTask;

        }
    }
}
