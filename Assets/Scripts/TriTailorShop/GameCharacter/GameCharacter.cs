using TriTailorShop.Data;

namespace TriTailorShop.GameCharacter
{
    public class GameCharacter
    {
        protected Inventory inventory;
        
        public Inventory Inventory => inventory;

        protected GameCharacter()
        {
            inventory = new Inventory();
        }
    }
}