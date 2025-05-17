using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HK_AudioMod
{
    public class EscortHandler
    {
        public string currentRoomName;

        public List<EscortRegion> regions;

        public EscortHandler() { }

        public void addEscortRegion(EscortRegion region)
        {
            regions.Add(region);
            Modding.Logger.Log("Added region: " + region.roomName);
        }

        public void setCurrentRoomName(String currentRoomName)
        {

            Modding.Logger.Log("Setting current room name to: " + currentRoomName);
            this.currentRoomName = currentRoomName;

            foreach (EscortRegion region in regions)
            {
                if (region.roomName == currentRoomName)
                {
                    region.ar.gameObject.SetActive(true);
                }
                else
                {
                    region.ar.gameObject.SetActive(false);
                }
            }
        }

        public void clearList()
        {
            regions.Clear();
        }
    }
}
