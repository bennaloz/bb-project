using bb_project.Infrastructure.Models.Data;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace bb_project.Modules.WorkoutAssistantModule.Converters
{
    public class VisibleConditionMultiConverter : IMultiValueConverter
    {
        const int valuesExpectedSize = 2; //Item corrente e quello selezionato

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {

            if (values.Length < valuesExpectedSize)
            {
                return false;
            }

            if (!(values[0] is WorkoutPlan selectedWorkoutPlan))
            {
                return false;
            }
            if (!(values[1] is WorkoutPlan currentWorkoutPlan))
            {
                return false;
            }

            if(selectedWorkoutPlan == currentWorkoutPlan)
            {
                return true;
            }

            return false;
            
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
