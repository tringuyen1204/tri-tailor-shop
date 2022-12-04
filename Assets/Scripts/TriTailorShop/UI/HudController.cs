using System;
using UnityEngine;

namespace TriTailorShop.UI
{
    public class HudController : MonoBehaviour
    {
        [SerializeField]
        protected NotificationUIBehaviour notiPrefab;

        [SerializeField]
        protected GameObject buttonsGroup;

        public static HudController Instance { get; private set; }
        
        public static void ShowNotification(string content, Color color)
        {
            NotificationUIBehaviour newNoti = Instantiate(Instance.notiPrefab, Instance.transform, false);
            newNoti.SetText(content, color);
        }

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            PopupBehaviour.OnPopupAvailableChanged.AddListener( OnPopupCountChanged );
        }

        private void OnPopupCountChanged(int count)
        { 
            buttonsGroup.gameObject.SetActive(count == 0);
        }
    }
}
