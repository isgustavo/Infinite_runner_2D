using System.Collections.Generic;
using UnityEngine;

namespace ODT.Util
{
    public class CustomInput : MonoBehaviour
    {
        static Dictionary<string, float> inputs = new Dictionary<string, float>();

        public static float GetInput(string input)
        {
            if (!inputs.ContainsKey(input))
            {
                inputs.Add(input, 0);
            }

            return inputs[input];
        }

        public static void SetInput(string input, float inputValue = 0)
        {
            if (!inputs.ContainsKey(input))
            {
                inputs.Add(input, 0);
            }

            inputs[input] = inputValue;
        }
    }
}

