using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VTS.Core.Constants;

namespace VTS.Web.Controllers
{
    /// <summary>
    /// Controller for Manager role.
    /// </summary>
    [Authorize(Policy = Policies.ManagerOnly)]
    public class ManagerController : Controller
    {
    }
}