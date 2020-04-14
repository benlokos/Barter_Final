using System;
using System.Collections.Generic;
using System.Text;
using Project.Services.Repos;
namespace Project.Services
{
    public class AllRepos
    {
        public readonly AddressRepo Address;
        public readonly Repos.ItemRepo Items;
        public readonly TraderRepo Traders;
        public readonly TagAssocRepo TagAssoc;
        public readonly TagRepo Tags;
        public readonly TransactionRepo Transactions;
       



        public AllRepos(string ConnectionString)
        {
            AddressRepo.SetInstance(ConnectionString);
            TraderRepo.SetInstance(ConnectionString);
            Repos.ItemRepo.SetInstance(ConnectionString);
            TagAssocRepo.SetInstance(ConnectionString);
            TagRepo.SetInstance(ConnectionString);
            TransactionRepo.SetInstance(ConnectionString);


            Address = AddressRepo.GetInstance();
            Traders = TraderRepo.GetInstance();
            Items = Repos.ItemRepo.GetInstance();
            TagAssoc = TagAssocRepo.GetInstance();
            Tags = TagRepo.GetInstance();
            Transactions= TransactionRepo.GetInstance();

        }

    }
}
