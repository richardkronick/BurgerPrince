namespace BurgerPrince.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedItemAndQuantityofOrderToOrderTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "OrderedItemsAndQuantities", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "OrderedItemsAndQuantities");
        }
    }
}
