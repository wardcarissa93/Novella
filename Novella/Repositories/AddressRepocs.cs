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

                return address.PkAddressId;  
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return -1;  
            }
        }

    }
}