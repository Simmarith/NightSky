﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;

public class Company {

	[JsonProperty("id")]
	public string Id { get; set; }

	[JsonProperty("name")]
	public string Name { get; set; }

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
