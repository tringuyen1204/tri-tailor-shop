using TMPro;
using UnityEngine;

namespace TriTailorShop.UI
{
    public class NotificationUIBehaviour : MonoBehaviour
    {
        [SerializeField]
        protected TextMeshProUGUI txtContent;

        public void SetText(string content, Color color)
        {
            txtContent.text = content;
            txtContent.color = color;
        }
    }
}