using bb_project.app.Contracts.Models.Enums;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace bb_project.app.Contracts.Models.Events
{
    public class EditEvent : PubSubEvent<ViewState>
    {
    }
}
