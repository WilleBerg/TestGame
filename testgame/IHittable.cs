using System;
using System.Collections.Generic;
using System.Text;

namespace testgame {
    interface IHittable {

        public void SetHitboxX(int value);
        public void SetHitBoxY(int value);
        public int GetHitBoxX();
        public int GetHitBoxY();
    }
}
