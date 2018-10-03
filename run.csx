using System;
using System.Collections.Generic;
using Microsoft.Azure.Management.DataBox;
using Microsoft.Azure.Management.DataBox.Models;
using Microsoft.Rest.Azure.Authentication;

public static void Run(TimerInfo myTimer, TraceWriter log)
{
    log.Info($"C# Timer trigger function executed at: {DateTime.Now}");

      
            string resourceGroupName = "DataboxTestGroup";
            string jobName = "test-order-satish";
            string expand = "details";

            const string frontDoorUrl = "https://login.microsoftonline.com";
            const string tokenUrl = "https://management.azure.com";

            // Fetch the configuration parameters.
            string tenantId = "<Enter Tenant ID>";
            string subscriptionId = "<Enter Subscription Id>";
            string aadApplicationId = "<Enter aadApplicationId>";
            string aadApplicationKey = "<Enter aadApplicationKey>";


            // Validates AAD ApplicationId and returns token
      try
        { 
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
          DataBoxManagementClient dataBoxManagementClient = new DataBoxManagementClient(credentials);

            // Set SubscriptionId
            dataBoxManagementClient.SubscriptionId = subscriptionId;

            // Gets information about the specified job.
            List<string> EmailList = new List<string>();
            EmailList.Add("<Enter your email>");
          JobResource jobResource = JobsOperationsExtensions.Get(dataBoxManagementClient.Jobs, resourceGroupName, jobName, expand);
            jobResource.Details.ContactDetails.Phone = "<Enter your Phone Number>";
            jobResource.Details.ContactDetails.ContactName = "<Enter your contact name>";
            jobResource.Details.ContactDetails.EmailList = EmailList;
            // Creates a new job.
             try
                {
                    Random generator = new Random();
					int r = generator.Next(100000, 999999);
                    JobResource jobResource1 = JobsOperationsExtensions.Create(
                        dataBoxManagementClient.Jobs,
                        resourceGroupName, "job_"+r.ToString(),jobResource);
                        log.Info("Your order has been created successfully"); 
                }
                catch (Exception ex)
                {
                    log.Info(ex.Message.ToString());
                }
      }
    catch (Exception ex)
    {
        log.Info(ex.Message.ToString());
    }
}