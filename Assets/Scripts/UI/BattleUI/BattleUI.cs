using System;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Gehenna
{
    public class BattleUI : BaseUI<BattleUIModel>, IInputHandler
    {
        [FoldoutGroup("Portrait")] [SerializeField] private Image portraitImage;
        [FoldoutGroup("Portrait")] [SerializeField] private TextMeshProUGUI nameText;
        [FoldoutGroup("Portrait")] [SerializeField] private Slider hpSlider;
        [FoldoutGroup("Portrait")] [SerializeField] private Slider manaSlider;
        [FoldoutGroup("Portrait")] [SerializeField] private TextMeshProUGUI hpText;
        [FoldoutGroup("Portrait")] [SerializeField] private TextMeshProUGUI manaText;

        [FoldoutGroup("Commands")] [SerializeField] private RectTransform commandRoot;
        [FoldoutGroup("Commands")] [SerializeField] private BattleCommandUI attackButton;
        [FoldoutGroup("Commands")] [SerializeField] private BattleCommandUI moveButton;
        [FoldoutGroup("Commands")] [SerializeField] private BattleCommandUI skillButton;
        [FoldoutGroup("Commands")] [SerializeField] private BattleCommandUI itemButton;
        [FoldoutGroup("Commands")] [SerializeField] private BattleCommandUI escapeButton;

        [FoldoutGroup("Skill")] [SerializeField] private RectTransform skillRoot;
        [FoldoutGroup("Skill")] [SerializeField] private BattleSkillUI skill1Button;
        [FoldoutGroup("Skill")] [SerializeField] private BattleSkillUI skill2Button;
        [FoldoutGroup("Skill")] [SerializeField] private BattleSkillUI skill3Button;
        [FoldoutGroup("Skill")] [SerializeField] private BattleSkillUI skill4Button;
        
        [FoldoutGroup("Round")] [SerializeField] private TextMeshProUGUI roundText;
        [FoldoutGroup("Round")] [SerializeField] private BattleActionOrderUI[] actionOrderSlots;
        [FoldoutGroup("Round")] [SerializeField] private Button roundStartButton;
        [FoldoutGroup("Round")] [SerializeField] private Button roundResetButton;
        
        [FoldoutGroup("Order")] [SerializeField] private BattleTurnOrderUI[] turnOrderSlots;

        [SerializeField] private Button speedButton;
        
        public override UILayerType LayerType => UILayerType.Overlay;

        private void Awake()
        {
            attackButton.AddListener(OnClickAttack);
            moveButton.AddListener(OnClickMove);
            skillButton.AddListener(OnClickSkill);
            itemButton.AddListener(OnClickItem);
            escapeButton.AddListener(OnClickEscape);
            
            skill1Button.AddListener(OnClickSkill1);
            skill2Button.AddListener(OnClickSkill2);
            skill3Button.AddListener(OnClickSkill3);
            skill4Button.AddListener(OnClickSkill4);
            
            roundStartButton.onClick.AddListener(OnClickRoundStart);
            roundResetButton.onClick.AddListener(OnClickRoundReset);
        }

        protected override void OnOpen()
        {
            base.OnOpen();

            model.RegisterInputHandler(this);
            UpdateViews();
        }

        protected override void OnClose()
        {
            base.OnClose();
        }
        
        public void Move(Vector2 direction)
        {
            
        }

        public void Submit()
        {
            
        }

        public void Cancel()
        {
            
        }
        
        private void UpdateViews()
        {
            
        }

        private void UpdatePortrait()
        {
            UpdatePortraitImage();
            UpdatePortraitName();
            UpdatePortraitStatus();
        }

        private void UpdatePortraitImage()
        {
            portraitImage.sprite = null;
        }

        private void UpdatePortraitName()
        {
            nameText.text = "";
        }

        private void UpdatePortraitStatus()
        {
            hpSlider.value = 0;
            manaSlider.value = 0;

            hpText.text = "";
            manaText.text = "";
        }
        
        private void UpdateCommand()
        {
            
        }
        
        private void UpdateRound()
        {
            
        }
        
        private void UpdateOrder()
        {
            
        }


        private void UpdateSpeed()
        {
            
        }
        
        private void OnClickAttack()
        {
            
        }
        
        private void OnClickMove()
        {
            
        }
        
        private void OnClickSkill()
        {
            
        }
        
        private void OnClickItem()
        {
            
        }
        
        private void OnClickSkill1()
        {
            
        }

        private void OnClickSkill2()
        {
            
        }

        private void OnClickSkill3()
        {
            
        }

        private void OnClickSkill4()
        {
            
        }
        
        private void OnClickEscape()
        {
            
        }
        
        private void OnClickRoundStart()
        {
            
        }
        
        private void OnClickRoundReset()
        {
            
        }
    }
}