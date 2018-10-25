using AppMonitorIndici.Bean;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppMonitorIndici.utility
{
    public class CallAPI
    {
        public async Task<List<RecordBean>> doInBackground()
        {
            DateTime c = DateTime.Now;
            var year = c.Year;
            var month = c.Month;
            var day = c.Day - 1;
            var hour = 23;
            var min = 59;

            string dataRichiesta = year.ToString() + "-" + month.ToString() + "-" + day.ToString();
            string oraRichiesta = hour.ToString() + ":" + min.ToString();
            string link = "http://172.20.145.37:3004/whaccettatips?dataingresso=" + dataRichiesta + "&oraingresso=" + oraRichiesta; //http://test.gesan.it/ps-api/whlisteattesamedicherie";
            RestFullConnection<RecordBean> rest = new RestFullConnection<RecordBean>();
            var result = new List<RecordBean>();
            try
            {
                result = await rest.GetJson(link);
                return result;
            }
            catch (Exception e)
            {
                return result;
            }
        }
    }
}
