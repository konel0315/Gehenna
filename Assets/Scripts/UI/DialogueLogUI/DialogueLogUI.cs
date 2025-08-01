using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Gehenna
{
    public class DialogueLogUI : BaseUI<DialogueLogUIModel>
    {
        public override UILayerType LayerType => UILayerType.Overlay;
        
        [SerializeField] private ScrollRect scrollRect;
        [SerializeField] private RectTransform content;
        [SerializeField] private GameObject logItemPrefab;

        protected override void OnOpen()
        {
            foreach (Transform child in content)
            {
                Destroy(child.gameObject);
            }
            
            var logEntries = model.OnRequestLog?.Invoke();
            if (logEntries == null || logEntries.Count == 0)
                return;

            foreach (var entry in logEntries)
            {
                var itemGO = Instantiate(logItemPrefab, content);
                
                var portraitImage = itemGO.transform.Find("Portrait")?.GetComponent<Image>();
                var speakerText = itemGO.transform.Find("Speaker")?.GetComponent<TextMeshProUGUI>();
                var contentText = itemGO.transform.Find("Content")?.GetComponent<TextMeshProUGUI>();

                if (portraitImage != null && !string.IsNullOrEmpty(entry.Portrait))
                {
                    var sprite = Resources.Load<Sprite>($"Portraits/{entry.Portrait}");
                    if (sprite != null)
                        portraitImage.sprite = sprite;
                }

                if (speakerText != null)
                {
                    speakerText.text = entry.Speaker;
                }

                if (contentText != null)
                {
                    contentText.text = entry.Text;
                }
            }
            
            Canvas.ForceUpdateCanvases();
            scrollRect.verticalNormalizedPosition = 0f;
        }
    }
}