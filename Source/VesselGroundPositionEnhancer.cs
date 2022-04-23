using UnityEngine;
using static Vessel;

namespace RealSolarSystem
{
    [KSPAddon(KSPAddon.Startup.Flight, false)]
    public class VesselGroundPositionEnhancer : MonoBehaviour
    {
        public void Start()
        {
            GameEvents.onVesselGoOffRails.Add(OnVesselGoOffRails);
        }

        public void OnDestroy()
        {
            GameEvents.onVesselGoOffRails.Remove(OnVesselGoOffRails);
        }

        private void OnVesselGoOffRails(Vessel v)
        {
            if (v.Landed && !v.Splashed && v.situation != Situations.PRELAUNCH)
            {
                v.skipGroundPositioning = false;
                var sw = System.Diagnostics.Stopwatch.StartNew();
                v.CheckGroundCollision();
                Debug.Log($"[RSS-VGPE] CheckGroundCollision() in {sw.ElapsedMilliseconds}ms");
                sw.Stop();
            }
            else
            {
                Debug.Log("[RSS-VGPE] not landed or is prelaunch");
            }
        }
    }
}
