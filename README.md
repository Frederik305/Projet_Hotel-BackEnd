Installer Visual Studio Community 2022 -> https://visualstudio.microsoft.com/fr/thank-you-downloading-visual-studio/?sku=Community&channel=Release&version=VS2022&source=VSLandingPage&cid=2030&passive=false

Installer SQL Server -> https://go.microsoft.com/fwlink/?linkid=866664&clcid=0x409&culture=en-us&country=us

Installer SSMS ->https://aka.ms/ssmsfullsetup 

installer GitHub Desktop -> https://central.github.com/deployments/desktop/desktop/latest/win32

Cloner le repository GitHub -> https://github.com/Frederik305/Projet_Hotel.git

Installer le NuGet package Microsoft.EntityFrameworkCore dans le projet visual studio 2022 si n'est pas installer

Rejoindre le projet JIRA et se créer un compte ou se connecter à son compte JIRA- https://gaspe-on-top.atlassian.net/jira/software/projects/KAN/boards/1

Créer sa branche GitHub sur GitHub Desktop

Faire un pull de la branche main vers sa branche

Dans SSMS se connecter au serveur local SQL server, créer une nouvelle base de donnée nommée HotelDB et exécuté la requête SQL du fichier HotelDB.sql dans le gitHub pour créer les tables dans la BD. 
Ensuite, exécuté la requête SQL du fichier script.sql également dans le gitHub pour insérer des chambres et des type de chambres dans la base de donnée. 
 
Ouvrir le projet/solution qui se trouve dans la braanche git avec visual studio 2022

Installer les packages suivant dans Visual Studio Community 2022: 
Microsoft.AspNetCore.OpenApi 8.0.8,
Microsoft.EntityFrameworkCore 8.0.8,
Microsoft.EntityFrameworkCore.Abstractions 8.0.8,
Microsoft.EntityFrameworkCore.Analyzers 8.0.8,
Microsoft.EntityFrameworkCore.Design 8.0.8,
Microsoft.EntityFrameworkCore.Relational 8.0.8,
Microsoft.EntityFrameworkCore.SqlServer 8.0.8,
Microsoft.VisualStudio.Azure.Containers.Tools.Targets 1.21.0,
Swashbuckle.AspNetCore 6.4.0

dans le projet ajouter un nouveau élément. Sélectionner "En ligne", rechercher pour "EntityFramework Reverse POCO Generator" dans les Modèles et ajouter l'élément au projet.

exécuter la requête suivante dans la base de données du serveur local ssms pour avoir le string de connexion: select'data source=' + @@servername +';initial catalog=' + db_name() +case type_desc when 'WINDOWS_LOGIN'then ';trusted_connection=true'else';user id=' + suser_name() + ';password=<<YourPassword>>'end as ConnectionString from sys.server_principals where name = suser_name();

ensuite dans le ficher "Database.tt" du projet Visual studio, ajouter le string de connexion à la fin de la ligne suivante = : "Settings.ConnectionString        = " 
