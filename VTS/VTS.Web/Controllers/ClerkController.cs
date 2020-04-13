using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VTS.Core.Constants;

namespace VTS.Web.Controllers
{
    /// <summary>
    /// Controller for Clerk role.
    /// </summary>
    [Authorize(Policy = Policies.ClerkOnly)]
    public class ClerkController : Controller
    {
    }
}