using System;
using System.Linq;

namespace Oracle.Data
{
    public static class OracleRepository
    {
        public static void ReplicateOracleToMSSQL()
        {
           //var sqlContex = new MSSQLSupermarketEntities();
           var orcContex = new OracleDbContext();

            //var orcMeasures = orcContex.Measures
            ////    .Select(m => m.MeasureName)
            ////    .ToList();

            //foreach (var orcMeasure in orcMeasures)
            //{
            //    Console.WriteLine(orcMeasure);
            //}

        }
    }
}