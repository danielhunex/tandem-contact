Steps to run 

1. User run scripts/start_db.sh    This will create the postgres database in docker 
2. You may need to do dotnet-ef migrations add InitialCreate to initialize the database
3. Start the ContactAddress, use swagger and make api calls with postman (http://localhost:5000/swagger

In order to run the tests, make you sure you are running the ContactAddress API at por 5000