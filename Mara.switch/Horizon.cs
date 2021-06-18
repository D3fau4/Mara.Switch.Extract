﻿using LibHac;
using LibHac.Common.Keys;
using LibHac.Fs;

namespace Mara.Switch
{
    public class HOS
    {
        public KeySet keys;
        public HorizonClient horizon;
        public bool CheckSignature;

        public HOS(string Keys, bool Sig = false)
        {
            this.keys = ExternalKeyReader.ReadKeyFile(Keys);
            Horizon tmp = HorizonFactory.CreateWithDefaultFsConfig(new HorizonConfiguration(), new InMemoryFileSystem(), this.keys);
            this.horizon = tmp.CreatePrivilegedHorizonClient();
            this.CheckSignature = Sig;
        }
    }
}
