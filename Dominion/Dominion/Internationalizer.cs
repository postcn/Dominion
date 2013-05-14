using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Resources;

namespace Dominion
{
    public class Internationalizer
    {
        static ResourceManager manager = new ResourceManager("Dominion.Resource_en_US", typeof(MainWindow).Assembly);
        static Locale locale = new Locale("en","US");

        static public void setLocale(Locale l)
        {
            Internationalizer.locale = l;
            Internationalizer.manager = new ResourceManager("Dominion.Resource" + l.getExtension(), typeof(MainWindow).Assembly);
        }

        static public String getMessage(String key)
        {
            return Internationalizer.manager.GetString(key).Replace("\\n","\n");
        }

        static public Locale getLocale()
        {
            return Internationalizer.locale;
        }
    }
}
