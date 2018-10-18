using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using System.Linq;

namespace AssemblyCSharp.Assets.Scripts
{
    public class StarFactory : MonoBehaviour
    {
        public static IList<Company> Companies { get; private set; }
        private Dictionary<string, Transform> _initialPositionMap; 
        public static Dictionary<string, GameObject> ObjectMap { get; private set; }
        public GameObject StarPrefab;

        void Start()
        {
            Companies = CompanyStore.ParseFromFile();

            _initialPositionMap = new Dictionary<string, Transform>(Companies.Count);
            ObjectMap = new Dictionary<string, GameObject>(Companies.Count);

            foreach(var company in Companies) {

                const float y = 1.62f;

                var t = new Vector3(
                    Random.Range(0, 0.1f),
                    y,
                    Random.Range(0, 0.1f)
                );

                var star = Instantiate(StarPrefab);
                star.transform.position = t;

                _initialPositionMap.Add(company.Id, star.transform);
                ObjectMap.Add(company.Id, star);
            }

            foreach(var o in ObjectMap) {
                var starController = (StarController)o.Value.GetComponent<StarController>();
                Debug.Assert(starController != null);

                var relatedSperes = Companies
                    .Single(p => p.Id == o.Key)
                    .Similarities;

                Debug.Log(string.Format("{0} has {1} similiar companies", o.Key, relatedSperes.Count));

                var relatedSpherePositions = relatedSperes
                    .Select(p => _initialPositionMap[p.Key]);

                starController.relatedSpheres = relatedSpherePositions.ToArray();
            }


        }

        void Update()
        {

        }
    }
}
