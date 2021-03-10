using System;
using System.Collections.Generic;
using System.Text;

namespace testgame {
    class ZoneEvent : Event {
        
        public ZoneEvent() {
            
        }


        public void ChangeZone(Zone newZone) {
            Game1.world.CurrentZone = newZone;
            Game1.world.PlayableCharacter.Vector = newZone.StartVector;

        }

    }
}
