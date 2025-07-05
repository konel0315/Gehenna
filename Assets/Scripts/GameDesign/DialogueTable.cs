namespace Gehenna
{
    [System.Serializable]
    public class DialogueTable : IGameDesignData
    {
        public string EventName;
        public int ID;
        public string Speaker;
        public string Text;
        public string Portrait;
        public string CommandType;
        public string CommandData;
        public int DefaultNextID;
        public string Condition1;
        public int NextID1;
        public string Condition2	;
        public int NextID2;
    }
}
