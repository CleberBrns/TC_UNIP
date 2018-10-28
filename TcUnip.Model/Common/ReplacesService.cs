using System;

namespace TcUnip.Model.Common
{
    public class ReplacesService
    {
        public string ToMilliseconds(DateTime data)
        {
            DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            var dataMS = (long)(data - UnixEpoch).TotalMilliseconds;
            return dataMS.ToString();
        }

        public DateTime FromMilliseconds(string dateService)
        {
            DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var dataRetornoMS = UnixEpoch.AddMilliseconds(Convert.ToInt64(dateService));

            return dataRetornoMS;
        }

        public DateTime CombinaDataHora(DateTime data, string hora)
        {
            return DateTime.Parse(data.ToString("dd/MM/yyyy") + " " + hora);
        }

        /// <summary>
        /// Ajusta os separadores das datas para poder passar pela API
        /// </summary>
        /// <param name="valueFromWeb"></param>
        /// <param name="fromWeb"></param>
        /// <returns></returns>
        public string ReplaceDateWebToApi(string valueFromWeb, bool fromWeb)
        {
            if (!string.IsNullOrEmpty(valueFromWeb))
            {
                if (fromWeb)
                    return valueFromWeb.Replace("/", "-").Trim();
                else
                    return valueFromWeb.Replace("-", "/").Trim();
            }
            else
                return valueFromWeb;            
        }

        /// <summary>
        /// Ajusta os '.'s do cpf e email para poder passar pela API
        /// </summary>
        /// <param name="valueFromWeb"></param>
        /// <param name="fromWeb"></param>
        /// <returns></returns>
        public string ReplaceCpfEmailWebToApi(string valueFromWeb, bool fromWeb)
        {
            if (!string.IsNullOrEmpty(valueFromWeb))
            {
                if (fromWeb)
                    return valueFromWeb.Replace(".", "_").Trim();
                else
                    return valueFromWeb.Replace("_", ".").Trim();
            }
            else
                return valueFromWeb;
        }
    }
}
