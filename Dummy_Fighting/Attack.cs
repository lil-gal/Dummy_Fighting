namespace Dummy_Fighting {
    internal class Attack {

        string attackName; 

        int healthCost;
        int staminaCost;
        int manaCost;

        int damage;

        int chanceOfHit;

        int cooldown;
        int cooldownCounter = 0;

        Effect inflictEffect;


        public Attack(string attackName, int healthCost, int staminaCost, int manaCost, int damage, int chanceOfHit = 100, int cooldown = 0) {
            this.attackName = attackName; 
            this.healthCost = healthCost;
            this.staminaCost = staminaCost;
            this.manaCost = manaCost;
            this.damage = damage;
            this.inflictEffect = new Effect();
            this.chanceOfHit = chanceOfHit;
            this.cooldown = cooldown;
        }

        public Attack(string attackName, int healthCost, int staminaCost, int manaCost, int damage, Effect inflictEffect, int chanceOfHit = 100, int cooldown = 0) {
            this.attackName = attackName; 
            this.healthCost = healthCost;
            this.staminaCost = staminaCost;
            this.manaCost = manaCost;
            this.damage = damage;
            this.inflictEffect = inflictEffect;
            this.chanceOfHit = chanceOfHit;
            this.cooldown = cooldown;
        }

        public Attack(Attack att) {
            this.attackName = att.attackName; 
            this.healthCost = att.healthCost;
            this.staminaCost = att.staminaCost;
            this.manaCost = att.manaCost;
            this.damage = att.damage;
            this.inflictEffect = att.inflictEffect;
            this.chanceOfHit = att.chanceOfHit;
            this.cooldown = att.cooldown;
        }

        public override string ToString() {
            string healthCostText = healthCost == 0? "": $"{healthCost} health" ;
            string staminaCostText = staminaCost == 0? "": $"{staminaCost} stamina" ;
            string manaCostText = manaCost == 0? "": $"{manaCost} mana";

            string[] costs = {};
            string chance = chanceOfHit ==100? "": $" ({chanceOfHit}%)";

            string temp = $"{attackName} (Deals: {damage}dmg; Costs: {healthCostText}{staminaCostText}{manaCostText}){chance}";
            if(cooldownCounter != 0) {
                return $"\x1B[38;5;240m{temp} ({cooldownCounter})";
            }
            return temp;
        }

        public int attack() {
            cooldownCounter = cooldown;
            return damage;
            
        }

        public bool canAttack() {
            if (chanceOfHit < 100){
                Random random = new Random();
                if (chanceOfHit < random.Next(1, 100 + 1)) {
                    return false;
                }
            }
            if(cooldownCounter != 0) {
                return false;
            }
            return true;
        }

        public void checkCooldown() {
            if(cooldownCounter != 0) {
                cooldownCounter--;
            }
            
        }

        public void returnCosts(ref int health, ref int stamina, ref int mana) {
            health -= this.healthCost;
            stamina -= this.staminaCost;
            mana -= this.manaCost;
        }

        public Effect? getAttackEffect() {
            if(inflictEffect.getEffectName() == "None") return null;
            return inflictEffect;
        }

        public string getAttackName() {
            return attackName;
        }





    }
}