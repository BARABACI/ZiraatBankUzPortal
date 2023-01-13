using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using ZiraatBankUzPortal.Server.Repositories;
using ZiraatBankUzPortal.Shared.DisplayModel;
using ZiraatBankUzPortal.Shared.Model;

namespace ZiraatBankUzPortal.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,ComplianceDep")]
    public class InternalExportExcelController : Controller
    {
        private readonly IInternalExportExcelRepository _internalExportExcelRepository;

        public InternalExportExcelController(IInternalExportExcelRepository internalExportExcelRepository)
        {
            _internalExportExcelRepository = internalExportExcelRepository;
        }

        [HttpGet("GetClientP")]
        public async Task<ActionResult<IEnumerable<InternalClientPDisplayModel>>> GetClientP()
        {
            var data = await _internalExportExcelRepository.GetDataClientPAsync();
            return Ok(data);
        }
        [HttpGet("GetClientP1")]
        public async Task<ActionResult<IEnumerable<InternalClientP1DisplayModel>>> GetClientP1(string startdate, string enddate)
        {
            var data = await _internalExportExcelRepository.GetDataClientP1Async(startdate,enddate);
            return Ok(data);
        }
        [HttpGet("GetGeneralArgh")]
        public async Task<ActionResult<IEnumerable<InternalGeneralArghDisplayModel>>> GetGeneralArgh(string enddate)
        {
            var data = await _internalExportExcelRepository.GetDataGenerealArghAsync(enddate);
            return Ok(data);
        }
        [HttpGet("GetGeneralArgh1")]
        public async Task<ActionResult<IEnumerable<InternalGeneralArghDisplayModel>>> GetGeneralArgh1(string enddate)
        {
            var data = await _internalExportExcelRepository.GetDataGenerealArgh1Async(enddate);
            return Ok(data);
        }
    }
}
