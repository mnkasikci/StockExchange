# Stock Exchange
A light stackexchange solution. Works extremely fast.
- Allows users to add moneys and items to the system. All of the additions require admin approval. 
- When different offers have the same price, the older offer is sold first. 
- When a buy order is higher than the sell order (per unit), the transaction is made by whichever order is issued last. This is because the person who issued the last order could already give a lower price. This way, buyers can buy, sellers can sell in bulk without losing any profit. 

- Supports 10+ currencies. When users add a different currency to their account its exchange rate to TRY is calculated from https://www.tcmb.gov.tr/kurlar/today.xml upon admin approval. 
- All buys and sales has 1% comission fee which is paid to the admin.
- Allows users to export their transactions to an xlsx file. Users can limit it to dates and to roles they took part in that transaction (buyer or seller)
- Keeps all the data on the Sql (sql server)
- Uses UTC hour for saving transaction dates but shows the local time on the desktop app. This way users from different timezones can use the program with their own timezone convenience and no data is lost or assesed unfairly on the grounds of time difference. 
- Uses Technologies: Asp.Net core, WPF, C#, sql
- Uses API created by ASP.NET Core 
- Uses Caliburn micro for MVVM framework. 


