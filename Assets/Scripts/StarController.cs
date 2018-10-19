using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StarController : MonoBehaviour
{
    public float preferredHeight;
    public GameObject companyInfoPrefab;
    public Transform[] relatedSpheres;

    public Company company;

    private VRTK.VRTK_InteractableObject objectHandler;
    private bool showsInfo = false;
    private GameObject companyInfo;

    // Use this for initialization
    void Start()
    {
        objectHandler = GetComponent<VRTK.VRTK_InteractableObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (company != null)
        {
            if (objectHandler.IsGrabbed())
            {
                if (!showsInfo)
                {
                    showsInfo = true;
                    if (companyInfo == null)
                    {
                        companyInfo = Instantiate(companyInfoPrefab);
                        ConfigureCompanyInfo();

                    } else
                    {
                        companyInfo.active = true;
                    }
                    // TODO: Maybe play some sound?
                }
                // Clamp info on top of Star
                companyInfo.transform.position = GetComponent<Transform>().position + new Vector3(0, 0.2f, 0);
                companyInfo.transform.LookAt(Camera.main.gameObject.transform.position);
            } else
            {
                if (companyInfo != null)
                {
                    if (showsInfo)
                    {
                        showsInfo = false;
                        companyInfo.active = false;
                    }
                }
            }
        }
    }

    private void ConfigureCompanyInfo()
    {
        TextMeshProUGUI[] fields = companyInfo.GetComponentsInChildren<TextMeshProUGUI>();
        foreach (TextMeshProUGUI field in fields)
        {
            switch (field.gameObject.name)
            {
                case "CompanyName":
                    field.SetText(company.Name);
                    break;
                case "EmployeeCount":
                    field.SetText(string.Concat(company.EmployeeCount.ToString(), " Employees"));
                    break;
                case "YearlyRev":
                    field.SetText(string.Concat(company.Revenue.ToString(), "USD in ", company.Revenue_Year.ToString()));
                    break;
                default:
                    field.SetText("MISSING INFO");
                    break;
            }
        }
    }

    private void FixedUpdate()
    {
        if (relatedSpheres == null)
            return;

        Transform myTransform = GetComponent<Transform>();
        Rigidbody myRb = GetComponent<Rigidbody>();
        for (int i = 0; i < relatedSpheres.Length; i++)
        {
            Vector3 toOtherSphere = (relatedSpheres[i].position - myTransform.position);
            toOtherSphere.Normalize();
            myRb.AddForce(toOtherSphere);
        }
        myRb.AddForce(0, preferredHeight - myTransform.position.y, 0);
    }
}
