using System;
using System.Collections.Generic;
using System.Text;

namespace Proje_Dal.Configuration
{
    public class AppSettings
    {
        public string SecretKey { get; set; } // daha sonrasında appSettings.jsona yazılan secretkey, startUp da buraya atanacak ve her çağrıldığında gelecek .(aynı aplicationDBcontext  - appSetting.jsondaki conntectionString gibi )

    }
}
