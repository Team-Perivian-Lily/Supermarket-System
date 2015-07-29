namespace SQLLite.Data
{
    public static class SQLiteRepository
    {
        public static void Test()
        {
            var ctx = new SQLiteEntities();

            ctx.Taxes.Add(new Tax()
            {
                ProductName = "Bira",
                Tax1 = 20
            });

            ctx.SaveChanges();
        }
    }
}