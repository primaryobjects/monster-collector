using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

[Route("api/audit")]
[ApiController]
public class AuditController : ControllerBase
{
    [HttpGet("{id}")]
    public IActionResult GetAuditLog(string id)
    {
        var auditLogs = AuditManager.Find(id);

        return auditLogs.Count > 0 ? new JsonResult(auditLogs) : NotFound();
    }
}