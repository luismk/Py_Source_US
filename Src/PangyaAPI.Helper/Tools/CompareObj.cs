namespace PangyaAPI.Helper.Tools
{
    public static class Compare
    {
        /// <summary>
        /// compares objects then returns a value
        /// </summary>
        /// <typeparam name="T">type of object to be returned</typeparam>
        /// <param name="expression">value to be compared</param>
        /// <param name="trueValue">returns the value trueValue</param>
        /// <param name="falseValue">returns the value falseValue</param>
        /// <returns></returns>
        public static T IfCompare<T>(bool expression, T trueValue, T falseValue)
        {
            if (expression)
            {
                return trueValue;
            }
            else
            {
                return falseValue;
            }
        }
    }
}
