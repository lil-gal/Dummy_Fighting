namespace Dummy_Fighting {
    internal class Effect {
        
        string effectName;

        int turnsLeft; // if 1, continue, if 0, stop.
        int turns;

        Action<Dummy> effect = (Dummy target) =>{};
        Action<Dummy> beforeDeath = (Dummy target) =>{};

        public Effect() {
            this.effectName = "None";
        }

        public Effect(Effect eff) {
            this.effectName = eff.effectName;
            this.turns = eff.turns;
            this.turnsLeft = eff.turns;

            this.effect = eff.effect;
            this.beforeDeath = eff.beforeDeath;
        }

        public Effect(string effectName, int turns, Action<Dummy> effect) {
            this.effectName = effectName;
            this.turns = turns;
            this.turnsLeft = turns;
            
            this.effect = effect;
        }

        public Effect(string effectName, int turns, Action<Dummy> effect, Action<Dummy> beforeDeath) {
            this.effectName = effectName;
            this.turns = turns;
            this.turnsLeft = turns;
            
            this.effect = effect;
            this.beforeDeath = beforeDeath;
        }

        public string getEffectName() {
            return effectName;
        }

        public bool shouldDie() {
            return turnsLeft == 0;
        }

        public int getTurnsLeft() {
            return turnsLeft;
        }

        public void takeEffect(Dummy target) {

            if(shouldDie()) {
                beforeDeath(target);
                return;
            }

            effect(target);


            turnsLeft--;

            
        }
    }
}