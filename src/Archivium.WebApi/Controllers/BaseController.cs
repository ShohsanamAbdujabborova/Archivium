using Archivium.WebApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace Archivium.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[CustomAuthorize]
public class BaseController : ControllerBase
{ }