using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HK_AudioMod
{
    public class EscortHandler
    {
        public List<EscortRegion> regions;

        public EscortHandler() { }

        public void addEscortRegion(EscortRegion region)
        {
            regions.Add(region);
        }
    }
}
