namespace Inventory.Models
{
    /// <summary>
    /// Inventory Request Model for GET
    /// </summary>
    public class InventoryRequest
    {
      
        public string MaterialNumbers { get; set; }
    
        public string CustomerNumber { get; set; } = null;
   
        public string PlantId { get; set; } = null;
    }
}