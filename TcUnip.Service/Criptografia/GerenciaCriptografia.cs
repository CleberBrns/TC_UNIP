using System;

namespace TcUnip.Service.Criptografia
{
    public class GerenciaCriptografia
    {
        #region Criptografa

        public static string CriptografaInteiro(int value)
        {
            return ScopedReferenceMap.GetIndirectReferenceMap(value);
        }

        public static string CriptografaString(string value)
        {
            return ScopedReferenceMap.GetIndirectReferenceMap(value);
        }

        #endregion

        #region Descriptografa

        public static int DescriptografaInteiro(string value)
        {
            return Convert.ToInt32(ScopedReferenceMap.GetDirectReferenceMap(value.Trim()));
        }

        public static string DescriptografaString(string value)
        {
            return ScopedReferenceMap.GetDirectReferenceMap(value.Trim());
        }

        #endregion
    }
}
