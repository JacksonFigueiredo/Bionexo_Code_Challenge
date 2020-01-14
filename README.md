Monitoring FrameWork

Challenge With 1 API and 1 Service Communicating Each Other, Its a Basic Windows service that collects computer name and date time at the moment, and the service save the date inside the 1st DataBase, later the same service will collect saved data in 1st database and then foward to an API.
Api will receive the data and save this inside a second database.

The services module are using a package called of Topshelf, easy to deploy and develop.

Instructions :

To install service : Open Command Prompt Elevated, Type ServiceName.exe install

To unistall service : Open Command Prompt Elevated, Type ServiceName.exe unistall

Basic Functioning : First Service will collect computer data and send to and database, and later, pick this data from database and foward to an api, api will save in a intermeadiate database.
