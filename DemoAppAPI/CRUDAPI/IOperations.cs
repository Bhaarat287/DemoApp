using DemoAppAPI.Models;

namespace DemoAppAPI.CRUDAPI
{
    public interface IOperations
    {
        IEnumerable<Address> ListAddress();
        bool CreateAddress(Address Address);
        Address DetailAddress(int id);
        bool EditAddress(int id, Address Address);
        bool DeleteAddress(int id);
        IEnumerable<Address> SearchAddress(string searchText);
        bool ValidateKey(string apiKey);
    }
}
