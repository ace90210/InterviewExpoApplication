To add migrations use the following command on package manager console, make sure the correct provider is set in app settings and project selected in dropdown as well

Add-Migration -Name MigrationName -StartupProject InterviewExpoApplication.Server -Project InterviewExpoApplication.Data.Sqlite -Context MainContext -OutputDir Migrations

or 

Add-Migration -Name MigrationName -StartupProject InterviewExpoApplication.Server -Project InterviewExpoApplication.Data.SqlServer -Context MainContext -OutputDir Migrations