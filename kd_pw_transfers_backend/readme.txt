ASP.NET Core 3.1 - Simple API for making Transfers between the inner system Users, with Authentication and Registration.

To get it working on your station:

1. Go to MySQL database and use kd_pw_transfers_db_tables_creator.sql for the database creation
2. Go to Startup.cs file and replace my DB version (line 33) with yours
3. Edit appsetting.json with you "DefaultConnection" line - replace <YOUR_USER> and <YOUR_PASSWORD>
4. Install nuget packages from my list if your Visual Studio asks for that
5. Compile and run the application.
6. All API functions are desribed in .png files attached to the root folder of this git. You can try bad POSTs and GETs as well
7. The project is backend part of 2-partial project giving RestAPI funcionality to https://github.com/AlexanderBrovchenko/kd_angular_pw_transfers_frontend. 
	Edit "origin" parameter (Startup.cs, line 98) with proper url you have deployed the 2nd part on.