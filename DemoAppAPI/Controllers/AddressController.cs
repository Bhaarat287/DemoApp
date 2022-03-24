using DemoAppAPI.CRUDAPI;
using DemoAppAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DemoAppAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]    
    public class AddressController : ControllerBase
    {   
        private readonly IOperations _Operations;

        public AddressController( IOperations operations)
        {
            _Operations = operations;
        }

        [HttpGet]
        public IEnumerable<Address> Get()
        {
            return _Operations.ListAddress();
        }
        [HttpGet("GetById/{id}")]
        public Address Get(int id)
        {
            return _Operations.DetailAddress(id);
        }
        [HttpGet("SearchAddress/{searchText}")]
        public IEnumerable<Address> SearchAddress(string searchText)
        {
            return _Operations.SearchAddress(searchText);
        }
        [HttpPut]
        public bool Put(int id, Address address)
        {
            return _Operations.EditAddress(id, address);
        }
        [HttpPost]
        public bool Post(Address address)
        {
            return _Operations.CreateAddress(address);
        }
        [HttpDelete]
        public bool Delete(int id)
        {
            return _Operations.DeleteAddress(id);
        }

    }
}