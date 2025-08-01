using System.Collections.Generic;

namespace Gehenna
{
    public class DialogueLog
    {
        private readonly List<DialogueLogEntry> entries = new();

        public IReadOnlyList<DialogueLogEntry> Entries => entries;

        public void AddEntry(DialogueLogEntry entry)
        {
            entries.Add(entry);
        }

        public void Clear()
        {
            entries.Clear();
        }
    }
}