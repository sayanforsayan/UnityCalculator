using UnityEngine;
using TMPro;

namespace Sayan.UnityCalculator
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private TMP_Text expressionText;
        [SerializeField] private TMP_Text resultText;

        private string expression = "";

        void Start()
        {
            OnClearAll();
        }

        public void OnButtonClick(string value)
        {
            if (!string.IsNullOrEmpty(resultText.text.ToString()))
            {
                expression = "";
                resultText.text = "";
            }

            if (value == ".")
            {
                string[] parts = expression.Split('+', '-', '*', '/');
                string currentPart = parts[parts.Length - 1];
                if (currentPart.Contains(".")) return;
            }

            expression += value;
            expressionText.text = expression;
        }

        public void OnEqualClick()
        {
            if (string.IsNullOrEmpty(expression)) return;
            float result = GameManager.Instance.Calculate(expression);
            expressionText.text = "";
            resultText.text = result.ToString();
        }

        public void OnClearAll()
        {
            expression = "";
            expressionText.text = "";
            resultText.text = "";
        }
    }
}