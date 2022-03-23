using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using OtherClassLib.Translations;

namespace WebApplicationExample.Controllers;

[ApiController]
[Route("[controller]")]
public class LocalizeTestController : ControllerBase
{
    private readonly IStringLocalizer<Shared> _sharedLocalizer;
    private readonly IStringLocalizer<LocalizeTestController> _controllerLocalizer;

    public LocalizeTestController(IStringLocalizer<Shared> sharedLocalizer,
        IStringLocalizer<LocalizeTestController> controllerLocalizer)
    {
        _sharedLocalizer = sharedLocalizer;
        _controllerLocalizer = controllerLocalizer;
    }

    [HttpGet("SharedWelcomeMessage")]
    public ActionResult<string> GetSharedWelcomeMessage([FromQuery] string name)
    {
        return Ok(_sharedLocalizer["welcome", name].Value);
    }

    [HttpGet("ControllerTestMessage")]
    public ActionResult<string> GetControllerTestMessage()
    {
        return Ok(_controllerLocalizer["testMessage"].Value);
    }
 }
