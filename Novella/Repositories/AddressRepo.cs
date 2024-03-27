using Novella.Data;
using Novella.EfModels;
using Novella.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Novella.Repositories
{
    public class AddressRepo
    {
        private readonly NovellaContext _db;

        public AddressRepo(NovellaContext db)
        {
            _db = db;
        }

        // Method to save an address
        public int SaveAddressAndGetId(AddressVM addressVM)
        {
            try
            {
                var address = new Address
                {
                    AddressLineOne = addressVM.AddressLine1,
                    AddressLineTwo = addressVM.AddressLine2,
                    City = addressVM.City,
                    Province = addressVM.Province,
                    PostalCode = addressVM.PostalCode
                };

                _db.Addresses.Add(address);
                _db.SaveChanges();

                return address.PkAddressId; // Return the ID of the saved address
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return -1; // Return -1 or some other error code to indicate failure
            }
        }

    }
}
