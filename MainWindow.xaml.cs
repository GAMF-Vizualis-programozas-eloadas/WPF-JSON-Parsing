using System.IO;
using System.Linq;
using System.Text.Json;
using System.Windows;

namespace JSON_Parsing
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
			string jsonp = File.ReadAllText("..\\..\\..\\Articles.json");
			tbJSON.Text = jsonp;

			//--------------------------------------------------------
			// Extracting JSON data from the string using the "parse"
			// approach and executing JSON specific LINQ queries.
			//--------------------------------------------------------
			
			JsonDocument doc = JsonDocument.Parse(jsonp);
			var docarray = doc.RootElement.EnumerateArray();
			// LINQ query creating a list of objects of non-open source
			// articles. Each object contains the title and the authors
			// of the article.
			var res1 = from x in docarray
								 where !x.GetProperty("IsOpenSource").GetBoolean()
								 select
									 new
									 {
										 Title = x.GetProperty("Title").GetString(),
										 Authors = x.GetProperty("Authors").GetString()
									 };
			// Displaying the results in a textbox.
			tbJSON.Text += "\n-------------------------------------------------------\n"+
				"Non-open source publications:\r\n" + res1.Aggregate("",
				(result, x) => result + "- " + x.Authors + ": " + x.Title + "\r\n") +
				"-------------------------------------------------------\r\n";
		}
	}
}
