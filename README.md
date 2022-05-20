# SP_DAO_Generator_Application

This application reads the tables from a SQL DB and generates the CRUD SP of the selected tables. Connection to DB using Dapper.
## Please add the config keys according to your local paths and change the DB connection string.
please check the following example for the app.config setup.

<configuration>
	<appSettings>
		<add key="basePath" value="D:\SP_Generator" />
		<add key="ComponentDirectories" value="SP,Models,DAOs" />
	</appSettings>
	<connectionStrings>
		<add name="Default" connectionString="Data Source=.;Initial Catalog=YourDB;User ID=YourUser;Password=YourPassword;Connect Timeout=60000;" />
	</connectionStrings>
</configuration>
