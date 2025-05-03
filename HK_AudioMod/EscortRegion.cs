using System;
using System.Collections.Generic;
using System.Linq;

using System.Text;
using System.Threading.Tasks;
using UnityEngine;


namespace HK_AudioMod
{
    public class EscortRegion
    {
        public AudioRegion ar;

        public EscortRegion(AudioRegion ar, Vector2 pos, Vector2 size)
        {
            this.ar = ar;
            ar.gameObject.transform.localPosition = pos;

            SpriteRenderer spriteRenderer = ar.gameObject.AddComponent<SpriteRenderer>();

            spriteRenderer.sortingLayerName = "HUD";
            spriteRenderer.sortingOrder = 1000;
            spriteRenderer.transform.localScale = new Vector3(size.x, size.y, 1);
        }
    }
}
