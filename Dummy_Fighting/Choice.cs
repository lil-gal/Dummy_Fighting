namespace Dummy_Fighting {
    class Choice {

        string name;
        bool isActionable = false;

        Choice parent;

        List<Choice> children = new List<Choice>();
        Action action = () => {};

        public Choice(List<Choice> children, string name) {
            this.children = children;
            this.name = name;
            parent = this;
            foreach(Choice child in children) {
                child.parent = this;
            }
        }
        public Choice(Action action, string name) {
            this.action = action;
            this.name = name;
            this.isActionable = true;
        }

        public override string ToString() {
            return name;
        }

        public Choice getParent() {
            return parent;
        }
        
        public void choiceAction() {
            action();
        }

        public Choice getChild(int index) {
            return children[index];
        }

        public int getChildrenNumber() {
            return children.Count();
        }

        public string[] getChoices() {
            List<string> choices = new List<string>();
            foreach (Choice child in children) {
                choices.Add(child.ToString());
            }
            return choices.ToArray();
        }

        public bool isAction() {
            return isActionable;
        }

    }
}