# DataBox-DIsk-Recurring-Order

 I. Steps to create Function follow the below link
 
https://docs.microsoft.com/en-us/azure/azure-functions/functions-create-scheduled-function

II. Download OrderData.json File and update fields then upload it in newly created Function.

How to get field values which is in OrderData.jso from Azure Portal

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


III.  Copy data from run.csx file and paste it in to script file which is in newly created funtion in portal.

IV.   Download project.json File and upload it in newly created Functions.
    
