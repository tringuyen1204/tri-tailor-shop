using System.Collections;
using TMPro;
using TriTailorShop.Data;
using TriTailorShop.GameCharacter;
using UnityEngine;
using UnityEngine.UI;

namespace TriTailorShop.UI
{
    public class PopupCraftingBehaviour : PopupBehaviour
    {
        [SerializeField] 
        protected InventoryUiBehaviour inventoryUI; 

        [SerializeField]
        protected CraftingTableAsset asset;

        [SerializeField] 
        protected Transform transformContent;

        [SerializeField] 
        protected CraftingTabBehaviour tabPrefab;

        [SerializeField] 
        protected GameObject confirmGroup;
        
        // resources requirement
        [SerializeField] protected Image[] imgResources;  
        [SerializeField] protected TextMeshProUGUI[] txtResources;
        [SerializeField] protected Image imgEquipment;

        
        private CraftingFormula m_currentCraftingFormula;
        
        private Player m_player;

        private void Awake()
        {
            m_player = Player.Main;
        }

        private void Start()
        {
            inventoryUI.PairInventory(m_player.Inventory);
            
            foreach (var definition in asset.craftingDefinitions)
            {
                var tab = Instantiate(tabPrefab, transformContent, false);
                tab.SetCraftingData(definition);
                tab.gameObject.SetActive(true);
                tab.btnCraft.onClick.AddListener( ()=> OnCraftItem(definition) );
            }
        }

        public void CloseConfirmPopup()
        {
            confirmGroup.gameObject.SetActive(false);
        }
        
        private void OnCraftItem(CraftingFormula formula)
        {
            if (m_player.Inventory.IsFull)
            {
                HudController.ShowNotification("Inventory is full!", Color.red);;
            }
            // if can craft, then open a confirm popup
            else if (CanCraft(formula))
            {
                m_currentCraftingFormula = formula;
                confirmGroup.gameObject.SetActive(true);
                confirmGroup.GetComponent<CraftingTabBehaviour>().SetCraftingData(formula);
            }
            else
            {
                HudController.ShowNotification("Not enough meterials!", Color.red);
            }
        }

        private bool CanCraft(CraftingFormula formula)
        {
            foreach (var requirement in formula.requirements)
            {
                var resource = m_player.GetResourceData(requirement.id);
                if (resource.Quantity < requirement.qty)
                {
                    return false;
                }
            }

            return true;
        }

        
        // triggered by confirm button
        public void Craft()
        {
            foreach (var requirement in m_currentCraftingFormula.requirements)
            {
                ResourceData resourceData = m_player.GetResourceData(requirement.id);
                resourceData.TrySpend(requirement.qty);
            }
            
            ItemData item = new ItemData(m_currentCraftingFormula.equipId);
            m_player.Inventory.AddItem(item);
            
            StartCoroutine(BounceLastSlot());
            CloseConfirmPopup();
        }

        // UI effect
        IEnumerator BounceLastSlot()
        {
            yield return new WaitForSeconds(0.1f);
            ItemSlotUiBehaviour slotUI = inventoryUI.GetLastItemSlot();
            slotUI.Bounce();
        }
    }
}
