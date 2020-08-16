Steps to run 

1.  Checkout the project tandem-contact
    While being on the root directory where you checked out the code, run on bash command
       
          scripts/start_db.sh
          
This will prepare the postgres database in a docker container

2.  Navigate to ```src\ContactAddress\ContactAddress``` on command prompt and run the following command to create the database
      
     ``` dotnet-ef database update  ```     
     
3. Run the solutuon, use swagger and make api calls with postman (http://localhost:5000/swagger

In order to run the integration API tests, make you sure you are running the ContactAddress API at port 5000
