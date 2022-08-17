using Prism.Mvvm;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace bb_project.Client.Modules.WorkoutEditorModule.Interfaces
{
    public class IViewModelCollection : BindableBase
    {



        public IEnumerable Items
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }
    }
}
