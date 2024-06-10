namespace RulesEnginePOC.MyRulesEngine
{
    public static class Utils
    {
        public static bool AreListsEqual(IEnumerable<int>? inputList, string valList)
        {
            if (inputList == null || String.IsNullOrEmpty(valList))
                return false;

            var list = valList.Split(',').Select(int.Parse).ToList();
            return list.Count == inputList.Count() && !list.Except(inputList).Any();
        }

        public static bool CheckContains(string check, string valList)
        {
            if (String.IsNullOrEmpty(check) || String.IsNullOrEmpty(valList))
                return false;

            var list = valList.Split(',').Select(l => l.Trim()).ToList();
            return list.Contains(check);
        }

        public static bool CheckContains(int? check, string valList)
        {
            if (check == null || String.IsNullOrEmpty(valList))
                return false;

            var list = valList.Split(',').Select(l => l.Trim()).Select(int.Parse).ToList();
            return list.Contains((int)check);
        }
    }
}
