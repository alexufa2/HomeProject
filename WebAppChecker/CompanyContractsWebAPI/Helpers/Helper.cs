using CompanyContractsWebAPI.Models.DB;
using CompanyContractsWebAPI.Models.RabbitMq.Messages;

namespace CompanyContractsWebAPI.Helpers
{
    public static class Helper
    {
        public static Tdb Convert<Tdb, Udto>(Udto item)
        {
            Tdb result = (Tdb)Activator.CreateInstance(typeof(Tdb));
            CopyFields(item, result);
            return result;
        }

        private static void CopyFields<T, U>(T sourceItem, U destItem)
        {
            if (sourceItem == null || destItem == null)
                return;

            var sourceType = typeof(T);
            var destType = typeof(U);

            foreach (var sourceField in sourceType.GetFields())
            {
                var destField = destType.GetField(sourceField.Name);
                if (destField != null)
                {
                    destField.SetValue(destItem, sourceField.GetValue(sourceItem));
                }
            }

            foreach (var sourceProp in sourceType.GetProperties())
            {
                var destProp = destType.GetProperties().FirstOrDefault(f => f.Name == sourceProp.Name);
                if (destProp != null)
                {
                    destProp.SetValue(destItem, sourceProp.GetValue(sourceItem, null), null);
                }
            }
        }

        public static Contract ConvertToContract(ContractCreated item)
        {
            var contract = Convert<Contract, ContractCreated>(item);
            contract.Id = 0;
            contract.Done_Sum = 0;
            return contract;
        }

        public static ContractDone ConvertToContractDone(ContractDoneCreated item)
        {
            var contractDone = Convert<ContractDone, ContractDoneCreated>(item);
            contractDone.Id = 0;
            return contractDone;
        }
    }
}
