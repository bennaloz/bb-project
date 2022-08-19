using Prism.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace bb_project.Infrastructure.Models.Events
{
    public class EditEvent : PubSubEvent<Enums.ViewState>
    {
    }
}
