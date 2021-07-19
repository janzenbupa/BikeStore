# BikeStore
An API designed for a bike rental shop to manage customer orders and bike inventory.


This is built in ASP.NET Core. So far the controllers that are set up to accept GET and POST requests are the OrdersController and the BikesController. Each controller will
a BaseResponse type or a GetBaseResponse type depending on if it is the GET action or the POST action. Each POST action will accept a type that will contain the base level
of information needed to verify the request and successfully "POST" the new request. The controller models can be found under BikeStore/Models.

This project is using ADO.NET instead of Entity Framework. The logic for connecting to the database can be found in BikeStore/DataAccessLayer/Logic. The database models that
are used to replicate the database and are used by the Data Access Layer can be found under BikeStore/DataAccessLayer/Models. Each controller will have a logic class in the
DataAccessLayer in their corresponding Logic folder that will make calls the database. These calls are for saving data (stored procedures), and reading data (views).
