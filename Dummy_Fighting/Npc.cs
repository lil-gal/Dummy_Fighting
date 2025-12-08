namespace Dummy_Fighting {
    internal class Npc : Dummy { //YAYYYY AI!!!!
        public Npc(string name = "npc") : base(name) {
            addAttack(getAttackByName("Kick")); //add kick
        }

        public override void goFight(List<Dummy> allies, List<Dummy> enemies) {
            Thread.Sleep(1000);
            Dummy target = enemies[0];
            Attack(target, availableAttacks[0]); 
        }

        
    }
}