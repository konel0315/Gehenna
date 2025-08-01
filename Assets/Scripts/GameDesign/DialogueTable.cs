namespace Gehenna
{
    [System.Serializable]
    public class DialogueTable : IGameDesignData
    {
        public string DialogueKey;
        public int ID;
        public string Speaker;
        public string Text;
        public string Portrait;
        public string Type;
        public int DefaultID;
        public string Condition1;
        public int NextID1;
        public string Condition2;
        public int NextID2;
        public string Condition3;
        public int NextID3;
        public string DefaultSpeed;
    }
}
