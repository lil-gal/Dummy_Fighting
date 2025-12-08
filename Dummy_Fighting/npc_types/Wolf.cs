namespace Dummy_Fighting {
    class Wolf : Npc {
        public Wolf() : base("wolf") {
            addAttack(getAttackByName("Kick"));
        }
    }
}