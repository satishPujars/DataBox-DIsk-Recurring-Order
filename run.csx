using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Azure;
using Microsoft.Azure.Management.DataBox;
using Microsoft.Azure.Management.DataBox.Models;
using Microsoft.Rest.Azure;
using Microsoft.Rest.Azure.Authentication;

public static void Run(TimerInfo myTimer, TraceWriter log)
{
            log.Info($"Order Creation started at: {DateTime.Now}");            
            string resourceGroupName = "<resource group name in which the function defined>";
            string jobName = "<Order Name which you want to place recurring>";

            const string frontDoorUrl = "https://login.microsoftonline.com";
            const string tokenUrl = "https://management.azure.com";

           // Fetch the configuration parameters.
            string tenantId = "<Tenant id>";
            string subscriptionId = "<Subscription Id>";
            string aadApplicationId = "<AAD Application Id>";
            string aadApplicationKey = "< AAD Application Key>";

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
            JobResource jobResource = JobsOperationsExtensions.Get(dataBoxManagementClient.Jobs, resourceGroupName, jobName, expand);
            
            // Creates a new job.
             try
                {
                    JobResource jobResource1 = JobsOperationsExtensions.Create(
                        dataBoxManagementClient.Jobs,
                        resourceGroupName, "job_"+DateTime.Now.ToFileTime(),jobResource);
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
