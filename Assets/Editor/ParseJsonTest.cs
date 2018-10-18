using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;
using System.Linq;

public class ParseJsonTest {

    public class CompanyStore {

        [JsonProperty("similarity_score")]
        public IList<Company> Companies { get; set; }

        public static IList<Company> ParseFromFile(string pathToFile) {

            string json = File.ReadAllText(pathToFile);
            var score = JsonConvert.DeserializeObject<CompanyStore>(json);
            return score.Companies;
        }
    }

    [Test]
    public void ParseJsonTestSimplePasses() {

        Debug.Log("Current Directory: " + Application.dataPath);

        string file = Path.Combine(Application.dataPath, "Crawler/data.json");
        Assert.IsTrue(File.Exists(file));
        var companies = CompanyStore.ParseFromFile(file);
        Assert.IsTrue(companies.Count == 22);

        Assert.IsTrue(companies.First().EmployeeCount == 500);

    }

    // A UnityTest behaves like a coroutine in PlayMode
    // and allows you to yield null to skip a frame in EditMode
    [UnityTest]
    public IEnumerator ParseJsonTestWithEnumeratorPasses() {
        // Use the Assert class to test conditions.
        // yield to skip a frame
        yield return null;
    }
}
