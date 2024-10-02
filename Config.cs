using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invisix
{
    class Config
    {
        public string serverUrl;
        public string encryptionKey;
        public string encryptionIv;

        public Config()
        {
            serverUrl = "http://127.0.0.1:5000/"; // load from encrypted file
            encryptionKey = "09123456781jash29iop98123esjneub"; // 32 byte
            encryptionIv = "16-byte-IV-here!"; // 16 byte
        }

    }
}
