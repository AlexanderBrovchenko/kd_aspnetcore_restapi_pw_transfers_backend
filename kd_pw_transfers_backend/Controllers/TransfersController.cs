using AutoMapper;
using kd_pw_transfers_backend.Models;
using kd_pw_transfers_backend.Resources;
using kd_pw_transfers_backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace kd_pw_transfers_backend.Controllers
{
    [Route("api/protected")]
    [ApiController]
    public class TransfersController : ControllerBase
    {
        private IProtectedService _transferService;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;

        public TransfersController(
            IProtectedService transferService,
            IMapper mapper,
            IOptions<AppSettings> appSettings)
        {
            _transferService = transferService;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }

        [Authorize]
        [HttpPost("initializetransfer")]
        public IActionResult InitializeTransfer([FromBody] AmountForPayeeModel model)
        {
            try
            {
                int userid = Int32.Parse(User.Claims.Where(x => x.Type == ClaimTypes.Email).FirstOrDefault()?.Value);
                User user = _transferService.GetById(userid);
                string payeename;
                Transfer transfer = _transferService.CreateTransfer(user, model.PayeeId, model.Amount, out payeename);
                int newBalance = _transferService.UserBalance(user);
                return Ok(new
                {
                    Id = transfer.Id,
                    CorrespondedTo = payeename,
                    Amount = transfer.Amount,
                    OperatedAt = transfer.OperatedAt,
                    Balance = newBalance
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }

        }

        [Authorize]
        [HttpGet("transferlist")]
        public IActionResult TransferList()
        {
            try
            {
                int userid = Int32.Parse(User.Claims.Where(x => x.Type == ClaimTypes.Email).FirstOrDefault()?.Value);
                IEnumerable<Transfer> userTransfers = _transferService.GetByUserId(userid);
                var model = _mapper.Map<IEnumerable<Transfer>, IList<TransferForListing>>(userTransfers);
                return Ok(model);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }

        }
    }
}
