namespace BackShopCore.Data
{
    public interface IApplicationDbContext
    {
        //DbSet<Customer> Customers { get; set; }
        public int SaveChanges();
    }
}