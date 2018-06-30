using System.Collections.Generic;

namespace ODT.UI.Util
{
    public class UIVirtualInput
    {
        private static Dictionary<string, float> inputs = new Dictionary<string, float>();

        public static bool AddInput(string input, float initValue = -1)
        {
            if (!inputs.ContainsKey(input))
            {
                inputs.Add(input, initValue);
                return true;
            }
            return false;
        }

        public static float GetInput(string input)
        {
            return inputs[input];
        }

        public static void UpdateInput(string input, float value)
        {
            inputs[input] = value;
        }

        public static void RemoveInput(string input)
        {
            if (inputs.ContainsKey(input))
            {
                inputs.Remove(input);
            }
        }
    }
}

