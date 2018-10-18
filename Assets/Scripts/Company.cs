using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;

public class Company {

	[JsonProperty("id")]
	public string Id { get; set; }

	[JsonProperty("name")]
	public string Name { get; set; }

    [JsonProperty("num_employees")]
    public int? EmployeeCount { get; set; }

    [JsonProperty("amt_revenue")]
    public long? Revenue { get; set; }

    [JsonProperty("amt_revenue_year")]
    public long? Revenue_Year { get; set; }

    [JsonProperty("similarities")]
	public IDictionary<string, float> Similarities { get; set; }
}


public class CompanyStore
{
    [JsonProperty("similarity_score")]
    public IList<Company> Companies { get; set; }


    public static IList<Company> ParseFromFile()
    {
        var defaultFilePath = Path.Combine(Application.dataPath, "Crawler/data.json");
        return ParseFromFile(defaultFilePath);
    }

    public static IList<Company> ParseFromFile(string pathToFile)
    {

        string json = File.ReadAllText(pathToFile);
        var score = JsonConvert.DeserializeObject<CompanyStore>(json);
        return score.Companies;
    }
}
