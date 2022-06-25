using bb_project.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace bb_project.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}