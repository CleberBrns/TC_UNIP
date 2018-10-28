using System;

namespace TcUnip.Model.Common
{
    public class DateTimeService
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
    }
}
