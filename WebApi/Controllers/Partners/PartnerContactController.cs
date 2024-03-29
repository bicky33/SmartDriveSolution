﻿using Contract.DTO.Partners;
using Contract.Records;
using Domain.RequestFeatured;
using Microsoft.AspNetCore.Mvc;
using Service.Abstraction.Partners;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers.Partners
{
    [Route("api/[controller]")]
    [ApiController]
    public class PartnerContactController : ControllerBase
    {
        private readonly IServicePartnerManager _servicePartnerManager;

        public PartnerContactController(IServicePartnerManager servicePartnerManager)
        {
            _servicePartnerManager = servicePartnerManager;
        }

        // GET: api/<PartnerContactController>
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            IEnumerable<PartnerContactDTO> patnerContacts = await _servicePartnerManager.ServicePartnerContact.GetAllAsync(false);
            return Ok(patnerContacts);
        }

        // GET: api/<PartnerContactController>
        [HttpGet("paging")]
        public async Task<ActionResult> GetPaging([FromQuery] EntityParameter request)
        {
            PaginationDTO<PartnerContactDTO> patnerContacts = await _servicePartnerManager.ServicePartnerContact.GetAllPagingAsync(request);
            return Ok(patnerContacts);
        }

        // GET api/<PartnerContactController>/5
        [HttpGet("{pacoPatrnEntityid:int}/{pacoUserEntityid:int}")]
        public async Task<ActionResult> Get(int pacoPatrnEntityid, int pacoUserEntityid)
        {
            PartnerContactDTO partnerContact =  await _servicePartnerManager.ServicePartnerContact.GetByIdAsync(pacoPatrnEntityid, pacoUserEntityid, false);
            return Ok(partnerContact);
        }

        [HttpGet("partner/{pacoUserEntityid:int}")]
        public async Task<ActionResult> GetPartner(int pacoUserEntityid, bool trackChanges)
        {
            IEnumerable<PartnerContactDTO> partnerContacts = await _servicePartnerManager.ServicePartnerContact.GetByUserId(pacoUserEntityid, trackChanges);
            return Ok(partnerContacts);
        }

        // POST api/<PartnerContactController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] PartnerContactDTO request)
        {
            PartnerContactDTO  partnerContact = await _servicePartnerManager.ServicePartnerContact.CreateAsync(request);
            return Ok(partnerContact);
        }

        // PUT api/<PartnerContactController>/5
        [HttpPut("{pacoPatrnEntityid:int}/{pacoUserEntityid:int}")]
        public async Task<ActionResult> Put(int pacoPatrnEntityid, int pacoUserEntityid, [FromBody] PartnerContactDTO request)
        {
            await _servicePartnerManager.ServicePartnerContact.UpdateAsync(pacoPatrnEntityid, pacoUserEntityid, request);
            return NoContent();
        }

        // DELETE api/<PartnerContactController>/5
        [HttpDelete("{pacoPatrnEntityid:int}/{pacoUserEntityid:int}")]
        public async Task<ActionResult> Delete(int pacoPatrnEntityid, int pacoUserEntityid)
        {
            await _servicePartnerManager.ServicePartnerContact.DeleteAsync(pacoPatrnEntityid, pacoUserEntityid);
            return NoContent();
        }
    }
}
