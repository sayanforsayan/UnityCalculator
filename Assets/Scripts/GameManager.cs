using System.Collections.Generic;
using UnityEngine;
namespace Sayan.UnityCalculator
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public float Calculate(string expression)
        {
            List<string> storeDigit = SeparateNumbersAndOperators(expression);
            return CalculateWithRule(storeDigit);
        }

        private List<string> SeparateNumbersAndOperators(string expression)
        {
            List<string> storeDigit = new List<string>();
            string temp = "";

            foreach (char c in expression)
            {
                if (char.IsDigit(c) || c == '.')
                {
                    temp += c;
                }
                else
                {
                    if (temp != "")
                    {
                        storeDigit.Add(temp);
                        temp = "";
                    }
                    storeDigit.Add(c.ToString());
                }
            }

            if (temp != "")
            {
                storeDigit.Add(temp);
            }

            return storeDigit;
        }

        private float CalculateWithRule(List<string> parts)
        {
            //Division and Multiplication
            for (int i = 0; i < parts.Count; i++)
            {
                string symbol = parts[i];
                Debug.Log(symbol);
                if (symbol == "/" || symbol == "*")
                {
                    float left = float.Parse(parts[i - 1]);
                    float right = float.Parse(parts[i + 1]);

                    float result = 0;

                    if (symbol == "/")
                    {
                        result = left / right;

                    }
                    else if (symbol == "*")
                    {
                        result = left * right;
                    }

                    parts[i - 1] = result.ToString();

                    parts.RemoveAt(i);
                    parts.RemoveAt(i);

                    i--;
                }
            }

            // Addition and Subtraction
            float finalResult = float.Parse(parts[0]);

            for (int i = 1; i < parts.Count; i += 2)
            {
                string symbol = parts[i];
                float nextNumber = float.Parse(parts[i + 1]);

                if (symbol == "+")
                {
                    finalResult += nextNumber;
                }
                else if (symbol == "-")
                {
                    finalResult -= nextNumber;
                }
            }

            return finalResult;
        }
    }
}