namespace GTalk.Client
{
    /// <summary>
    ///   Rules over GTalk.
    /// </summary>
    internal static class GTalkRules
    {
        public const string Postfix = "@gmail.com";

        /// <summary>
        /// Checks is name ends with postfix.
        /// </summary>
        public static bool ContainsPrefix(string name)
        {
            return name.EndsWith(Postfix);
        }

        public static string FixPostfix(string name)
        {
            return ContainsPrefix(name) ? name : name + Postfix;
        }
    }
}