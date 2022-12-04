using Spine.Unity;
using TriTailorShop.Data;
using TriTailorShop.GameCharacter;
using TriTailorShop.Spine;
using TriTailorShop.UI;
using UnityEngine;

namespace TriTailorShop
{
    public class MainCharacterBehaviour : MonoBehaviour
    {
        #region Definitions

        [SerializeField]
        protected float moveSpeed;
        
        [SerializeField]
        protected PlayerDataAsset playerDataAsset;
        
        [SerializeField]
        protected SkeletonAnimation skeletonAnimation;

        private SpineRegionChangerBehaviour m_spineRegionChanger;
        
        private bool m_facingRight;
        private Vector2 m_moveDirection;
        private Rigidbody2D m_body2D;
        private State m_state;
        private Player m_player;

        // store previous state to compare
        private State m_prevState;

        public static MainCharacterBehaviour Instance { get; private set; }
        
        public enum State
        {
            Idle = 0,
            Moving = 1,
            Working = 2,
        }
        
        #endregion
       
        #region Main Functions
    
        void Awake()
        {
            m_body2D = GetComponent<Rigidbody2D>();
            m_facingRight = true;
            m_player = new Player();
            m_player.LoadFromAsset(playerDataAsset);
            m_spineRegionChanger = skeletonAnimation.GetComponent<SpineRegionChangerBehaviour>();
            m_player.onHelmetChanged.AddListener(OnHelmetChanged);
            m_player.onClothChanged.AddListener(OnClothChanged);

            Player.Main = m_player;
       
            Instance = this;
        }

        private void OnHelmetChanged()
        {
            var helmet = m_player.EquippedHelmet;
            if (helmet != null)
            {
                m_spineRegionChanger.EquipEquipment(ItemType.Helmet, helmet.masterData.id);
            }
            else
            {
                m_spineRegionChanger.EquipEquipment(ItemType.Helmet, "helmet_default");
            }
        }

        private void OnClothChanged()
        {
            var cloth = m_player.EquippedCloth;
            if (cloth != null)
            {
                m_spineRegionChanger.EquipEquipment(ItemType.Cloth, cloth.masterData.id);
            }
            else
            {
                m_spineRegionChanger.EquipEquipment(ItemType.Cloth, "cloth_default");
            }
        }

        public void SetState(State state)
        {
            m_state = state;
        }

        void Update()
        {
            if (!PopupBehaviour.UiHandlingInput)
            {
                HandleInputs();
            }
            UpdateMove();
            UpdateAnimation();
            ApplyFacingDirection();
        }
        
        #endregion

        #region Implementations

        void HandleInputs()
        {
            // reset direction
            m_moveDirection = new Vector2();

            if (m_state == State.Working) return;
            
            m_state = State.Idle;
        
            // up
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                m_moveDirection += Vector2.up;
                m_state = State.Moving;
            }
            
            // down
            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                m_moveDirection += Vector2.down;
                m_state = State.Moving;
            }

            // right
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                m_facingRight = true;
                m_moveDirection += Vector2.right;
                m_state = State.Moving;
            }
        
            // left
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                m_facingRight = false;
                m_moveDirection += Vector2.left;
                m_state = State.Moving;
            }
            
            // press I to open Inventory
            if (Input.GetKey(KeyCode.I)) 
            {
                PopupManagerBehaviour.Instance.ShowInventory();
            }
            
            // press H to open Help
            if (Input.GetKey(KeyCode.H)) 
            {
                PopupManagerBehaviour.Instance.ShowHelp();
            }

            if (Input.GetKey(KeyCode.Escape))
            {
                PopupManagerBehaviour.Instance.ShowConfirmQuit();
            }
        }

        void UpdateMove()
        {
            var direction = m_moveDirection.normalized;
        
            // optimize multiplication
            m_body2D.position += direction * (moveSpeed * Time.deltaTime); 
        }
        
        void UpdateAnimation()
        {
            if (m_state != m_prevState)
            {
                switch (m_state)
                {
                    case State.Idle:
                        SetAnimation("Stand");
                        break;
                    
                    case State.Moving:
                        SetAnimation("Run");
                        break;
                    
                    case State.Working:
                        SetAnimation("Attack2");
                        break;
                }
                
                // cache previous stage to avoid animation reset 
                m_prevState = m_state;
            }
        }

        // look left or right, I choose the rotation Y rather than set scale X to -1
        // in case we want to modify scale
        // but it's up to the whole game concept to design what to use to flip the character
        void ApplyFacingDirection()
        {
            if (m_facingRight)
            {
                skeletonAnimation.transform.eulerAngles = new Vector3(0, 0, 1);
            }
            else
            {
                skeletonAnimation.transform.eulerAngles = new Vector3(0, 180, 1);
            }
        }

        // set animation to spine skeleton
        void SetAnimation(string animName)
        {
            var state = skeletonAnimation.AnimationState;
            state.SetAnimation(1, animName, true);
        }
        
        #endregion
    }
}


