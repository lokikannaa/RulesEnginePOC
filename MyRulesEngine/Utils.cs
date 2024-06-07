using System.Text.Json;

namespace RulesEnginePOC.MyRulesEngine
{
    public static class Utils
    {
        public static bool AreListsEqual(IEnumerable<string> inputList, string valList)
        {
            if (inputList == null || String.IsNullOrEmpty(valList))
                return false;

            var list = valList.Split(',').ToList();
            return list.Count == inputList.Count() && !list.Except(inputList).Any();
        }

        public static bool CheckContains(string check, string valList)
        {
            if (String.IsNullOrEmpty(check) || String.IsNullOrEmpty(valList))
                return false;

            var list = valList.Split(',').ToList();
            return list.Contains(check);
        }
    }
}
