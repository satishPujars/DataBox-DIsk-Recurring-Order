# DataBox-DIsk-Recurring-Order

This project will contain Timer Trigger Function which include C# script, json files(Configuration files). This function will execute automatically depending on schedule time mentioned.

When the function is triggered first then it creates job order for DataBoxDisk.

This function is used for Recurring order Purpose that is once you complete this uploading function in your portal and sets schedule for function. Then this function will runs on schedule.

<ul><b><u>For Example</u></b>: If your requirement is you need to order DataBoxDisk of 35TB every week on Monday and you need only for 5 schedule that is after 5 Monday's you don't want to order.
		<li> First you need to specify in schedule option - click on created function select integrate option you will get  Schedule option on Refer this link specify expression (https://en.wikipedia.org/wiki/Cron#CRON_expression).</li>
		<li> Second thing is after fifth Monday you should stop this function so that new orders were not create-- To do this click on your function name on right side there is option stop click on that.</li></ul>

<b><u>what is TimerTrigger Function - C<span>#</span></u></b>

The `TimerTrigger` makes it incredibly easy to have your functions executed on a schedule. This sample demonstrates a simple use case of calling your function every 5 minutes.

<u><b>How it works</b></u>

For a `TimerTrigger` to work, you provide a schedule in the form of a [CRON expression](https://en.wikipedia.org/wiki/Cron#CRON_expression)(See the link for full details). A CRON expression is a string with 6 separate expressions which represent a given schedule via patterns. The pattern we use to represent every 5 minutes is `0 */5 * * * *`. This, in plain text, means: "When seconds is equal to 0, minutes is divisible by 5, for any hour, day of the month, month, day of the week, or year".

 I. Steps to create azure function follow the below link
 
https://docs.microsoft.com/en-us/azure/azure-functions/functions-create-scheduled-function

II. Download OrderData.json File and update fields then upload it in newly created azure function root directory.

How to get field values which is in OrderData.json from Azure Portal

	Ø To find resourceGroupName in the Azure AD portal
	1.	Log in to Microsoft Azure as an administrator.
	2.	While you creating Timer Trigger Function in Resource group option which you mentioned  use that name.

	Ø To find storageAccountName in the Azure AD portal
	1.	Log in to Microsoft Azure as an administrator.
	2.	While you creating Timer Trigger Function in Storage option which you mentioned use that name.
	
	Ø To find tenant ID in the Azure AD portal

	1.	Log in to Microsoft Azure as an administrator.
	2.	In the Microsoft Azure portal, click Azure Active Directory.
	3.	Under Manage, click Properties. The tenant ID is shown in the Directory ID box.

	Ø  To find subscription ID in the Azure AD porta
	
	1. 	Log in to Microsoft Azure as an administrator.
	2.	In the Microsoft Azure portal, click storage account.
	3.	Select any account, in overview tab you will get Subscription ID.

	Ø To find aadApplication ID and AADApplication Key in the Azure AD portal refer below link 
	Only create-an-azure-active-directory-application headed paragraph

	https://docs.microsoft.com/en-us/azure/azure-resource-manager/resource-group-create-service-principal-portal#create-an-azure-active-directory-application
	
	Ø Possible values for Country - US,NL,IE,AT,IT,BE,LV,BG,LT,HR,LU,CY,MT,CZ,DK,PL,EE,PT,FI,RO,FR,SK,DE,SI,GR,ES,HU,SE,GB

	Ø For other fields explanation is given in Orderdata.json file itself.

III.  Copy data from run.csx file and paste it in to script file which is in newly created azure function in Microsoft Azure Portal.

IV.  In script change function-name text as Timer trigger function name which you created in Microsoft Azure Portal.

V.   Download project.json File and upload it in newly created azure function in Microsoft Azure Portal.
