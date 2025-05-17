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
        public String roomName;

        public EscortRegion(Vector2 pos, Vector2 size, string roomName)
        {
            this.ar = new AudioRegion(roomName);
            ar.gameObject.transform.localPosition = pos;

            SpriteRenderer spriteRenderer = ar.gameObject.AddComponent<SpriteRenderer>();

            spriteRenderer.sortingLayerName = "HUD";
            spriteRenderer.sortingOrder = 1000;
            spriteRenderer.transform.localScale = new Vector3(size.x, size.y, 1);
            this.roomName = roomName;
        }

        public void Enable()
        {
            this.ar.isEnabled = true;
        }
        public void Disable()
        {
            this.ar.isEnabled = false;
        }
    }
}
