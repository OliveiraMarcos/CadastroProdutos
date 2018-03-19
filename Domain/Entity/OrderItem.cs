namespace Domain.Entity
{
    using Domain.Interface;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("OrderItem")]
    public partial class OrderItem : IBaseEntity
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int OrderId { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }

        public virtual Order Order { get; set; }

        public virtual Product Product { get; set; }
    }
}
