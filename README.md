# Art-link

<b><h2>Run backend instructions:</h2></b>

- Make sure you have installed <b>SQL Server </b>and<b> Microsoft SQL Managment Studio</b>

<b><h2>Steps:</h2></b>

- Update your DB, type in terminal project <b>'ChatTeamChallenge.Services.Api'</b>: <code>dotnet ef database update</code>
- Open SSMS, connect with localhost credentials, choose folder <b>Database</b> in pull and find <b>'ChatTeamChallengeDB'</b>

<hr>

<u><b>For Developers</b></u> 
- If you want to create migrations redirect to project <b>'ChatTeamChallenge.Persistence'</b> and type in terminal: <code>dotnet ef migrations add Initial --startup-project ../ChatTeamChallenge.Services.Api --project ../ChatTeamChallenge.Persistence</code>
