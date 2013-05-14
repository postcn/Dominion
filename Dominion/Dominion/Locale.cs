using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominion
{
    [Serializable]
    public class Locale
    {
        String langCode;
        String countryCode;

        public Locale(String langCode, String countryCode)
        {
            this.langCode = langCode;
            this.countryCode = countryCode;
        }

        public String getExtension()
        {
            return "_" + this.langCode + "_" + this.countryCode;
        }
    }
}
