Project Overview : Many services need to control the rate of traffic they receive to prevent abuse and ensure 
availability for all users. Your task is to design and build a simplified, in-memory rate-limiting 
service. 

Setup and Execution: Clone The Repo Link and then Open RateLimiterDemo.slnx file in Visualstudion and then Run it.
      Open Postman or Browser : Run this Link https://localhost:44381/Data for 5 times you wil get 
      {
    "allowed": true,
    "message": "Request allowed",
    "timestamp": "2026-01-09T17:09:29.7245221Z"
    }
And from 6th time you will get
  {
    "allowed": false,
    "message": "Rate limit exceeded",
    "timestamp": "2026-01-09T17:10:14.6114434Z"
}

API Documentation :   - Algorithm: Fixed Window
                      - Permit Limit: 5 requests
                      - Window Duration: 10 seconds
                      - Queue Limit: 0 (requests beyond limit are rejected immediately)
