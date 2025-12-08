namespace Dummy_Fighting {
    internal class Dummy {

        protected string name;

        
        int health;
        int _MaxHealth;
        
        int stamina;
        int _MaxStamina;

        int mana;
        int _MaxMana;
        
        int exp;
        int level;

        bool canTakeTurn = true;

        
        protected List<Effect> currentEffects = new List<Effect>();
        protected List<Attack> availableAttacks = new List<Attack>();

        

        protected static Effect[] effects = {
                        //name, turns
            new Effect("Burned", 3,
            (Dummy target) => { //effect
                target.takeDamage(5);
            }),


            new Effect("Stunned", 2,
            (Dummy target) => {
                target.takeFreedomAway();
            }
            ),


        };

        protected static Attack[] attacks = {  //hpC,stC,maC,dmg, eff, chance, cooldown
            new Attack("Kick", 0, 20, 0, 15), 
            new Attack("Pounce", 0, 20, 0, 5, getEffectByName("Stunned"), cooldown: 5),
            new Attack("Blood for the Blood God", 40, 0, 0, 40),
            new Attack("Fireball", 0, 0, 20, 15, getEffectByName("Burned"), cooldown: 3),

        };

        public Dummy(string name = "Unnamed") {
            this.name = name;
            _MaxHealth = 100;
            _MaxStamina = 100;
            _MaxMana = 100;
            health = _MaxHealth;
            stamina = _MaxStamina;
            mana = _MaxMana;
            
            exp = 0;
            level = 1;
            
        }

        public Dummy(string name, int health, int stamina, int mana, int _MaxHealth, int _MaxStamina, int _MaxMana, int exp, int level) {
            this.name = name;
            this._MaxHealth = _MaxHealth;
            this._MaxStamina = _MaxStamina;
            this._MaxMana = _MaxMana;
            this.health = health;
            this.stamina = stamina;
            this.mana = mana; 
            this.exp = exp;
            this.level = level;
            
        }

        public override string ToString() {
            string effs = "";
            for(int i = 0; i < currentEffects.Count(); i++) {
                effs += currentEffects[i].getEffectName() + currentEffects[i].getTurnsLeft() + (i < currentEffects.Count()-1? ", ": "");
            }
            return  $"Name: {name}\n"+
                    $"Health: {health}/{_MaxHealth}\n" +
                    $"Stamina: {stamina}/{_MaxStamina}\n" +
                    $"Mana: {mana}/{_MaxMana}\n" +
                    $"Status: {effs}"; 
        }

        public string[] ToStringArr() {
            string effs = "";
            for(int i = 0; i < currentEffects.Count(); i++) {
                effs += currentEffects[i].getEffectName() + $"({currentEffects[i].getTurnsLeft()})" + (i < currentEffects.Count()-1? ", ": "");
            }

            string[] arr = {
                $"Name: {name}       ",
                $"Health: {health}/{_MaxHealth}      ",
                $"Stamina: {stamina}/{_MaxStamina}      ",
                $"Mana: {mana}/{_MaxMana}       ",
                $"Status: {effs}                              "
                
            };

            return arr;
        }

        static protected Effect getEffectByName(string name) {
            for(int i = 0; i < effects.Length; i++) {
               if(name == effects[i].getEffectName()) return new Effect(effects[i]);
            }
            return new Effect();
        }

        static protected bool existsEffect(string name) {
            for(int i = 0; i < effects.Length; i++) {
               if(name == effects[i].getEffectName()) return true;
            }
            return false;
        }

        

        public bool isEffectAppliedByName(string name) {
            for(int i = 0; i < currentEffects.Count(); i++) {
               if(name == currentEffects[i].getEffectName()) return true;
            }
            return false;
            
        }

        public bool isEffectAppliedByName(string name, out int index) {
            for(int i = 0; i < currentEffects.Count(); i++) {
               if(name == currentEffects[i].getEffectName()){
                index = i;
                return true;
                }
            }
            index = 0; //lets hope this isnt used, cuz its supposed to be NULL
            return false;
        }

        public void takeFreedomAway(bool f = false) {
            canTakeTurn = f;
        }



        public void takeTurn(List<Dummy> allies, List<Dummy> enemies) {
            if(!canTakeTurn) return;

            goFight(allies, enemies);
            foreach (Attack att in availableAttacks) {
                att.checkCooldown();
            }
        }

        public virtual void  goFight(List<Dummy> allies, List<Dummy> enemies) {
            
        }

        public void applyEffects() {
            takeFreedomAway(true);

            List<string> delThese = new List<string>();
            foreach(Effect eff in currentEffects) {
                eff.takeEffect(this);
                if (eff.getTurnsLeft() == 0) {
                    delThese.Add(eff.getEffectName());
                }
            }

            foreach(string name in delThese) {
                if (isEffectAppliedByName(name, out int index)) {
                    currentEffects.RemoveAt(index);
                }
            }
        }

        



        public void takeDamage(int dmg) {
            health -= dmg;
        }

        private void addEffect(Effect effect) {
            if(effect != null){
                
                
                if (isEffectAppliedByName(effect.getEffectName(), out int index)) {
                    currentEffects.RemoveAt(index);
                }

                
                currentEffects.Add(new Effect(effect));
            }

        }

        protected void addAttack(Attack att) {

            if(hasAttackByName(att.getAttackName())) return;
            availableAttacks.Add(new Attack(att));
        }
        protected Attack getAttackByName(string name) {
            for(int i = 0; i < attacks.Length; i++) {
                if(name == attacks[i].getAttackName()) {
                    return attacks[i];
                }
            }
            return attacks[0]; //uhhh
        }
        
        private bool hasAttackByName(string name) {
            for(int i = 0; i < availableAttacks.Count(); i++) {
                if(name == availableAttacks[i].getAttackName()) {
                    return true;
                }
            }
            return false;
        }



        public void getAttacked(Attack attack) {
            takeDamage(attack.attack());
            addEffect(attack.getAttackEffect());
            
        }

        public void Attack(Dummy enemy, Attack attack) {
            int h = health, s = stamina, m = mana;
            attack.returnCosts(ref h, ref s, ref m);
            if(0 <= s && 0 <= m){ //if you can afford the cost  //  0 <= h &&  -  so that health costing spells are dangerous 
                if (attack.canAttack()) {
                    enemy.getAttacked(attack);
                }
                
                attack.returnCosts(ref health, ref stamina, ref mana);
            }else { 
                Rest();
                }
        }


        public void Rest() {
            int by = 10;
            int healthBy = 1;

            health += healthBy;
            stamina += by;
            mana += by;
            
        }




        
    }
}