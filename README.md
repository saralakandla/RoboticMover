# RoboticMover

**Below are the steps to  open and run the application** 

 1.Download the code from GIT .
 2.Open the code in .net core application.The .net core version that I am using here is 3.0
 3.Build the application and run/debug the same
 4.As I had configured the app1lication with swagger,as soon as you run the application ,you see the swagger page on the browser with all api's and their methods to test the same

Below are the steps to make a request body and test post api either from swagger/Postman

HttpPost URl IS : https://localhost:44339/api/RobotMover/Payload
Request Body Json is: 
{
  "loadid": 231,
  "x": 15,
  "y": 13
} 
see the below response body for the above request

{
  "robotId": 34,
  "distanceToGoal": 14.03567,
  "batteryLevel": 92
}

**Next Steps to do**
1.Add users ,groups and roles 
2.Secure the api with below steps
  1. Authenticating the user by validating the same and generate the token using JWT or OAuth or AAD 
  2. Apply the autorization from logged in user and roles
3.Pass token as header for every subsequest http requests 
4.Design the .net core application in DDD layer architectiure and make a communication with sql server

