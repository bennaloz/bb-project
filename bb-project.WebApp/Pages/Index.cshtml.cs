using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using bb_project.Infrastructure.Models.Data;

namespace bb_project.WebApp.Pages;

public class IndexModel : PageModel
{
    public Workout SampleWorkout { get; private set; } = new Workout(1)
    {
        Name = "New project kickoff",
        Order = 1
    };

    public void OnGet()
    {
    }
}
