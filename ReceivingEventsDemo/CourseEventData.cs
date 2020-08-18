// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Text;

namespace ReceivingEventsDemo
{
    public partial class CourseEventData
    {
        public string Department { get; set; }

        public int CourseNumber { get; set; }

        public DateTimeOffset StartTime { get; set; }
    }
}