using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

namespace Gehenna
{
    public class DialogueUI : BaseUI<DialogueUIModel>
    {
        public override UILayerType LayerType => UILayerType.Overlay;
        public bool IsTyping { get; private set; } = false;
        
        [SerializeField] private TextMeshProUGUI speakerText;
        [SerializeField] private TextMeshProUGUI dialogueText;
        [SerializeField] private Image portraitImage;
        [SerializeField] private List<Button> choiceButtons;
        [SerializeField] private List<GameObject> choiceArrows;
        [SerializeField] private Button autoButton;
        [SerializeField] private Button logButton;
        [SerializeField] private Button nextButton;
        [SerializeField] private GameObject skipUI;
        [SerializeField] private Image skipFill;
        
        private Coroutine typingCoroutine;
        private Queue<(int index, float startTime,string anim)> charAnimQueue  = new();
        
        
        private bool isAutoMode = false;
        public bool IsAutoMode=>isAutoMode;
        
        private int currentCharCount = 0;
        private float autoTimer = 0f;
        private float baseDelayPerChar = 0.1f;
        private float maxAutoDelay = 3f;
        
        public void Start()
        {
            var Autobutton = autoButton;
            Autobutton.onClick.RemoveAllListeners();
            Autobutton.onClick.AddListener(() => SetAutoMode(!isAutoMode));
            
            var LogButton = logButton;
            LogButton.onClick.RemoveAllListeners();
            LogButton.onClick.AddListener(() => model.OnLog());
            
            var NextButton = nextButton;
            NextButton.onClick.RemoveAllListeners();
            NextButton.onClick.AddListener(() => model.OnNext());
        }

        public void ShowChoices(List<ConditionBranch> branches, Action<int> onSelect)
        {
            for (int i = 0; i < choiceButtons.Count; i++)
            {
                if (i < branches.Count)
                {
                    var branch = branches[i];
                    var button = choiceButtons[i];
                    button.gameObject.SetActive(true);
                    button.GetComponentInChildren<TextMeshProUGUI>().text = branch.Condition;

                    int buttonIndex = i;
                    int nextID = branch.NextID;
                    
                    if (i < choiceArrows.Count)
                        choiceArrows[i].SetActive(false);

                    button.onClick.RemoveAllListeners();
                    button.onClick.AddListener(() =>
                    {
                        foreach (var arrow in choiceArrows)
                            arrow.SetActive(false);
                        
                        if (buttonIndex < choiceArrows.Count)
                            choiceArrows[buttonIndex].SetActive(true);

                        onSelect(nextID);
                    });
                }
                else
                {
                    choiceButtons[i].gameObject.SetActive(false);
                    if (i < choiceArrows.Count)
                        choiceArrows[i].SetActive(false);
                }
            }
        }
        public void SetAutoMode(bool isOn)
        {
            isAutoMode = isOn;
        }
        
        public void HideChoices()
        {
            foreach (var button in choiceButtons)
                button.gameObject.SetActive(false);
            foreach (var arrow in choiceArrows)
                arrow.SetActive(false);
        }
        
        public void SetChoiceArrow(int index)
        {
            for (int i = 0; i < choiceArrows.Count; i++)
            {
                choiceArrows[i].SetActive(i == index);
            }
        }
        
        public void UpdateDialogue(DialogueUIModel model)
        {
            speakerText.text = model.Speaker;
            dialogueText.text = "";

            if (typingCoroutine != null)
            {
                StopCoroutine(typingCoroutine);
                typingCoroutine = null;
            }

            if (model.Segments != null && model.Segments.Count > 0)
            {
                typingCoroutine = StartCoroutine(PlayTyping(model.Segments));
            }
            else
            {
                dialogueText.text = model.Text;
            }

            if (!string.IsNullOrEmpty(model.Portrait))
            {
                var sprite = Resources.Load<Sprite>($"Portraits/{model.Portrait}");
                portraitImage.sprite = sprite;
            }
        }

        public void SkipTyping()
        {
            if (typingCoroutine != null)
            {
                StopCoroutine(typingCoroutine);
                typingCoroutine = null;
            }

            if (model is DialogueUIModel dialogueModel)
            {
                dialogueText.text = string.Join("", dialogueModel.Segments.Select(s => s.Content));
                dialogueText.ForceMeshUpdate();
            }

            charAnimQueue.Clear();
            IsTyping = false;
        }

        
        public void ShowSkipUI()
        {
            skipUI.SetActive(true);
            skipFill.fillAmount = 0f;
        }

        public void UpdateSkipGauge(float ratio)
        {
            skipFill.fillAmount = Mathf.Clamp01(ratio);
        }

