namespace Gehenna
{
    public class SequenceParam
    {
        public ISequence Sequence;
        public SequenceData Data;

        public void SetService(SequenceService owner)
        {
            Data.Service = owner;
        }
    }
}