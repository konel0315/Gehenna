using System;
using System.Collections.Generic;
using UnityEngine;

namespace Gehenna
{
    public class DialogueLogUIModel : BaseUIModel
    {
        public Func<List<DialogueLogEntry>> OnRequestLog { get; set; }
    }
}