# TimerTrigger - C<span>#</span>

The `TimerTrigger` makes it incredibly easy to have your functions executed on a schedule. This sample demonstrates a simple use case of calling your function every 5 minutes.

## How it works

For a `TimerTrigger` to work, you provide a schedule in the form of a [cron expression](https://en.wikipedia.org/wiki/Cron#CRON_expression)(See the link for full details). A cron expression is a string with 6 separate expressions which represent a given schedule via patterns. The pattern we use to represent every 5 minutes is `0 */5 * * * *`. This, in plain text, means: "When seconds is equal to 0, minutes is divisible by 5, for any hour, day of the month, month, day of the week, or year".

# DataBox-DIsk-Recurring-Order

 I. Steps to create Function follow the below link
 
https://docs.microsoft.com/en-us/azure/azure-functions/functions-create-scheduled-function

II. Download OrderData.json File and update fields then upload it in newly created Function.

How to get field values which is in OrderData.json from Azure Portal

> To find resourceGroupName in the Azure AD portal
1.	Log in to Microsoft Azure as an administrator.
2.	In the Microsoft Azure portal, click Azure Active Directory.
3.	Click on function Apps
4.	Search your App. The resourceGroupName  shows in resource group column .

> To find tenant ID in the Azure AD portal

1.	Log in to Microsoft Azure as an administrator.
2.	In the Microsoft Azure portal, click Azure Active Directory.
3.	Under Manage, click Properties. The tenant ID is shown in the Directory ID box.

> To find subscription ID in the Azure AD porta

1. 	Log in to Microsoft Azure as an administrator.
2.	In the Microsoft Azure portal, click storage account.
3.	Select any account, in overview tab you will get Subscription ID.

>  To find aadApplication ID and AADApplication Key in the Azure AD portal refer below link 
	Only create-an-azure-active-directory-application headed paragraph

https://docs.microsoft.com/en-us/azure/azure-resource-manager/resource-group-create-service-principal-portal#create-an-azure-active-directory-application

>  explanation is given to get other fields in Orderdata.json file itself.

III.  Copy data from run.csx file and paste it in to script file which is in newly created funtion in portal.

IV.   Download project.json File and upload it in newly created Function.
    

