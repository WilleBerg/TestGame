using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace testgame {
    public class World {
        private List<Character> currCharacters;
        private PC playableCharacter;
        private Zone currentZone;
        private Game1.LoadStates currentLoadState;
        public List<Character> CurrCharacters { get { return currCharacters; } set { currCharacters = value; } }
        public PC PlayableCharacter { get { return playableCharacter;  } set { playableCharacter = value; } }
        public Zone CurrentZone { get { return currentZone; } set { currentZone = value; } }
        public Game1.LoadStates CurrentLoadState { get { return currentLoadState; } set { currentLoadState = value; } }


        public World(List<Character> currCharacters, PC playableCharacter, Zone currentZone) {
            this.currCharacters = currCharacters;
            this.playableCharacter = playableCharacter;
            this.currentZone = currentZone;
        }

        public World() {
            currCharacters = new List<Character>();
        }

    }
}