        public void HideSkipUI()
        {
            skipUI.SetActive(false);
        }
        
        
        protected override void OnOpen()
        {
            if (model is not DialogueUIModel testModel)
                return;

            speakerText.text = testModel.Speaker;
            dialogueText.text = "";

            if (typingCoroutine != null)
            {
                StopCoroutine(typingCoroutine);
                typingCoroutine = null;
            }

            if (testModel.Segments != null && testModel.Segments.Count > 0)
            {
                typingCoroutine = StartCoroutine(PlayTyping(testModel.Segments));
            }
            else
            {
                dialogueText.text = testModel.Text;
            }

            if (!string.IsNullOrEmpty(testModel.Portrait))
            {
                var sprite = Resources.Load<Sprite>($"Portraits/{testModel.Portrait}");
                portraitImage.sprite = sprite;
            }
        }
        
        protected override void OnClose()
        {
            if (typingCoroutine != null)
            {
                StopCoroutine(typingCoroutine);
                typingCoroutine = null;
            }
        }
        
        private IEnumerator PlayTyping(List<TextSegment> segments)
		{
    		IsTyping = true;
    		dialogueText.text = "";

            currentCharCount = 0;
            var tagRegex = new Regex(@"<.*?>");

            foreach (var segment in segments)
            {
                string cleanText = tagRegex.Replace(segment.Content, "");
                currentCharCount += cleanText.Count(c => !char.IsWhiteSpace(c));
            }
            
    		for (int i = 0; i < segments.Count; i++)
    		{
        		var segment = segments[i];

        		if (segment.TypingSpeed <= 0f)
        		{
            		dialogueText.text += segment.Content;
            		dialogueText.ForceMeshUpdate();

            		continue;
        		}

        		float waitTime = Mathf.Max(segment.TypingSpeed, 0.01f);

        		foreach (char c in segment.Content)
        		{
            		dialogueText.text += c;
            		dialogueText.ForceMeshUpdate();
                    
            		TMP_TextInfo textInfo = dialogueText.textInfo;
            		int latestVisibleIndex = textInfo.characterCount - 1;

            		if (!string.IsNullOrEmpty(segment.Animation))
            		{
                        charAnimQueue.Enqueue((latestVisibleIndex, Time.time,segment.Animation));
            		}

            		if (!char.IsWhiteSpace(c))
                		yield return new WaitForSeconds(waitTime);
                    
                }
            }
            IsTyping = false;
        }

        
        private void Update()
        {
            if (!IsTyping && charAnimQueue.Count == 0)
            {
                if (isAutoMode && model is DialogueUIModel m && m.OnAutoNext != null)
                {
                    autoTimer += Time.deltaTime;
                    float autoDelay = Math.Max(
                        Mathf.Min(currentCharCount * baseDelayPerChar, maxAutoDelay), 1);
                    if (autoTimer >= autoDelay)
                    {
                        autoTimer = 0f;
                        m.OnAutoNext.Invoke();
                    }
                }
                return;
            }
            autoTimer = 0;
            dialogueText.ForceMeshUpdate();
            var textInfo = dialogueText.textInfo;
            
            while (charAnimQueue.Count > 0 && Time.time - charAnimQueue.Peek().startTime > 1f)
                charAnimQueue.Dequeue();

            foreach (var (index, _,anim) in charAnimQueue)
            {
                if (index >= textInfo.characterCount) continue;

                var charInfo = textInfo.characterInfo[index];
                if (!charInfo.isVisible) continue;

                int vi = charInfo.vertexIndex;
                int mi = charInfo.materialReferenceIndex;

                Vector3[] verts = textInfo.meshInfo[mi].vertices;
                Vector3 center = (verts[vi] + verts[vi + 2]) / 2;

                for (int j = 0; j < 4; j++) verts[vi + j] -= center;

                switch (anim)
                {
                    case "shake":
                        ApplyShake(ref verts, vi);
                        break;
                    case "punch":
                        //후에 제작 예정
                        break;
                    case "bounce":
                        //후에 제작 예정
                        break;
                }

                for (int j = 0; j < 4; j++) verts[vi + j] += center;
            }

            for (int i = 0; i < textInfo.meshInfo.Length; i++)
            {
                var meshInfo = textInfo.meshInfo[i];
                meshInfo.mesh.vertices = meshInfo.vertices;
                dialogueText.UpdateGeometry(meshInfo.mesh, i);
            }
        }
        private void ApplyShake(ref Vector3[] vertices, int vi)
        {
            float x = (Mathf.PerlinNoise(vi, Time.time * 8f) - 0.5f) * 2f;
            float y = (Mathf.PerlinNoise(vi + 100f, Time.time * 8f) - 0.5f) * 2f;
            float intensity = 3f;

            Vector3 offset = new Vector3(x, y, 0f) * intensity;
            for (int j = 0; j < 4; j++)
                vertices[vi + j] += offset;
        }
    }
}