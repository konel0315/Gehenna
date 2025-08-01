using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Gehenna
{
    public class TitleUI : BaseUI<TitleUIModel>, IInputHandler
    {
        [SerializeField] private Button newGameButton;
        [SerializeField] private Button continueGameButton;
        [SerializeField] private Button settingButton;
        [SerializeField] private List<Button> buttons;
        private int currentIndex;
        
        public override UILayerType LayerType => UILayerType.Overlay;
        
        private void Awake()
        {
            newGameButton.onClick.AddListener(OnClickedNewGame);
            continueGameButton.onClick.AddListener(OnClickedContinueGame);
            settingButton.onClick.AddListener(OnClickedSetting);
        }

        protected override void OnOpen()
        {
            base.OnOpen();
            
            currentIndex = 0;
            buttons[currentIndex].Select();
            
            model.RegisterInputHandler(this);
        }

        protected override void OnClose()
        {
            base.OnClose();
        }

        public void Move(Vector2 direction)
        {
            if (direction.x > 0.1f)
            {
                currentIndex = (currentIndex + 1) % buttons.Count;
                buttons[currentIndex].Select();
            }
            else if (direction.x < -0.1f)
            {
                currentIndex = (currentIndex - 1 + buttons.Count) % buttons.Count;
                buttons[currentIndex].Select();
            }
        }

        public void Submit()
        {
            buttons[currentIndex].onClick?.Invoke();
        }

        public void Cancel() { }
        
        private void OnClickedNewGame()
        {
            model.OnNewGame.Invoke();
        }

        private void OnClickedContinueGame()
        {
            model.OnContinueGame?.Invoke();
        }

        private void OnClickedSetting()
        {
            model.OnSetting?.Invoke();
        }
    }
}