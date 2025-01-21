using UnityEngine;

namespace CardGameVR.Languages
{
    public class TextLanguage : MonoBehaviour
    {
        public string key;
        public string[] arguments;

        public string text => LanguageManager.Get(key, arguments);

        void Start()
        {
            LanguageManager.OnLanguageChanged += UpdateText;
            UpdateText();
        }

        void OnDestroy() => LanguageManager.OnLanguageChanged -= UpdateText;

        void OnValidate() => UpdateText();

        public void UpdateText()
        {
            if (GetComponent<TMPro.TextMeshProUGUI>() is TMPro.TextMeshProUGUI textMeshProUGUI)
                textMeshProUGUI.text = text;
            else if (GetComponent<UnityEngine.UI.Text>() is UnityEngine.UI.Text textUI)
                textUI.text = text;
        }

        public void UpdateText(string[] arguments)
        {
            this.arguments = arguments;
            UpdateText();
        }

        public void UpdateText(string key, string[] arguments = null)
        {
            this.key = key;
            if (arguments != null)
                this.arguments = arguments;
            UpdateText();
        }
    }
}