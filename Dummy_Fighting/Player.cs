namespace Dummy_Fighting {
    internal class Player : Dummy {

        int x;
        int y;
        public Player(string name = "Player") : base(name) {
            addAttack(getAttackByName("Kick")); //add kick
            addAttack(getAttackByName("Fireball")); //add fireball
            addAttack(getAttackByName("Pounce"));
            addAttack(getAttackByName("Blood for the Blood God"));
        }

        public Player(string name, int health, int stamina, int mana, int _MaxHealth, int _MaxStamina, int _MaxMana, int exp, int level, int x, int y, List<Attack> attacks)
            :base(name, health, stamina, mana, _MaxHealth, _MaxStamina, _MaxMana, exp, level) {

            this.x = x;
            this.y = y;

            foreach(Attack att in attacks) {
                addAttack(att);
            }
        }

        public void move(int dx, int dy) {
            x += dx;
            y += dy;
        }

        public void getPosition(out int x, out int y) {
            x = this.x;
            y = this.y;
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
                if(insideChoice == rootChoice) {curChoiceNum = 0; return;}
                insideChoice = insideChoice.getParent();
                curChoice = insideChoice;
                curChoiceNum = 0;

            }
            else if(key == ConsoleKey.LeftArrow) {
                if(curChoiceNum - 1 >= 0 && (curChoiceNum - 1) % 2 != 1) {
                    curChoiceNum--;
                    curChoice = insideChoice.getChild(curChoiceNum);
                }
            }else if(key == ConsoleKey.RightArrow) {
                if(curChoiceNum+1 < insideChoice.getChildrenNumber() && (curChoiceNum + 1) % 2 != 0) {
                    curChoiceNum++;
                    curChoice = insideChoice.getChild(curChoiceNum);
                }
            }else if(key == ConsoleKey.UpArrow) {
                if(curChoiceNum - 2 >= 0) {
                    curChoiceNum-= 2;
                    curChoice = insideChoice.getChild(curChoiceNum);
                }
            }else if(key == ConsoleKey.DownArrow) {
                if(curChoiceNum+2 < insideChoice.getChildrenNumber()) {
                    curChoiceNum+= 2;
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

            Console.SetCursorPosition(ww/2 - "Menu".Length/2, curHeight); 
            Console.Write("Menu");

            curHeight++;

            string[] choices = insideChoice.getChoices();

            //Console.Write(availableAttacks[0]);
            for (int i = 0; i < choices.Length; i++){
                string token = (i == curChoiceNum)? $"> ":"  " ;
                string choice = token + choices[i];

                int by = 0;

                if(i%2 == 0) {
                    Console.SetCursorPosition(
                        ww / 2 - (gapFromCenter + Program.StripAnsi(choice).Length), 
                        curHeight
                    );
                    
                }else {
                    Console.SetCursorPosition(
                        ww / 2 + gapFromCenter, 
                        curHeight
                    );
                    by = 1; 
                }
                

                Console.Write(choice);
                curHeight += by;
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