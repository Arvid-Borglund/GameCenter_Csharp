# GameCenter_Csharp
This .NET MVC project is meant to provide a complete system for an internet-cafe, it provides functionality for everyday operations for administrators, employees and customers, with a tailored experience for each user type, the login screen sends each user to their personal application interface, where the users can view and moderate their personal profile and access any information that is deemed suitable for that usertype. 

I worked on this/these projects as the only dev and my aim was to produce an application that was in an as "production-ready" deployable state as possible, meaning I strived to provide an platform that have the full functionality of an real world internet-cafe, and I was planning on adding launchable games that the customers could run on their booked computer, but never got as far, however my system still has the capability of administrating game installations on specific computers with license/serial numbers, with customers being able to view installed games on their booked PCs and search the game library and see what is installed where. I also created a bonus system for user retention.
Employees have different access lvls based on their role, that allows them to do the stuff that is suitable for that role, the system also provides functionality for work-schedules, planning, reservations, computer-administration, employee-administration, account handling etc etc, I strived to also adhere to such things as GDPR with each user having full control of their personal data. 
The system does not implement a full payment system, and is meant to be used with physical cash in conjunction with receipts and acts of value being stored in the system, for accounting and accountability, making it hard to abuse/manipulate anything that is of value. 

This project was created for an LU course in program construction, integration/ configuration of ERP-systems and integration technologies. 
ADO.NET and Entity Framework were used for the data exchange between the T-SQL database (handled with Microsoft SSMS) and the program. 
I wanted to make the project as "modular" as possible and did this by creating interfaces and entity managers that was used with a factory pattern. 

I put a lot of time and effort into everything from the database structure to the design, reusability, and ease of maintenance of the program, I even created animations using GIMP (and code) to make the app more fun to use. 
There are of course things that I would have done differently today but this was my first time coding with C#, and I experimented with stuff outside of the curriculum and learned a lot. 
ChatGPT 3.0 was used as a tool/ support during development and a lot of the methods are co-written and have been revised multiple times with and without AI support, but the database structure, program architecture, design and app idea was all me. 

(The console app included is not very interesting and was created solely with ChatGPT to practice AI use)-  
