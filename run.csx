using System;
using System.Collections.Generic;
using Microsoft.Azure.Management.DataBox;
using Microsoft.Azure.Management.DataBox.Models;
using Microsoft.Rest.Azure.Authentication;
using Newtonsoft.Json.Linq;

public static void Run(TimerInfo myTimer, TraceWriter log)
{
    log.Info($"C# Timer trigger function executed at: {DateTime.Now}");
try{
        string MyFilePath=@"D:\home\site\wwwroot\function-name\OrderData.json";
        string jsonData=File.ReadAllText(MyFilePath);
          
       
          //Basic Data
            string resourceGroupName = string.Empty; 
            string storageAccountName = string.Empty;
            string jobName = string.Empty;
            string expand = string.Empty;
            string tenantId = string.Empty;
            string subscriptionId = string.Empty;
            string aadApplicationKey = string.Empty;
            string aadApplicationId = string.Empty;
            string strEmailList = string.Empty;
            string Phone = string.Empty;
            string ContactName = string.Empty;
            string location = string.Empty; ;


            //Shipping Address and Disk Size Data
            int diskSize = 0;
            string StreetAddress1 = string.Empty;
            string StreetAddress2 = string.Empty; //optional
            string StreetAddress3 = string.Empty; //optional           
            string Country = string.Empty;
            string PostalCode = string.Empty;
            string City = string.Empty;
            string StateOrProvince = string.Empty;
            string SingleOrMultiple = string.Empty;


            AddressType addressType = AddressType.None;

            JObject o = JObject.Parse(jsonData);
         

            foreach (JProperty column in o.Properties())
            {
                if (!string.IsNullOrEmpty(column.Value.ToString()) || !string.IsNullOrWhiteSpace(column.Value.ToString()))
                {
                    if (column.Name.Equals("resourceGroupName"))
                        resourceGroupName = column.Value.ToString();
                    else if (column.Name.Equals("storageAccountName")) 
                         storageAccountName = column.Value.ToString();
                    else if (column.Name.Equals("location"))
                        location = column.Value.ToString();
                    else if (column.Name.Equals("jobName"))
                        jobName = column.Value.ToString();
                    else if (column.Name.Equals("expand"))
                        expand = column.Value.ToString();
                    else if (column.Name.Equals("tenantId"))
                        tenantId = column.Value.ToString();
                    else if (column.Name.Equals("subscriptionId"))
                        subscriptionId = column.Value.ToString();
                    else if (column.Name.Equals("aadApplicationKey"))
                        aadApplicationKey = column.Value.ToString();
                    else if (column.Name.Equals("aadApplicationId"))
                        aadApplicationId = column.Value.ToString();
                    else if (column.Name.Equals("EmailList"))
                        strEmailList = column.Value.ToString();
                    else if (column.Name.Equals("Phone"))
                        Phone = column.Value.ToString();
                    else if (column.Name.Equals("ContactName"))
                        ContactName = column.Value.ToString();
                    else if (column.Name.Equals("diskSize"))
                        diskSize = Convert.ToInt32(column.Value.ToString());
                    else if (column.Name.Equals("shippingAndDiskDetails"))
                    {
                        JArray srcArray = JArray.Parse(column.Value.ToString());
                        // var srcArraya = jsonLinq.Descendants  ().Where(d => d is JArray).First();

                        foreach (JObject row in srcArray.Children<JObject>())
                        {

                            foreach (JProperty column1 in row.Properties())
                            {
                                if (!string.IsNullOrEmpty(column1.Value.ToString().Trim()) || !string.IsNullOrWhiteSpace(column1.Value.ToString().Trim()))
                                {
                                    if (column1.Name.Equals("StreetAddress1"))
                                        StreetAddress1 = column1.Value.ToString();
                                    if (column1.Name.Equals("StreetAddress2"))
                                        StreetAddress2 = column1.Value.ToString(); //optional
                                    if (column1.Name.Equals("StreetAddress3"))
                                        StreetAddress3 = column1.Value.ToString(); //optional
                                    if (column1.Name.Equals("Country"))
                                        Country = column1.Value.ToString();
                                    if (column1.Name.Equals("PostalCode"))
                                        PostalCode = column1.Value.ToString();
                                    if (column1.Name.Equals("City"))
                                        City = column1.Value.ToString();
                                    if (column1.Name.Equals("StateOrProvince"))
                                        StateOrProvince = column1.Value.ToString();
                                }
                                else if (column1.Name.Equals("StreetAddress2") || column1.Name.Equals("StreetAddress3"))
                                {

                                    if (column1.Name.Equals("StreetAddress2"))
                                        StreetAddress2 = string.IsNullOrEmpty(column1.Value.ToString().Trim()) ? string.Empty : column1.Value.ToString(); //optional
                                    if (column1.Name.Equals("StreetAddress3"))
                                        StreetAddress3 = string.IsNullOrEmpty(column1.Value.ToString().Trim()) ? string.Empty : column1.Value.ToString(); //optional
                                }
                                else
                                {
                                    Console.WriteLine("Invalid Value for parameter:{0}",column1.Name.ToString());
                                }
                            }

                        }
                    }
                    else
                    {
                        log.Info($"Invalid Value for parameter:{0}", column.Name.ToString());
                        return;
                    }
                }
                else
                {
                    log.Info($"Invalid Value for parameter:{0}", column.Name.ToString());
                    return;
                }
            }

            ShippingAddress shippingAddress = new ShippingAddress()
            {
                StreetAddress1 = StreetAddress1,
                StreetAddress2 = StreetAddress2,
                StreetAddress3 = StreetAddress3,
                AddressType = addressType,
                Country = Country,
                PostalCode = PostalCode,
                City = City,
                StateOrProvince = StateOrProvince,
            };


            List<string> EmailList = new List<string>();
            string[] aryEmailList = strEmailList.Split(',');
            foreach (string item in aryEmailList)
            {
                EmailList.Add(item);
            }

            ContactDetails contactDetails = new ContactDetails()
            {
                Phone = Phone,
                EmailList = EmailList,
                ContactName = ContactName
            };
            
            string storageAccProviderType = "Microsoft.Storage"; // Input the storage account provider type; Valid types: Microsoft.Storage / Microsoft.ClassicStorage
            string storageAccResourceGroupName = resourceGroupName;
            string storageAccName = storageAccountName;

            DestinationAccountDetails d = new DestinationAccountDetails();
            d.AccountId = string.Concat("/subscriptions/", subscriptionId, "/resourceGroups/", storageAccResourceGroupName,
                        "/providers/", storageAccProviderType, "/storageAccounts/", storageAccName.ToLower());
            List<DestinationAccountDetails> destinationAccountDetails = new List<DestinationAccountDetails>();
            destinationAccountDetails.Add(d);

            DataBoxDiskJobDetails jobDetails = new DataBoxDiskJobDetails(contactDetails, shippingAddress, destinationAccountDetails,35);
            
            Sku sku = new Sku();
            sku.Name = SkuName.DataBoxDisk;
            JobResource newJobResource = new JobResource();
            newJobResource.Location = location;
            newJobResource.Sku = sku;
            newJobResource.Details = jobDetails;
           

         
            // Initializes a new instance of the DataBoxManagementClient class.
           
             DataBoxManagementClient dataBoxManagementClient = null;
            try
            {

                const string frontDoorUrl = "https://login.microsoftonline.com";
                const string tokenUrl = "https://management.azure.com";
                

                // Validates AAD ApplicationId and returns token
                var credentials = ApplicationTokenProvider.LoginSilentAsync(
                                    tenantId,
                                    aadApplicationId,
                                    aadApplicationKey,
                                    new ActiveDirectoryServiceSettings()
                                    {
                                        AuthenticationEndpoint = new Uri(frontDoorUrl),
                                        TokenAudience = new Uri(tokenUrl),
                                        ValidateAuthority = true,
                                    }).GetAwaiter().GetResult();

                // Initializes a new instance of the DataBoxManagementClient class.
                dataBoxManagementClient = new DataBoxManagementClient(credentials);

                // Set SubscriptionId
                dataBoxManagementClient.SubscriptionId = subscriptionId;
            }
            catch (Exception ex)
            {
                log.Info($"{ex.Message}");
                return;
            }


            bool jobExist = false;
            try
            {
                newJobResource = JobsOperationsExtensions.Get(dataBoxManagementClient.Jobs, resourceGroupName, jobName, expand);
                jobExist = true;
            }
            catch (Exception ex)
            {
               
            }

            if (jobExist)
            {
                Random generator = new Random();
                jobName = jobName + "_" + generator.Next(100000, 1000000);
            }

            // Validate shipping address
             AddressValidationOutput addressValidateResult =
                ServiceOperationsExtensions.ValidateAddressMethod(
                    dataBoxManagementClient.Service, newJobResource.Location, shippingAddress,SkuName.DataBoxDisk);

            try
            {
                // Creates a new job.
                if (addressValidateResult.ValidationStatus == AddressValidationStatus.Valid)
                {
                    JobResource jobResource = JobsOperationsExtensions.Create(
                        dataBoxManagementClient.Jobs,
                        resourceGroupName,
                        jobName, newJobResource);
                    log.Info($"Your order has been created successfully Created Job Name Is:{jobName}"); 
                }
                else
                {
                      log.Info($"Invalid Address Please try again.");
                      return;
                }
            }
            catch(Exception ex)
            {
                     log.Info($"Job not created please contact Administrator. Exception Details:{ex.Message.ToString()}");
                     return;
            }
 }
catch(Exception ex)
{
            log.Info($"Exception Details:{ex.Message.ToString()}");
            return;
}

}
