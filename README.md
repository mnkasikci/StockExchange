# Stock Exchange
A light stackexchange solution. Works extremely fast.
- Allows users to add moneys and items to the system. All of the additions require admin approval. 
- When different offers have the same price, the older offer is sold first. 
- When a buy order is higher than the sell order (per unit), the transaction is made by whichever order is issued last. This is because the person who issued the last order could already give a lower price. This way, buyers can buy, sellers can sell in bulk without losing any profit. 


- Uses UTC hour for saving transaction dates but shows the local time on the desktop app. 
- Uses API created by ASP.NET Core 
- Uses caliburn micro for MVVM framework. 
- Used technologies: Asp.Net core, WPF, C#, sql

