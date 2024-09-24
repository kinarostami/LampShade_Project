using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace ServiceHost.Areas.Administration.Pages
{
    public class IndexModel : PageModel
    {
        public Chart DougnutDataSet { get; set; }
        public List<Chart> BarLineDataSet { get; set; }
        public void OnGet()
        {
            BarLineDataSet = new List<Chart>
            {
                new Chart
                {
                    Label = "Apple",
                    Data = [100, 150, 20, 400, 450, 700],
                    BackgroundColor = ["#ffcdb2"],
                    BorderColor = "#b5838b"
                },
                new Chart
                {
                    Label = "Samsung",
                    Data = [180, 200, 300,90,220,600],
                    BackgroundColor = ["#ffc8dd"],
                    BorderColor = "#ffafcc"
                },
                new Chart
                {
                    Label = "Total",
                    Data = [280, 350, 320,490,670,1300],
                    BackgroundColor = ["#0077b6"],
                    BorderColor = "#023e8a"
                }
            };

            DougnutDataSet = new Chart
            {
                Label = "Apple",
                Data = [100,200,150,80,400],
                BackgroundColor = ["#b5838d","#ffd166","#ef233c","#7f4f24","#003049"],
                BorderColor = "#ffcdb2",

            };
        }
    }

    public class Chart
    {
        [JsonProperty(PropertyName = "label")]
        public string Label { get; set; }

        [JsonProperty(PropertyName = "data")]
        public List<int> Data { get; set; }

        [JsonProperty(PropertyName = "backgroundColor")]
        public string[] BackgroundColor { get; set; }

        [JsonProperty(PropertyName = "borderColor")]
        public string BorderColor { get; set; }
    }

}
