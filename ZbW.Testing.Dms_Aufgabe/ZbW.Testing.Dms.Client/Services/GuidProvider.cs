using System;

namespace ZbW.Testing.Dms.Client.Services
{
    public class GuidProvider
    {
        public string NextGuid => Guid.NewGuid().ToString();
    }
}