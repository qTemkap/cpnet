using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Cryptography_LB3.Services
{
    public class DSACngService
    {
        private DSACng dsaService;

        public DSACngService(int size)
        {
            //Initializes a new instance of the DSACng class with a randomly generated key of the specified size.
            this.dsaService = new DSACng(size);
        }
        public DSACngService()
        {
            //Initializes a new instance of the DSACng class with a random 2,048-bit key pair.
            this.dsaService = new DSACng();
        }

        public DSACngService(CngKey cngKey)
        {
            //Initializes a new instance of the DSACng class with a random 2,048-bit key pair.
            this.dsaService = new DSACng(cngKey);
        }

        public byte[] CreateSignature(byte[] data)
        {
           return dsaService.SignData(data, HashAlgorithmName.SHA256);
        }
    }
}
