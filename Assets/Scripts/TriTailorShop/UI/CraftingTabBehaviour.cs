using TMPro;
using TriTailorShop.Data;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace TriTailorShop.UI
{
    public class CraftingTabBehaviour : MonoBehaviour
    {
        // resources requirement
        [SerializeField] protected Image[] imgResources;  
        [SerializeField] protected TextMeshProUGUI[] txtResources;

        [FormerlySerializedAs("imgEquip")] [SerializeField] protected Image imgEquipment;

        public Button btnCraft;
        
        private CraftingFormula m_formula;

        public void SetCraftingData(CraftingFormula formula)
        {
            m_formula = formula;

            if (m_formula == null) return;

            var requirements = m_formula.requirements;

            imgEquipment.sprite = ItemUtilities.GetIconFromId(m_formula.equipId);
            
            for (int a = 0; a < 3; a++)
            {
                if (a < requirements.Length)
                {
                    imgResources[a].gameObject.SetActive(true);
                    imgResources[a].sprite = ItemUtilities.GetIconFromId(requirements[a].id);
                    txtResources[a].text = requirements[a].qty + "";
                }
                else
                {
                    imgResources[a].gameObject.SetActive(false);
                }
            }
        }
    }
}
