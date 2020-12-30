using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cryptography_LB3
{
    public enum Actions
    {
        Uknown = 0,
        EncryptTripleDES = 1,
        RSACngEncrypt = 2,
        DSA = 3,
        SHA384Managed = 4,
        DecryptTripleDES = 5,
        RSACngDecrypt = 6,
        Exit = 7
    }
}
