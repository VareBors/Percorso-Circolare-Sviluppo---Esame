Script per la creazione del database:
	- Script Creazione DB.sql

Pacchetti Nuget Installati:
	- Microsoft.EntityFrameworkCore(Reti.PortalePercorsi.DAL);
	- Microsoft.EntityFrameworkCore.Proxies (Reti.PortalePercorsi.DAL);
	- Microsoft.EntityFrameworkCore.SqlServer (Reti.PortalePercorsi.DAL);
	- Microsoft.EntityFrameworkCore.Tools (Reti.PortalePercorsi.DAL);
	- Microsoft.Extensions.Logging.Log4Net.AspNetCore (Reti.PortalePercorsi.SL);
	- Swashbuckle.AspNetCore (Reti.PortalePercorsi.SL);
	
Node Modules Installati:
	- @fortawesome/fontawesome-free;
	- bootstrap;
	- http-server;
	- jquery;
	- typescript;
	- @types/bootstrap;
	- @types/jquery;

Primo Avvio:
	Avendo scelto di utilizzare l'approccio "Code First" bisogna creare il Database con lo script presente(Script Creazione DB.sql).
	Modificare la connection string all'interno del file "appsettings.json" sotto la voce "ConnectionString", senza la S finale, presente nel progetto Reti.PortalePercorsi.SL situato in Back-end\Reti.PortalePercorsi.
	In caso venga scaricata la soulution FE senza node_modules, dopo l'installazione di tutti i pacchetti tramite "npm i" è necessario copiare il file bootstrap.min.css, presente nella root del pacchetto, nella folder node_modules\bootstrap\dist\css per avere lo schema colori pensato per l'applicazione.
	Il back-end viene hostato utilizzando IIS integrato con Visual Studio alla porta 49267, accetta tutte le chiamate che arrivano da "http://127.0.0.1:5500" ed "http://localhost:5500".
	Il front-end viene hostato utilizzando http-server, viene lanciato tramite il comando "npm run start" da terminale di Visual Studio Code quando si è nella cartella Front-End. 