﻿using System.Collections.Generic;
using Totem.Timeline.Area;

namespace Totem.Timeline.StreamsDb
{
    public class ResumeProjectionState
    {
        public long Checkpoint { get; set; }
        public Dictionary<long, ResumeProjectionSchedule> Schedule { get; set; }
        public Dictionary<AreaTypeName, ResumeProjectInstance> SingleInstances { get; set; }
        public Dictionary<AreaTypeName, Dictionary<Id, ResumeProjectInstance>> MultiInstances { get; set; }
    }
}