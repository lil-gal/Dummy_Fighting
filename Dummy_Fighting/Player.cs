namespace Dummy_Fighting {
    internal class Player : Dummy {
        public Player(string name = "Player") : base(name) {
            addAttack(getAttackByName("Kick")); //add kick
            addAttack(getAttackByName("Fireball")); //add fireball
            addAttack(getAttackByName("Pounce"));
        }


        //Fighting mode:
        Choice rootChoice;
        
        Choice insideChoice;
        Choice curChoice;
        int curChoiceNum = 0;



        public override void goFight(List<Dummy> allies, List<Dummy> enemies) {
            chooseAction(enemies[0]);
        }
        
        bool continueChoosing;
        public void chooseAction(Dummy target) {
            continueChoosing = true;



            List<Choice> attacks = new List<Choice>();
            foreach(Attack att in availableAttacks) {
                attacks.Add(
                    new Choice(
                        () => {
                            Attack(target, att);
                        }, 
                        att.ToString()
                    )
                );
            }

            Choice attacksFolder = new Choice(attacks, "Attacks"); 





            rootChoice = new Choice(
                new List<Choice> {
                    attacksFolder,

                    new Choice( 
                        () => {Console.SetCursorPosition(0,0);Console.WriteLine("Hello world2");} , "Print Hello World (Testing)"
                    )

                },
                "Root"
            );
            insideChoice = rootChoice;
            curChoice = rootChoice.getChild(0);
            curChoiceNum = 0;

            while(continueChoosing){
                if (Console.KeyAvailable) {
                    CheckChoiceInput(Console.ReadKey(true).Key);
                }

                printMenu();
            }
            Console.Clear();

            

            
        }

        void CheckChoiceInput(ConsoleKey key) {
            if (key == ConsoleKey.Spacebar || key == ConsoleKey.Enter) {
                clearMenu();
                if(curChoice.isAction()){continueChoosing = false ; curChoice.choiceAction(); return;}
                insideChoice = curChoice;
                curChoiceNum = 0;
                curChoice = insideChoice.getChild(curChoiceNum);
                
            }else if (key == ConsoleKey.Escape) {
                clearMenu();
                if(insideChoice == rootChoice) {return;}
                insideChoice = insideChoice.getParent();
                curChoice = insideChoice;
                curChoiceNum = 0;

            }
            else if(key == ConsoleKey.UpArrow) {
                if(curChoiceNum - 1 >= 0) {
                    curChoiceNum--;
                    curChoice = insideChoice.getChild(curChoiceNum);
                }
            }else if(key == ConsoleKey.DownArrow) {
                if(curChoiceNum+1 < insideChoice.getChildrenNumber()) {
                    curChoiceNum++;
                    curChoice = insideChoice.getChild(curChoiceNum);
                }
            }

        }




        //Attack / Defend / Special (heal, buff, debuff) / Run

        public void printMenu() {
            Program.reCheckWindowProps();
            int ww = Program.ww;
            int wh = Program.wh;


            int gapFromCenter = 4;
            int curHeight = wh- 6;

            Console.SetCursorPosition(ww/2, curHeight); //temp values
            Console.Write("Menu");

            curHeight++;

            string[] choices = insideChoice.getChoices();

            //Console.Write(availableAttacks[0]);
            for (int i = 0; i < choices.Length; i++){
                string token = (i == curChoiceNum)? $"> ":"  " ;
                string choice = token + choices[i];

                Console.SetCursorPosition(
                    ww / 2 - (gapFromCenter + Program.StripAnsi(choice).Length), 
                    curHeight
                );

                

                

                Console.Write(choice);
                curHeight++;
            }


        }

        public void clearMenu() {
            Program.reCheckWindowProps();
            int ww = Program.ww;
            int wh = Program.wh;
            int curHeight = wh- 6;

            curHeight++;

            //Console.Write(availableAttacks[0]);
            for (int i = 0; i < 4; i++){
                for(int j = 0; j < ww; j++) {
                    Console.SetCursorPosition(j, curHeight);
                    Console.Write(" ");
                }
                curHeight++;
            }


        }

        


    }
}