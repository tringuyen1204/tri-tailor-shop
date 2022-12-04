using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using TriTailorShop.Data;
using TriTailorShop.GameCharacter;
using UnityEngine;
using UnityEngine.Serialization;

namespace TriTailorShop.UI
{
    public class ResourceDisplayBehaviour : MonoBehaviour
    {
        [SerializeField]
        protected string resourceId;
        
        [FormerlySerializedAs("indicator")] [SerializeField]
        protected GameObject indicatorPrefab;
        
        [SerializeField]
        protected TextMeshProUGUI txtValue;

        private ResourceData m_resourceData;
        private Animator m_animator;

        private void Awake()
        {
            m_animator = GetComponent<Animator>();
            m_resourceData = Player.Main.GetResourceData(resourceId);
        }

        private void Start()
        {
            txtValue.text = m_resourceData.Quantity + "";
        }

       // show indicator how much the resource changed
       private void OnValueChanged(int oldVal, int newVal)
        {
            txtValue.text = newVal + "";
            m_animator.SetTrigger("Bounce");
            
            var indicator = Instantiate(indicatorPrefab, transform, false).GetComponent<SpendingIndicatorBehaviour>();
            
            indicator.gameObject.SetActive(true);

            var diff = newVal - oldVal;

            if (diff > 0)
            {
                indicator.txtValue.text = "+" + diff;
                indicator.txtValue.color = Color.green;
            }
            else
            {
                indicator.txtValue.text = diff + "";
                indicator.txtValue.color = Color.red;
            }
            
        }
        
        private void OnDisable()
        {
            m_resourceData.onValueChanged.RemoveListener(OnValueChanged);
        }

        private void OnEnable()
        {
            // remove all old indicators
            var objectsToDestroy = new List<SpendingIndicatorBehaviour>();
            
            for (int a = 0; a < transform.childCount; a++)
            {
                var child = transform.GetChild(a);
                var spendingIndicator = child.GetComponent<SpendingIndicatorBehaviour>();
                if (spendingIndicator && spendingIndicator.gameObject.activeSelf)
                {
                    objectsToDestroy.Add(spendingIndicator);
                }
            }

            foreach (var obj in objectsToDestroy)
            {
                obj.transform.SetParent(null);
                Destroy(obj);
            }
            
            m_resourceData.onValueChanged.AddListener( OnValueChanged );
        }

    }
}
