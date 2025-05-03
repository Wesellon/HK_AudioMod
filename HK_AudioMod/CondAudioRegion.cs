using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HK_AudioMod
{
    public class CondAudioRegion : AudioRegion
    {
        public bool condition;

        public CondAudioRegion(string name, ref bool condition) : base(name)
        {
            this.condition = condition;
        }

        public new void Play()
        {
            if (!audioSource.isPlaying && isEnabled && condition)
            {
                audioSource.Play();
            }
        }
    }
}
