using System;
using TMPro;
using TriTailorShop.Data;
using TriTailorShop.GameCharacter;
using TriTailorShop.UI;
using UnityEngine;

namespace TriTailorShop.InteractionHandler
{
    public class InteractionHandlerConvertResource : InteractionHandler
    {
        [SerializeField]
        protected float holdConfirmInterval = 0.5f;

        [SerializeField]
        protected float shakeInterval = 0.5f;
        
        [SerializeField]
        protected SpriteRenderer rendererProgress;

        [SerializeField] 
        protected float rendererMaxWidth;
        
        [SerializeField] 
        protected string resourceInput;
        
        [SerializeField] 
        protected int quantityInput;

        [SerializeField] 
        protected string resourceOutput;
        
        [SerializeField] 
        protected int quantityOutput;

        [SerializeField] 
        protected Animator workTableAnimator;

        [SerializeField] 
        protected GameObject groupError;

        [SerializeField]
        protected GameObject groupWorking;

        [SerializeField] 
        protected TextMeshPro txtError;

        [SerializeField] protected Animator notifyAnimator;
        
        private float m_holdTime;
        private bool m_shaking;
        private ResourceData m_inputResource;
        private ResourceData m_outputResource;
        
        public bool CanConvert
        {
            get
            {
                var inputResource = Player.Main.GetResourceData(resourceInput);
                return inputResource.Quantity >= quantityInput;
            }
        }

        private void Convert()
        {

            if (m_inputResource.TrySpend(quantityInput))
            {
                m_outputResource.Add(quantityOutput);
                notifyAnimator.SetTrigger("Show");
            }
        }

        private void Start()
        {
            m_inputResource = Player.Main.GetResourceData(resourceInput);
            m_outputResource = Player.Main.GetResourceData(resourceOutput);
        }

        private void Update()
        {
            if (!inParameter) return;
            if (PopupBehaviour.UiHandlingInput) return;

            bool canConvert = CanConvert;
            
            if (Input.GetKey(KeyCode.E) && canConvert)
            {
                MainCharacterBehaviour.Instance.SetState(MainCharacterBehaviour.State.Working);
                
                m_holdTime += Time.deltaTime;

                if (m_holdTime >= shakeInterval && !m_shaking)
                {
                    m_shaking = true;
                    workTableAnimator.SetTrigger("Shake");
                }
                
                if (m_holdTime >= holdConfirmInterval)
                {
                    Convert();
                    m_holdTime = 0;
                    m_shaking = false;
                }
            }
            else
            {
                MainCharacterBehaviour.Instance.SetState(MainCharacterBehaviour.State.Idle);
                m_holdTime = 0;
            }

            var size = rendererProgress.size;
            size.x = Mathf.Min( m_holdTime / holdConfirmInterval, 1) * rendererMaxWidth;
            rendererProgress.size = size;

            if (canConvert)
            {
                groupWorking.gameObject.SetActive(true);
                groupError.gameObject.SetActive(false);
            }
            else
            {
                groupWorking.gameObject.SetActive(false);
                groupError.gameObject.SetActive(true);

                txtError.text = $"{m_inputResource.Quantity}/{quantityInput}";
            }
        }
    }
}